using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObject.Enums;

namespace BusinessObject.Entities
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        [Column("booking_id")]
        public string BookingId { get; set; } = Guid.NewGuid().ToString();

        [Key]
        [Column("station_id")]
        public string StationId { get; set; } = string.Empty;

        [Key]
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;

        [Key]
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;

        [Key]
        [Column("battery_id")]
        public string BatteryId { get; set; } = string.Empty;

        [Key]
        [Column("battery_type_id")]
        public string BatteryTypeId { get; set; } = string.Empty;

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("status")]
        public BBRStatus Status { get; set; } = BBRStatus.Pending;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key navigation properties
        [ForeignKey("StationId")]
        public virtual Station Station { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("VehiclesId")]
        public virtual Vehicle Vehicle { get; set; } = null!;

        [ForeignKey("BatteryId")]
        public virtual Battery Battery { get; set; } = null!;

        [ForeignKey("BatteryTypeId")]
        public virtual BatteryType BatteryType { get; set; } = null!;
    }
}
