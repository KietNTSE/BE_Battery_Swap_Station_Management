﻿using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos
{
    public class BatteryRequest
    {
        [Required]
        public int SerialNo { get; set; }

        [Required]
        public BatteryOwner Owner { get; set; }

        [Required]
        public BatteryStatus Status { get; set; } = BatteryStatus.Available;

        [Required]
        public string Voltage { get; set; } = null!;

        [Required]
        public string CapacityWh { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? StationId { get; set; }

        public string? BatteryTypeId { get; set; }

        public string? UserId { get; set; }

        public string? ReservationId { get; set; }
    }
    public class BatteryResponse
    {
        public string BatteryId { get; set; } = null!;
        public int SerialNo { get; set; }
        public BatteryOwner Owner { get; set; }
        public BatteryStatus Status { get; set; }
        public string Voltage { get; set; } = null!;
        public string CapacityWh { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? StationId { get; set; }
        public string? StationName { get; set; }
        public string? BatteryTypeId { get; set; }
        public string? BatteryTypeName { get; set; }
        public string? UserId { get; set; }
        public string? ReservationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
