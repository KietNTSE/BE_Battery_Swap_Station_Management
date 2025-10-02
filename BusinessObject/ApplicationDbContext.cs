using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Enums;

namespace BusinessObject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<BatteryType> BatteryTypes { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<StationStaff> StationStaffs { get; set; }
        public DbSet<BatterySwap> BatterySwaps { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<StationInventory> StationInventories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Role).HasConversion<int>();
                entity.Property(u => u.Status).HasConversion<int>();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region BatteryType Configuration
            modelBuilder.Entity<BatteryType>(entity =>
            {
                entity.HasKey(bt => bt.BatteryTypeId);
            });
            #endregion

            #region Station Configuration
            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(s => s.StationId);
                entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(s => s.Latitude).HasPrecision(18, 6);
                entity.Property(s => s.Longitude).HasPrecision(18, 6);
            });
            #endregion

            #region Battery Configuration
            modelBuilder.Entity<Battery>(entity =>
            {
                entity.HasKey(b => b.BatteryId);
                entity.Property(b => b.Status).HasConversion<int>();
                entity.Property(b => b.Owner).HasConversion<int>();
                entity.HasIndex(b => b.SerialNo).IsUnique();
                entity.Property(b => b.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region Vehicles Configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.VehicleId);
                entity.HasIndex(v => v.LicensePlate).IsUnique();
                entity.Property(v => v.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region Booking Configuration
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.BookingId);
                entity.Property(b => b.Status).HasConversion<int>();
                entity.Property(b => b.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(b => new { b.StationId, b.BookingDate, b.TimeSlot })
                    .HasDatabaseName("IX_Booking_Station_Date_TimeSlot");
            });
            #endregion

            #region StationStaff Configuration
            modelBuilder.Entity<StationStaff>(entity =>
            {
                entity.HasKey(ss => ss.StationStaffId);
                entity.Property(ss => ss.AssignedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region BatterySwaps Configuration
            modelBuilder.Entity<BatterySwap>(entity =>
            {
                entity.HasKey(bs => bs.SwapId);
                entity.Property(bs => bs.Status).HasConversion<int>();
                entity.Property(bs => bs.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(bs => bs.SwappedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region SubscriptionPlans Configuration
            modelBuilder.Entity<SubscriptionPlan>(entity =>
            {
                entity.HasKey(sp => sp.PlanId);
                entity.Property(sp => sp.MonthlyFee).HasPrecision(18, 2);
                entity.Property(sp => sp.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region Subscriptions Configuration
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => s.SubscriptionId);
                entity.Property(s => s.Status).HasConversion<int>();
                entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region SubscriptionPayment Configuration
            modelBuilder.Entity<SubscriptionPayment>(entity =>
            {
                entity.HasKey(sp => sp.SubPayId);
                entity.Property(sp => sp.Amount).HasPrecision(18, 2);
                entity.Property(sp => sp.Status).HasConversion<int>();
                entity.Property(sp => sp.PaymentMethod).HasConversion<int>();
                entity.Property(sp => sp.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region Payment Configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.PayId);
                entity.Property(p => p.Amount).HasPrecision(18, 2);
                entity.Property(p => p.Status).HasConversion<int>();
                entity.Property(p => p.PaymentMethod).HasConversion<int>();
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region Reviews Configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.ReviewId);
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region SupportTickets Configuration
            modelBuilder.Entity<SupportTicket>(entity =>
            {
                entity.HasKey(st => st.TicketId);
                entity.Property(st => st.Priority).HasConversion<int>();
                entity.Property(st => st.Status).HasConversion<int>();
                entity.Property(st => st.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region StationInventory Configuration
            modelBuilder.Entity<StationInventory>(entity =>
            {
                entity.HasKey(si => si.StationInventoryId);
                entity.Property(si => si.LastUpdate).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            #region Reservation Configuration
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.ReservationId);
                entity.Property(r => r.Status).HasConversion<int>();
                entity.Property(r => r.ReservedAt).HasDefaultValueSql("GETUTCDATE()");
            });
            #endregion

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "admin-001",
                    FullName = "System Administrator",
                    Email = "admin@evdriver.com",
                    Phone = "0123456789",
                    Role = UserRole.Admin, // Admin
                    Status = UserStatus.Active, // Active
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed battery types
            modelBuilder.Entity<BatteryType>().HasData(
                new BatteryType { BatteryTypeId = "type-001", BatteryTypeName = "Standard Li-ion" },
                new BatteryType { BatteryTypeId = "type-002", BatteryTypeName = "High Capacity Li-ion" },
                new BatteryType { BatteryTypeId = "type-003", BatteryTypeName = "Fast Charge Li-ion" }
            );

            // Seed subscription plans
            modelBuilder.Entity<SubscriptionPlan>().HasData(
                new SubscriptionPlan
                {
                    PlanId = "plan-001",
                    Name = "Basic Plan",
                    Description = "Basic battery swap plan",
                    MonthlyFee = 199000,
                    SwapsIncluded = "10",
                    Active = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SubscriptionPlan
                {
                    PlanId = "plan-002",
                    Name = "Premium Plan",
                    Description = "Premium battery swap plan",
                    MonthlyFee = 399000,
                    SwapsIncluded = "25",
                    Active = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}