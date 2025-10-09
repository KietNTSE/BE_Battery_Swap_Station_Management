using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos
{
    public class StationInventoryRequest
    {
        public string? StationInventoryId { get; set; }
        public string StationId { get; set; }
        public int MaintenanceCount { get; set; }
        public int FullCount { get; set; }
        public int ChargingCount { get; set; }
    }

    public class StationInventoryResponse
    {
        public string StationInventoryId { get; set; }
        public string StationId { get; set; }
        public int MaintenanceCount { get; set; }
        public int FullCount { get; set; }
        public int ChargingCount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
