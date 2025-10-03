using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class BookingDemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Station_station_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_User_user_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Station_station_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_user_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_User_user_id",
                table: "Station");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_User_user_id",
                table: "SubscriptionPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Station_station_id",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_User_user_id",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle");

            migrationBuilder.AddColumn<string>(
                name: "BatteryId1",
                table: "Vehicle",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StationId1",
                table: "SupportTicket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "SupportTicket",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "SubscriptionPayment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 3, 1, 36, 4, 396, DateTimeKind.Utc).AddTicks(9709));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 3, 1, 36, 4, 396, DateTimeKind.Utc).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 3, 1, 36, 4, 396, DateTimeKind.Utc).AddTicks(8015), "$2a$11$h1ZUglnLVr3cLVVn5RdFeOGqHtLFBxs0G0idOgufh6WGSEFEp1tAi" });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_BatteryId1",
                table: "Vehicle",
                column: "BatteryId1");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_StationId1",
                table: "SupportTicket",
                column: "StationId1");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_UserId1",
                table: "SupportTicket",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayment_UserId1",
                table: "SubscriptionPayment",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Station_station_id",
                table: "BatterySwap",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_User_user_id",
                table: "BatterySwap",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Station_station_id",
                table: "Review",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_user_id",
                table: "Review",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_User_user_id",
                table: "Station",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPayment_User_UserId1",
                table: "SubscriptionPayment",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPayment_User_user_id",
                table: "SubscriptionPayment",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Station_StationId1",
                table: "SupportTicket",
                column: "StationId1",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Station_station_id",
                table: "SupportTicket",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_User_UserId1",
                table: "SupportTicket",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_User_user_id",
                table: "SupportTicket",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_BatteryId1",
                table: "Vehicle",
                column: "BatteryId1",
                principalTable: "Battery",
                principalColumn: "battery_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Station_station_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_User_user_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Station_station_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_user_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_User_user_id",
                table: "Station");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_User_UserId1",
                table: "SubscriptionPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_User_user_id",
                table: "SubscriptionPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Station_StationId1",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Station_station_id",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_User_UserId1",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_User_user_id",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_BatteryId1",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_BatteryId1",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_SupportTicket_StationId1",
                table: "SupportTicket");

            migrationBuilder.DropIndex(
                name: "IX_SupportTicket_UserId1",
                table: "SupportTicket");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayment_UserId1",
                table: "SubscriptionPayment");

            migrationBuilder.DropColumn(
                name: "BatteryId1",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "StationId1",
                table: "SupportTicket");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SupportTicket");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SubscriptionPayment");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Station_station_id",
                table: "BatterySwap",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_User_user_id",
                table: "BatterySwap",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Station_station_id",
                table: "Review",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_user_id",
                table: "Review",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_User_user_id",
                table: "Station",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPayment_User_user_id",
                table: "SubscriptionPayment",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Station_station_id",
                table: "SupportTicket",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_User_user_id",
                table: "SupportTicket",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
