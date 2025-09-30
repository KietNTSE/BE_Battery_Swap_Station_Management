using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.HasIndex(u => u.Email).IsUnique();

                // Configure enum conversions
                entity.Property(u => u.Role)
                    .HasConversion<int>();

                entity.Property(u => u.Status)
                    .HasConversion<int>();

                // Set default values
                entity.Property(u => u.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
            });

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
                    Role = UserRole.Admin,
                    Status = UserStatus.Active,
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}