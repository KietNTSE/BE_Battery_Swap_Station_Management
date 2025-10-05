using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicleField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle");
            
            migrationBuilder.DropIndex(
                name: "IX_Vehicle_battery_id",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "battery_id",
                table: "Vehicle"
            );

            migrationBuilder.AddColumn<int>(
                name: "battery_capacity",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "vehicle_id",
                table: "Battery",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 5, 7, 46, 8, 948, DateTimeKind.Utc).AddTicks(8990));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 5, 7, 46, 8, 948, DateTimeKind.Utc).AddTicks(9014));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 5, 7, 46, 8, 948, DateTimeKind.Utc).AddTicks(8021), "$2a$11$B0.QWB7azsFaf/YElJ1BfeKpz2vOpTv1ve6crw3Y0R2zOGVup21SK" });

            migrationBuilder.CreateIndex(
                name: "IX_Battery_vehicle_id",
                table: "Battery",
                column: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battery_Vehicle_vehicle_id",
                table: "Battery",
                column: "vehicle_id",
                principalTable: "Vehicle",
                principalColumn: "vehicles_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battery_Vehicle_vehicle_id",
                table: "Battery");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_BatteryId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Battery_vehicle_id",
                table: "Battery");

            migrationBuilder.DropColumn(
                name: "battery_capacity",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "vehicle_id",
                table: "Battery");

            migrationBuilder.AddColumn<string>(
                name: "battery_id",
                table: "Vehicle",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_battery_id",
                table: "Vehicle",
                column: "battery_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 4, 18, 12, 47, 841, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 4, 18, 12, 47, 841, DateTimeKind.Utc).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 4, 18, 12, 47, 841, DateTimeKind.Utc).AddTicks(5110), "$2a$11$eSoh6.yCQk4oMmloOjF.Ne780.x2i4gT4YrufiJdNVBF1lOrMHMP6" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id");
        }
    }
}
