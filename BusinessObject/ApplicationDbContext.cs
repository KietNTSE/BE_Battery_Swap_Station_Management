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

            #region Station Configuration
            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(s => s.StationId);
                entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(s => s.Latitude).HasPrecision(18, 6);
                entity.Property(s => s.Longitude).HasPrecision(18, 6);
            });
            #endregion

            #region BatteryType Configuration
            modelBuilder.Entity<BatteryType>(entity =>
            {
                entity.HasKey(bt => bt.BatteryTypeId);
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

                // ✅ SỬA: Relationships với NO ACTION
                entity.HasOne(b => b.Station)
                    .WithMany(s => s.Batteries)
                    .HasForeignKey(b => b.StationId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(b => b.User)
                    .WithMany(u => u.Batteries)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(b => b.BatteryType)
                    .WithMany(bt => bt.Batteries)
                    .HasForeignKey(b => b.BatteryTypeId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
            });
            #endregion

            #region Vehicles Configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.VehicleId);
                entity.HasIndex(v => v.LicensePlate).IsUnique();
                entity.Property(v => v.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // ✅ SỬA: Relationships với NO ACTION
                entity.HasOne(v => v.User)
                    .WithMany(u => u.Vehicles)
                    .HasForeignKey(v => v.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(v => v.Battery)
                    .WithMany() // ✅ Bỏ navigation property để tránh circular reference
                    .HasForeignKey(v => v.BatteryId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(v => v.BatteryType)
                    .WithMany(bt => bt.Vehicles)
                    .HasForeignKey(v => v.BatteryTypeId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
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

                // ✅ SỬA: Relationships với NO ACTION
                entity.HasOne(b => b.User)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(b => b.Vehicle)
                    .WithMany(v => v.Bookings)
                    .HasForeignKey(b => b.VehicleId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(b => b.Station)
                    .WithMany(s => s.Bookings)
                    .HasForeignKey(b => b.StationId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(b => b.Battery)
                    .WithMany(bat => bat.Bookings)
                    .HasForeignKey(b => b.BatteryId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(b => b.BatteryType)
                    .WithMany() // ✅ Bỏ navigation property
                    .HasForeignKey(b => b.BatteryTypeId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
            });
            #endregion

            #region StationStaff Configuration
            modelBuilder.Entity<StationStaff>(entity =>
            {
                entity.HasKey(ss => ss.StationStaffId);
                entity.Property(ss => ss.AssignedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(ss => ss.User)
                    .WithMany(u => u.StationStaffs)
                    .HasForeignKey(ss => ss.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(ss => ss.Station)
                    .WithMany(s => s.StationStaffs)
                    .HasForeignKey(ss => ss.StationId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
            });
            #endregion

            #region BatterySwaps Configuration
            modelBuilder.Entity<BatterySwap>(entity =>
            {
                entity.HasKey(bs => bs.SwapId);
                entity.Property(bs => bs.Status).HasConversion<int>();
                entity.Property(bs => bs.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(bs => bs.SwappedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(bs => bs.Vehicle)
                    .WithMany(v => v.BatterySwaps)
                    .HasForeignKey(bs => bs.VehicleId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(bs => bs.User)
                    .WithMany(u => u.BatterySwaps)
                    .HasForeignKey(bs => bs.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(bs => bs.Station)
                    .WithMany() // ✅ Bỏ navigation property
                    .HasForeignKey(bs => bs.StationId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(bs => bs.Battery)
                    .WithMany(b => b.BatterySwaps)
                    .HasForeignKey(bs => bs.BatteryId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(bs => bs.StationStaff)
                    .WithMany(ss => ss.BatterySwaps)
                    .HasForeignKey(bs => bs.StationStaffId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
            });
            #endregion

            #region Subscription Related Configurations
            modelBuilder.Entity<SubscriptionPlan>(entity =>
            {
                entity.HasKey(sp => sp.PlanId);
                entity.Property(sp => sp.MonthlyFee).HasPrecision(18, 2);
                entity.Property(sp => sp.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => s.SubscriptionId);
                entity.Property(s => s.Status).HasConversion<int>();
                entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(s => s.User)
                    .WithMany(u => u.Subscriptions)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(s => s.SubscriptionPlan)
                    .WithMany(sp => sp.Subscriptions)
                    .HasForeignKey(s => s.PlanId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
            });

            // ✅ SỬA: SubscriptionPayment configuration
            modelBuilder.Entity<SubscriptionPayment>(entity =>
            {
                entity.HasKey(sp => sp.SubPayId);
                entity.Property(sp => sp.Amount).HasPrecision(18, 2);
                entity.Property(sp => sp.Status).HasConversion<int>();
                entity.Property(sp => sp.PaymentMethod).HasConversion<int>();
                entity.Property(sp => sp.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // ✅ SỬA: Explicitly specify navigation properties to avoid shadow property
                entity.HasOne(sp => sp.User)
                    .WithMany() // Không tạo navigation property ngược lại
                    .HasForeignKey(sp => sp.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(sp => sp.Subscription)
                    .WithMany(s => s.SubscriptionPayments)
                    .HasForeignKey(sp => sp.SubscriptionId)
                    .OnDelete(DeleteBehavior.NoAction);
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

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Payments)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(p => p.BatterySwap)
                    .WithMany()
                    .HasForeignKey(p => p.SwapId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
            });
            #endregion

            #region Reviews Configuration - ✅ SỬA CHÍNH Ở ĐÂY
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.ReviewId);
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // ✅ SỬA: NO ACTION thay vì Restrict để tránh cascade conflicts
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(r => r.Station)
                    .WithMany(s => s.Reviews)
                    .HasForeignKey(r => r.StationId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI
            });
            #endregion

            #region SupportTickets Configuration
            modelBuilder.Entity<SupportTicket>(entity =>
            {
                entity.HasKey(st => st.TicketId);
                entity.Property(st => st.Priority).HasConversion<int>();
                entity.Property(st => st.Status).HasConversion<int>();
                entity.Property(st => st.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(st => st.User)
                    .WithMany(u => u.SupportTickets)
                    .HasForeignKey(st => st.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // ✅ THAY ĐỔI

                entity.HasOne(st => st.Station)
                    .WithMany() // ✅ Bỏ navigation property
                    .HasForeignKey(st => st.StationId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            #endregion

            #region StationInventory and Reservation Configuration
            modelBuilder.Entity<StationInventory>(entity =>
            {
                entity.HasKey(si => si.StationInventoryId);
                entity.Property(si => si.LastUpdate).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(si => si.Station)
                    .WithMany(s => s.StationInventories)
                    .HasForeignKey(si => si.StationId)
                    .OnDelete(DeleteBehavior.Cascade); // Giữ Cascade cho relationship này
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.ReservationId);
                entity.Property(r => r.Status).HasConversion<int>();
                entity.Property(r => r.ReservedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(r => r.StationInventory)
                    .WithMany(si => si.Reservations)
                    .HasForeignKey(r => r.StationInventoryId)
                    .OnDelete(DeleteBehavior.Cascade); // Giữ Cascade cho relationship này
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

            // ✅ SỬA: Seed subscription plans với correct type
            modelBuilder.Entity<SubscriptionPlan>().HasData(
                new SubscriptionPlan
                {
                    PlanId = "plan-001",
                    Name = "Basic Plan",
                    Description = "Basic battery swap plan",
                    MonthlyFee = 199000,
                    SwapsIncluded = "10", // int không phải string
                    Active = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SubscriptionPlan
                {
                    PlanId = "plan-002",
                    Name = "Premium Plan",
                    Description = "Premium battery swap plan",
                    MonthlyFee = 399000,
                    SwapsIncluded = "25", // int không phải string
                    Active = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}