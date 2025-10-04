using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BatteryType_battery_type_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Battery_battery_id",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "VehiclesId",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "battery_type_id",
                table: "Booking",
                newName: "BatteryTypeId");

            migrationBuilder.RenameColumn(
                name: "battery_id",
                table: "Booking",
                newName: "BatteryId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_battery_type_id",
                table: "Booking",
                newName: "IX_Booking_BatteryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_battery_id",
                table: "Booking",
                newName: "IX_Booking_BatteryId");

            migrationBuilder.AlterColumn<string>(
                name: "booking_id",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BatteryTypeId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BatteryId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "to_battery_id",
                table: "BatterySwap",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "booking_id");

            migrationBuilder.CreateTable(
                name: "StationBatterySlot",
                columns: table => new
                {
                    station_slot_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    battery_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    station_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    slot_no = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    last_updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationBatterySlot", x => x.station_slot_id);
                    table.ForeignKey(
                        name: "FK_StationBatterySlot_Battery_battery_id",
                        column: x => x.battery_id,
                        principalTable: "Battery",
                        principalColumn: "battery_id");
                    table.ForeignKey(
                        name: "FK_StationBatterySlot_Station_station_id",
                        column: x => x.station_id,
                        principalTable: "Station",
                        principalColumn: "station_id");
                });

            migrationBuilder.CreateTable(
                name: "BatteryBookingSlot",
                columns: table => new
                {
                    booking_slot_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    booking_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    battery_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    station_slot_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryBookingSlot", x => x.booking_slot_id);
                    table.ForeignKey(
                        name: "FK_BatteryBookingSlot_Battery_battery_id",
                        column: x => x.battery_id,
                        principalTable: "Battery",
                        principalColumn: "battery_id");
                    table.ForeignKey(
                        name: "FK_BatteryBookingSlot_Booking_booking_id",
                        column: x => x.booking_id,
                        principalTable: "Booking",
                        principalColumn: "booking_id");
                    table.ForeignKey(
                        name: "FK_BatteryBookingSlot_StationBatterySlot_station_slot_id",
                        column: x => x.station_slot_id,
                        principalTable: "StationBatterySlot",
                        principalColumn: "station_slot_id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Booking_station_id",
                table: "Booking",
                column: "station_id");

            migrationBuilder.CreateIndex(
                name: "IX_BatterySwap_to_battery_id",
                table: "BatterySwap",
                column: "to_battery_id");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryBookingSlot_battery_id",
                table: "BatteryBookingSlot",
                column: "battery_id");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryBookingSlot_booking_id",
                table: "BatteryBookingSlot",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryBookingSlot_station_slot_id",
                table: "BatteryBookingSlot",
                column: "station_slot_id");

            migrationBuilder.CreateIndex(
                name: "IX_StationBatterySlot_battery_id",
                table: "StationBatterySlot",
                column: "battery_id",
                unique: true,
                filter: "[battery_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StationBatterySlot_station_id",
                table: "StationBatterySlot",
                column: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Battery_to_battery_id",
                table: "BatterySwap",
                column: "to_battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BatteryType_BatteryTypeId",
                table: "Booking",
                column: "BatteryTypeId",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Battery_BatteryId",
                table: "Booking",
                column: "BatteryId",
                principalTable: "Battery",
                principalColumn: "battery_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Battery_to_battery_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BatteryType_BatteryTypeId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Battery_BatteryId",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "BatteryBookingSlot");

            migrationBuilder.DropTable(
                name: "StationBatterySlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_station_id",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_BatterySwap_to_battery_id",
                table: "BatterySwap");

            migrationBuilder.DropColumn(
                name: "to_battery_id",
                table: "BatterySwap");

            migrationBuilder.RenameColumn(
                name: "BatteryTypeId",
                table: "Booking",
                newName: "battery_type_id");

            migrationBuilder.RenameColumn(
                name: "BatteryId",
                table: "Booking",
                newName: "battery_id");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BatteryTypeId",
                table: "Booking",
                newName: "IX_Booking_battery_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BatteryId",
                table: "Booking",
                newName: "IX_Booking_battery_id");

            migrationBuilder.AlterColumn<string>(
                name: "booking_id",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "battery_type_id",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "battery_id",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehiclesId",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                columns: new[] { "station_id", "user_id", "vehicle_id", "battery_id", "battery_type_id" });

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 3, 14, 56, 57, 764, DateTimeKind.Utc).AddTicks(7217));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 3, 14, 56, 57, 764, DateTimeKind.Utc).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 3, 14, 56, 57, 764, DateTimeKind.Utc).AddTicks(6079), "$2a$11$XDP5/Wu.jsfVAN/vbvxTm.3CpjPJeyoleJAbYVXITXfKETIk5yGea" });

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BatteryType_battery_type_id",
                table: "Booking",
                column: "battery_type_id",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Battery_battery_id",
                table: "Booking",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id");
        }
    }
}
