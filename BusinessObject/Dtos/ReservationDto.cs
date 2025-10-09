using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos
{
    public class ReservationRequest
    {
        public string? ReservationId { get; set; }
        public string StationInventoryId { get; set; }
        public BBRStatus Status { get; set; } = BBRStatus.Pending;
        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiredAt { get; set; }
    }

    public class ReservationResponse
    {
        public string ReservationId { get; set; }
        public string StationInventoryId { get; set; }
        public BBRStatus Status { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
