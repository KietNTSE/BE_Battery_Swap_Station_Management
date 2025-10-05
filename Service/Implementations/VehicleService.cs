using System.Net;
using BusinessObject;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using Service.Utils;

namespace Service.Implementations;

public class VehicleService(ApplicationDbContext context, IHttpContextAccessor accessor) : IVehicleService
{
    public async Task<VehicleResponse> GetVehicleAsync(string vehicleId)
    {
        var vehicle = await context.Vehicles
            .Include(v => v.BatteryType)
            .Include(v => v.User)
            .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);
        if (vehicle is null)
            throw new ValidationException
            {
                ErrorMessage = "Vehicle not found",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        return new VehicleResponse
        {
            VehicleId = vehicle.VehicleId,
            UserId = vehicle.UserId,
            UserName = vehicle.User.FullName,
            BatteryTypeId = vehicle.BatteryTypeId,
            BatteryTypeName = vehicle.BatteryType.BatteryTypeName,
            VBrand = vehicle.VBrand,
            Model = vehicle.Model,
            LicensePlate = vehicle.LicensePlate
        };
    }

    public async Task<VehicleResponse> CreateVehicleAsync(VehicleRequest vehicleRequest)
    {
        var userId = JwtUtils.GetUserId(accessor);
        if (string.IsNullOrEmpty(userId))
            throw new ValidationException
            {
                StatusCode = HttpStatusCode.Unauthorized,
                ErrorMessage = "Unauthorized",
                Code = "401"
            };

        await ValidateVehicleRequest(vehicleRequest);

        var vehicleEntity = new Vehicle
        {
            BatteryTypeId = vehicleRequest.BatteryTypeId,
            VBrand = vehicleRequest.VBrand,
            Model = vehicleRequest.Model,
            LicensePlate = vehicleRequest.LicensePlate,
            UserId = userId
        };

        try
        {
            context.Vehicles.Add(vehicleEntity);
            await context.SaveChangesAsync();
            return new VehicleResponse
            {
                VehicleId = vehicleEntity.VehicleId,
                UserId = vehicleEntity.UserId,
                UserName = vehicleEntity.User.FullName,
                BatteryTypeId = vehicleEntity.BatteryTypeId,
                BatteryTypeName = vehicleEntity.BatteryType.BatteryTypeName,
                VBrand = vehicleEntity.VBrand,
                Model = vehicleEntity.Model,
                LicensePlate = vehicleEntity.LicensePlate
            };
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

    public async Task<VehicleResponse> UpdateVehicleAsync(string vehicleId, VehicleRequest vehicleRequest)
    {
        var vehicleEntity = await context.Vehicles.Include(v => v.BatteryType)
            .Include(v => v.User)
            .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);
        if (vehicleEntity is null)
            throw new ValidationException
            {
                ErrorMessage = "Vehicle not found",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };
        await ValidateVehicleRequest(vehicleRequest);
        vehicleEntity.BatteryTypeId = vehicleRequest.BatteryTypeId;
        vehicleEntity.VBrand = vehicleRequest.VBrand;
        vehicleEntity.Model = vehicleRequest.Model;
        vehicleEntity.LicensePlate = vehicleRequest.LicensePlate;
        try
        {
            await context.SaveChangesAsync();
            return new VehicleResponse
            {
                VehicleId = vehicleEntity.VehicleId,
                UserId = vehicleEntity.UserId,
                UserName = vehicleEntity.User.FullName,
                BatteryTypeId = vehicleEntity.BatteryTypeId,
                BatteryTypeName = vehicleEntity.BatteryType.BatteryTypeName,
                VBrand = vehicleEntity.VBrand,
                Model = vehicleEntity.Model,
                LicensePlate = vehicleEntity.LicensePlate
            };
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

    public Task<VehicleResponse> DeleteVehicleAsync(string vehicleId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginationWrapper<List<VehicleResponse>, VehicleResponse>> GetAllVehiclesAsync(int page,
        int pageSize, string? search)
    {
        var query = context.Vehicles.AsQueryable();
        if (search is not null) query = query.Where(v => v.Model.Contains(search) || v.LicensePlate.Contains(search));
        var totalItems = await query.CountAsync();
        var users = await query.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(v => v.CreatedAt)
            .Select(v => new VehicleResponse
            {
                UserId = v.UserId,
                UserName = v.User.FullName,
                BatteryTypeId = v.BatteryTypeId,
                BatteryTypeName = v.BatteryType.BatteryTypeName,
                VBrand = v.VBrand,
                Model = v.Model,
                LicensePlate = v.LicensePlate
            }).ToListAsync();
        return new PaginationWrapper<List<VehicleResponse>, VehicleResponse>(users, totalItems, page, pageSize);
    }

    private async Task ValidateVehicleRequest(VehicleRequest vehicleRequest)
    {
        if (!await context.BatteryTypes.AnyAsync(bt => bt.BatteryTypeId == vehicleRequest.BatteryTypeId))
            throw new ValidationException
            {
                ErrorMessage = "Battery type not found",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (await context.Vehicles.AnyAsync(v => v.LicensePlate == vehicleRequest.LicensePlate))
            throw new ValidationException
            {
                ErrorMessage = "LicensePlate is already existed in our system",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };
    }
}