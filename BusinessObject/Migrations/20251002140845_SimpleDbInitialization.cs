using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class SimpleDbInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Vehicle_VehiclesId",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_VehiclesId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "VehiclesId",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Booking",
                newName: "booking_date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Vehicle",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SupportTicket",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "monthly_fee",
                table: "SubscriptionPlan",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPlan",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPayment",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "SubscriptionPayment",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "assigned_at",
                table: "StationStaff",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                table: "StationInventory",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                table: "Station",
                type: "float(18)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                table: "Station",
                type: "float(18)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "reserved_at",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "Payment",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "booking_id",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "complete_at",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "confirm_by",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time_slot",
                table: "Booking",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "swapped_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Battery",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "booking_id");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$QFJZHCsua177Hhi76GwgSu9WqnSmKjqrK5JB9ouoe/npSGg5d3ogO" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Station_Date_TimeSlot",
                table: "Booking",
                columns: new[] { "station_id", "booking_date", "time_slot" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_vehicle_id",
                table: "Booking",
                column: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Vehicle_vehicle_id",
                table: "Booking",
                column: "vehicle_id",
                principalTable: "Vehicle",
                principalColumn: "vehicles_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Vehicle_vehicle_id",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_Station_Date_TimeSlot",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_vehicle_id",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "complete_at",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "confirm_by",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "time_slot",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "booking_date",
                table: "Booking",
                newName: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Vehicle",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SupportTicket",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "monthly_fee",
                table: "SubscriptionPlan",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPlan",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPayment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "SubscriptionPayment",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "assigned_at",
                table: "StationStaff",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                table: "StationInventory",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                table: "Station",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                table: "Station",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Review",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "reserved_at",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "Payment",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "booking_id",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "VehiclesId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "swapped_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Battery",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                columns: new[] { "station_id", "user_id", "vehicle_id", "battery_id", "battery_type_id" });

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 1, 9, 10, 54, 245, DateTimeKind.Utc).AddTicks(2381));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 1, 9, 10, 54, 245, DateTimeKind.Utc).AddTicks(2409));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 1, 9, 10, 54, 245, DateTimeKind.Utc).AddTicks(1374), "$2a$11$SymoTl0N6SBGODyfgObM/uuZfEiYKftoeeEhUKbp6i3HTD/9Jp8rS" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_VehiclesId",
                table: "Booking",
                column: "VehiclesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Vehicle_VehiclesId",
                table: "Booking",
                column: "VehiclesId",
                principalTable: "Vehicle",
                principalColumn: "vehicles_id");
        }
    }
}
