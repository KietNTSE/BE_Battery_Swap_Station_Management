﻿// Service/Implementations/BatterySwapService.cs
using System.Linq; // THÊM để dùng Where method cho char
using BusinessObject;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using Service.Utils;
using System.Net;

namespace Service.Implementations
{
    public class BatterySwapService(ApplicationDbContext context, IHttpContextAccessor accessor) : IBatterySwapService
    {
        public async Task<BatterySwapResponse> CreateBatterySwapFromBookingAsync(CreateBatterySwapRequest request)
        {
            var staffUserId = JwtUtils.GetUserId(accessor);
            if (string.IsNullOrEmpty(staffUserId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Unauthorized",
                    Code = "401"
                };

            // Validate staff role
            var staff = await context.Users.FindAsync(staffUserId);
            if (staff == null || staff.Role != UserRole.Staff)
                throw new ValidationException
                {
                    ErrorMessage = "Only staff can create battery swaps",
                    Code = "403",
                    StatusCode = HttpStatusCode.Forbidden
                };

            // Get booking with all necessary includes
            var booking = await context.Bookings
                .Include(b => b.User)
                .Include(b => b.Vehicle)
                .Include(b => b.Station)
                .Include(b => b.BatteryBookingSlots)
                    .ThenInclude(bbs => bbs.Battery)
                        .ThenInclude(b => b.BatteryType)
                .FirstOrDefaultAsync(b => b.BookingId == request.BookingId);

            if (booking == null)
                throw new ValidationException
                {
                    ErrorMessage = "Booking not found",
                    Code = "404",
                    StatusCode = HttpStatusCode.NotFound
                };

            // Validate booking status
            if (booking.Status != BBRStatus.Confirmed)
                throw new ValidationException
                {
                    ErrorMessage = "Only confirmed bookings can be used for battery swap",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };

            // Check if staff is assigned to this station
            var stationStaff = await context.StationStaffs
                .FirstOrDefaultAsync(ss => ss.UserId == staffUserId && ss.StationId == booking.StationId);

            if (stationStaff == null)
                throw new ValidationException
                {
                    ErrorMessage = "You are not assigned to manage this station",
                    Code = "403",
                    StatusCode = HttpStatusCode.Forbidden
                };

            // Check if battery swap already exists for this booking
            var existingSwap = await context.BatterySwaps
                .FirstOrDefaultAsync(bs => bs.VehicleId == booking.VehicleId &&
                                          bs.UserId == booking.UserId &&
                                          bs.StationId == booking.StationId &&
                                          bs.CreatedAt.Date == DateTime.UtcNow.Date);

            if (existingSwap != null)
                throw new ValidationException
                {
                    ErrorMessage = "Battery swap already exists for this booking today",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };

            // Validate new battery
            var newBattery = await context.Batteries
                .Include(b => b.BatteryType)
                .Include(b => b.StationBatterySlot)
                .FirstOrDefaultAsync(b => b.BatteryId == request.ToBatteryId &&
                                          b.StationId == booking.StationId &&
                                          b.Status == BatteryStatus.Available);

            if (newBattery == null)
                throw new ValidationException
                {
                    ErrorMessage = "New battery not found or not available at this station",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };

            // Validate battery type compatibility
            var vehicleBatteryType = await context.Vehicles
                .Where(v => v.VehicleId == booking.VehicleId)
                .Select(v => v.BatteryTypeId)
                .FirstOrDefaultAsync();

            if (newBattery.BatteryTypeId != vehicleBatteryType)
                throw new ValidationException
                {
                    ErrorMessage = "Battery type is not compatible with the vehicle",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };

            // Get old battery from booking
            var oldBattery = booking.BatteryBookingSlots.FirstOrDefault()?.Battery;
            if (oldBattery == null)
                throw new ValidationException
                {
                    ErrorMessage = "No battery found in booking slots",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };

            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // Create battery swap
                var batterySwap = new BatterySwap
                {
                    VehicleId = booking.VehicleId,
                    StationStaffId = stationStaff.StationStaffId,
                    UserId = booking.UserId,
                    StationId = booking.StationId,
                    BatteryId = oldBattery.BatteryId,
                    ToBatteryId = request.ToBatteryId,
                    Status = BBRStatus.Pending,
                    SwappedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };

                context.BatterySwaps.Add(batterySwap);

                // Update battery statuses
                oldBattery.Status = BatteryStatus.Charging;
                newBattery.Status = BatteryStatus.InUse;

                // Update station battery slot status
                if (newBattery.StationBatterySlot != null)
                {
                    newBattery.StationBatterySlot.Status = SBSStatus.Full_slot; // Use int instead of enum
                    newBattery.StationBatterySlot.LastUpdated = DateTime.UtcNow;
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Query lại với full includes
                var savedBatterySwap = await context.BatterySwaps
                    .Include(bs => bs.Vehicle)
                    .Include(bs => bs.User)
                    .Include(bs => bs.Station)
                    .Include(bs => bs.StationStaff)
                        .ThenInclude(ss => ss.User)
                    .Include(bs => bs.Battery)
                    .Include(bs => bs.ToBattery)
                    .Include(bs => bs.Payments)
                    .FirstOrDefaultAsync(bs => bs.SwapId == batterySwap.SwapId);

                return MapToBatterySwapResponse(savedBatterySwap!);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ValidationException
                {
                    ErrorMessage = ex.Message,
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<BatterySwapResponse> UpdateBatterySwapStatusAsync(string swapId, UpdateBatterySwapStatusRequest request)
        {
            var staffUserId = JwtUtils.GetUserId(accessor);
            if (string.IsNullOrEmpty(staffUserId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Unauthorized",
                    Code = "401"
                };

            var batterySwap = await context.BatterySwaps
                .Include(bs => bs.Vehicle)
                .Include(bs => bs.User)
                .Include(bs => bs.Station)
                .Include(bs => bs.StationStaff)
                    .ThenInclude(ss => ss.User)
                .Include(bs => bs.Battery)
                .Include(bs => bs.ToBattery)
                .Include(bs => bs.Payments)
                .FirstOrDefaultAsync(bs => bs.SwapId == swapId);

            if (batterySwap == null)
                throw new ValidationException
                {
                    ErrorMessage = "Battery swap not found",
                    Code = "404",
                    StatusCode = HttpStatusCode.NotFound
                };

            // Check if staff is assigned to this station
            var stationStaff = await context.StationStaffs
                .FirstOrDefaultAsync(ss => ss.UserId == staffUserId && ss.StationId == batterySwap.StationId);

            if (stationStaff == null)
                throw new ValidationException
                {
                    ErrorMessage = "You are not assigned to manage this station",
                    Code = "403",
                    StatusCode = HttpStatusCode.Forbidden
                };

            // Validate status transition
            if (!IsValidStatusTransition(batterySwap.Status, request.Status))
                throw new ValidationException
                {
                    ErrorMessage = $"Invalid status transition from {batterySwap.Status} to {request.Status}",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };

            try
            {
                batterySwap.Status = request.Status;

                // If completing the swap, update related booking status
                if (request.Status == BBRStatus.Completed)
                {
                    var relatedBooking = await context.Bookings
                        .FirstOrDefaultAsync(b => b.VehicleId == batterySwap.VehicleId &&
                                                  b.UserId == batterySwap.UserId &&
                                                  b.StationId == batterySwap.StationId &&
                                                  b.Status == BBRStatus.Confirmed);

                    if (relatedBooking != null)
                    {
                        relatedBooking.Status = BBRStatus.Completed;
                    }
                }

                await context.SaveChangesAsync();
                return MapToBatterySwapResponse(batterySwap);
            }
            catch (Exception ex)
            {
                throw new ValidationException
                {
                    ErrorMessage = ex.Message,
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<BatterySwapDetailResponse> GetBatterySwapDetailAsync(string swapId)
        {
            var currentUserId = JwtUtils.GetUserId(accessor);
            if (string.IsNullOrEmpty(currentUserId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Unauthorized",
                    Code = "401"
                };

            var batterySwap = await context.BatterySwaps
                .Include(bs => bs.Vehicle)
                .Include(bs => bs.User)
                .Include(bs => bs.Station)
                .Include(bs => bs.StationStaff)
                    .ThenInclude(ss => ss.User)
                .Include(bs => bs.Battery)
                .Include(bs => bs.ToBattery)
                .Include(bs => bs.Payments)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(bs => bs.SwapId == swapId);

            if (batterySwap == null)
                throw new ValidationException
                {
                    ErrorMessage = "Battery swap not found",
                    Code = "404",
                    StatusCode = HttpStatusCode.NotFound
                };

            var currentUser = await context.Users.FindAsync(currentUserId);

            // Check permission
            var hasPermission = false;

            if (currentUser?.Role == UserRole.Staff)
            {
                var isStaffOfStation = await context.StationStaffs
                    .AnyAsync(ss => ss.UserId == currentUserId && ss.StationId == batterySwap.StationId);
                hasPermission = isStaffOfStation;
            }
            else if (currentUser?.Role == UserRole.Admin)
            {
                hasPermission = batterySwap.Station.UserId == currentUserId;
            }
            else if (currentUser?.Role == UserRole.Driver)
            {
                hasPermission = batterySwap.UserId == currentUserId;
            }

            if (!hasPermission)
                throw new ValidationException
                {
                    ErrorMessage = "You don't have permission to view this battery swap",
                    Code = "403",
                    StatusCode = HttpStatusCode.Forbidden
                };

            var response = MapToBatterySwapDetailResponse(batterySwap);

            // Add payment info if exists
            var payment = batterySwap.Payments.FirstOrDefault();
            if (payment != null)
            {
                response.Payment = new PaymentResponse
                {
                    PayId = payment.PayId,
                    UserId = payment.UserId,
                    UserName = payment.User.FullName,
                    SwapId = payment.SwapId,
                    OrderCode = payment.OrderCode,
                    Amount = payment.Amount,
                    Currency = payment.Currency,
                    PaymentMethod = payment.PaymentMethod,
                    Status = payment.Status,
                    CreatedAt = payment.CreatedAt
                };
            }

            return response;
        }

        public async Task<PaginationWrapper<List<BatterySwapResponse>, BatterySwapResponse>> GetStationBatterySwapsAsync(string stationId, int page, int pageSize, string? search)
        {
            var staffUserId = JwtUtils.GetUserId(accessor);
            if (string.IsNullOrEmpty(staffUserId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Unauthorized",
                    Code = "401"
                };

            var currentUser = await context.Users.FindAsync(staffUserId);
            var hasPermission = false;

            if (currentUser?.Role == UserRole.Staff)
            {
                hasPermission = await context.StationStaffs
                    .AnyAsync(ss => ss.UserId == staffUserId && ss.StationId == stationId);
            }
            else if (currentUser?.Role == UserRole.Admin)
            {
                hasPermission = await context.Stations
                    .AnyAsync(s => s.StationId == stationId && s.UserId == staffUserId);
            }

            if (!hasPermission)
                throw new ValidationException
                {
                    ErrorMessage = "You don't have permission to view battery swaps for this station",
                    Code = "403",
                    StatusCode = HttpStatusCode.Forbidden
                };

            var query = context.BatterySwaps
                .Include(bs => bs.Vehicle)
                .Include(bs => bs.User)
                .Include(bs => bs.Station)
                .Include(bs => bs.StationStaff)
                    .ThenInclude(ss => ss.User)
                .Include(bs => bs.Battery)
                .Include(bs => bs.ToBattery)
                .Include(bs => bs.Payments)
                .Where(bs => bs.StationId == stationId);

            // FIX: Chỉ search trên string fields, bỏ serial number search
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(bs => bs.User.FullName.Contains(search) ||
                                         bs.Vehicle.LicensePlate.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var batterySwaps = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(bs => bs.CreatedAt)
                .ToListAsync();

            var responses = batterySwaps.Select(MapToBatterySwapResponse).ToList();

            return new PaginationWrapper<List<BatterySwapResponse>, BatterySwapResponse>(responses, totalItems, page, pageSize);
        }

        public async Task<PaginationWrapper<List<BatterySwapResponse>, BatterySwapResponse>> GetMyBatterySwapsAsync(int page, int pageSize, string? search)
        {
            var staffUserId = JwtUtils.GetUserId(accessor);
            if (string.IsNullOrEmpty(staffUserId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Unauthorized",
                    Code = "401"
                };

            var assignedStations = await context.StationStaffs
                .Where(ss => ss.UserId == staffUserId)
                .Select(ss => ss.StationId)
                .ToListAsync();

            if (!assignedStations.Any())
                return new PaginationWrapper<List<BatterySwapResponse>, BatterySwapResponse>(
                    new List<BatterySwapResponse>(), 0, page, pageSize);

            var query = context.BatterySwaps
                .Include(bs => bs.Vehicle)
                .Include(bs => bs.User)
                .Include(bs => bs.Station)
                .Include(bs => bs.StationStaff)
                    .ThenInclude(ss => ss.User)
                .Include(bs => bs.Battery)
                .Include(bs => bs.ToBattery)
                .Include(bs => bs.Payments)
                .Where(bs => assignedStations.Contains(bs.StationId));

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(bs => bs.User.FullName.Contains(search) ||
                                         bs.Vehicle.LicensePlate.Contains(search) ||
                                         bs.Station.Name.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var batterySwaps = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(bs => bs.CreatedAt)
                .ToListAsync();

            var responses = batterySwaps.Select(MapToBatterySwapResponse).ToList();

            return new PaginationWrapper<List<BatterySwapResponse>, BatterySwapResponse>(responses, totalItems, page, pageSize);
        }

        public async Task<List<BatterySwapResponse>> GetSwapsByBookingAsync(string bookingId)
        {
            var currentUserId = JwtUtils.GetUserId(accessor);
            if (string.IsNullOrEmpty(currentUserId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Unauthorized",
                    Code = "401"
                };

            var booking = await context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
                throw new ValidationException
                {
                    ErrorMessage = "Booking not found",
                    Code = "404",
                    StatusCode = HttpStatusCode.NotFound
                };

            var swaps = await context.BatterySwaps
                .Include(bs => bs.Vehicle)
                .Include(bs => bs.User)
                .Include(bs => bs.Station)
                .Include(bs => bs.StationStaff)
                    .ThenInclude(ss => ss.User)
                .Include(bs => bs.Battery)
                .Include(bs => bs.ToBattery)
                .Include(bs => bs.Payments)
                .Where(bs => bs.VehicleId == booking.VehicleId &&
                             bs.UserId == booking.UserId &&
                             bs.StationId == booking.StationId &&
                             bs.CreatedAt.Date == DateTime.UtcNow.Date)
                .ToListAsync();

            return swaps.Select(MapToBatterySwapResponse).ToList();
        }

        #region Private Helper Methods

        private static bool IsValidStatusTransition(BBRStatus currentStatus, BBRStatus newStatus)
        {
            return currentStatus switch
            {
                BBRStatus.Pending => newStatus is BBRStatus.Confirmed or BBRStatus.Cancelled,
                BBRStatus.Confirmed => newStatus is BBRStatus.Completed or BBRStatus.Cancelled,
                BBRStatus.Completed => false,
                BBRStatus.Cancelled => false,
                _ => false
            };
        }

        private static BatterySwapResponse MapToBatterySwapResponse(BatterySwap batterySwap)
        {
            return new BatterySwapResponse
            {
                SwapId = batterySwap.SwapId,
                VehicleId = batterySwap.VehicleId,
                LicensePlate = batterySwap.Vehicle.LicensePlate,
                VehicleBrand = batterySwap.Vehicle.VBrand,
                VehicleModel = batterySwap.Vehicle.Model,
                StationStaffId = batterySwap.StationStaffId,
                UserId = batterySwap.UserId,
                UserName = batterySwap.User.FullName,
                UserPhone = batterySwap.User.Phone ?? "",
                StationId = batterySwap.StationId,
                StationName = batterySwap.Station.Name,
                BatteryId = batterySwap.BatteryId,
                BatterySerial = batterySwap.Battery.SerialNo, // Parse từ string SerialNo
                ToBatteryId = batterySwap.ToBatteryId,
                ToBatterySerial = batterySwap.ToBattery.SerialNo, // Parse từ string SerialNo
                Status = batterySwap.Status,
                SwappedAt = batterySwap.SwappedAt,
                CreatedAt = batterySwap.CreatedAt,
                HasPayment = batterySwap.Payments.Any(),
                PaymentId = batterySwap.Payments.FirstOrDefault()?.PayId
            };
        }

        private static BatterySwapDetailResponse MapToBatterySwapDetailResponse(BatterySwap batterySwap)
        {
            var response = MapToBatterySwapResponse(batterySwap);
            return new BatterySwapDetailResponse
            {
                SwapId = response.SwapId,
                VehicleId = response.VehicleId,
                LicensePlate = response.LicensePlate,
                VehicleBrand = response.VehicleBrand,
                VehicleModel = response.VehicleModel,
                StationStaffId = response.StationStaffId,
                UserId = response.UserId,
                UserName = response.UserName,
                UserPhone = response.UserPhone,
                StationId = response.StationId,
                StationName = response.StationName,
                BatteryId = response.BatteryId,
                BatterySerial = response.BatterySerial,
                ToBatteryId = response.ToBatteryId,
                ToBatterySerial = response.ToBatterySerial,
                Status = response.Status,
                SwappedAt = response.SwappedAt,
                CreatedAt = response.CreatedAt,
                HasPayment = response.HasPayment,
                PaymentId = response.PaymentId
            };
        }
        #endregion
    }
}