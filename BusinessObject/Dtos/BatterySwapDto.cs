using System.ComponentModel.DataAnnotations;
using BusinessObject.Enums;

namespace BusinessObject.DTOs;

public class BatterySwapSummaryResponse
{
    public string SwapId { get; set; } = string.Empty;
    public string VehicleId { get; set; } = string.Empty;
    public string VehicleLicensePlate { get; set; } = string.Empty;
    public string BatteryId { get; set; } = string.Empty;
    public string BatterySerialNo { get; set; } = string.Empty;
    public string StationId { get; set; } = string.Empty;
    public string StationName { get; set; } = string.Empty;
    public string StaffId { get; set; } = string.Empty;
    public string StaffName { get; set; } = string.Empty;
    public BBRStatus Status { get; set; }
    public DateTime SwappedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class BatterySwapResponse : BatterySwapSummaryResponse
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string UserPhone { get; set; } = string.Empty;
    public string VehicleBrand { get; set; } = string.Empty;
    public string VehicleModel { get; set; } = string.Empty;
    public string BatteryTypeId { get; set; } = string.Empty;
    public string BatteryTypeName { get; set; } = string.Empty;
    public string? BatteryVoltage { get; set; }
    public string? BatteryCapacity { get; set; }
    public string StationAddress { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? Notes { get; set; }
}

public class BatterySwapDetailDto : BatterySwapSummaryResponse
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string UserPhone { get; set; } = string.Empty;
    public string VehicleBrand { get; set; } = string.Empty;
    public string VehicleModel { get; set; } = string.Empty;
    public string BatteryTypeName { get; set; } = string.Empty;
    public string BatteryVoltage { get; set; } = string.Empty;
    public string BatteryCapacity { get; set; } = string.Empty;
    public string StationAddress { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateBatterySwapRequest
{
    [Required] public string NewBatteryId { get; set; } = string.Empty;
}