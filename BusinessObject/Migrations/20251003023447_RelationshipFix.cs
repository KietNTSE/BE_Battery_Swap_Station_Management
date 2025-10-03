using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_User_UserId1",
                table: "SubscriptionPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Station_StationId1",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_User_UserId1",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_BatteryId1",
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
                value: new DateTime(2025, 10, 3, 2, 34, 47, 175, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 3, 2, 34, 47, 175, DateTimeKind.Utc).AddTicks(2543));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 3, 2, 34, 47, 175, DateTimeKind.Utc).AddTicks(1881), "$2a$11$gToH27KJYlNhSHjB2IV5d.m5cO/nlIc7Ze5JdkblNnTflzmYCG7kK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                value: new DateTime(2025, 10, 3, 2, 16, 49, 473, DateTimeKind.Utc).AddTicks(4734));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 3, 2, 16, 49, 473, DateTimeKind.Utc).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 3, 2, 16, 49, 473, DateTimeKind.Utc).AddTicks(3682), "$2a$11$FvCh68kbQL38AVubevWwQuYVPBwLXKqi5zEUlX2O6zS7QVLPMXgjq" });

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
                name: "FK_SubscriptionPayment_User_UserId1",
                table: "SubscriptionPayment",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Station_StationId1",
                table: "SupportTicket",
                column: "StationId1",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_User_UserId1",
                table: "SupportTicket",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_BatteryId1",
                table: "Vehicle",
                column: "BatteryId1",
                principalTable: "Battery",
                principalColumn: "battery_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
