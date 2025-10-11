using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddBatterySwapRelationshipToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "swap_id",
                table: "Review",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 11, 11, 51, 36, 445, DateTimeKind.Utc).AddTicks(6628));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 11, 11, 51, 36, 445, DateTimeKind.Utc).AddTicks(6631));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 11, 11, 51, 36, 445, DateTimeKind.Utc).AddTicks(5449), "$2a$11$5X1KdWMRXltyK4MTVgSKWuHcWmlvGuLfF5avI0WcBaAvCF/2nKKZ6" });

            migrationBuilder.CreateIndex(
                name: "IX_Review_swap_id",
                table: "Review",
                column: "swap_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_BatterySwap_swap_id",
                table: "Review",
                column: "swap_id",
                principalTable: "BatterySwap",
                principalColumn: "swap_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_BatterySwap_swap_id",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_swap_id",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "swap_id",
                table: "Review");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 9, 3, 52, 54, 735, DateTimeKind.Utc).AddTicks(6193));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 9, 3, 52, 54, 735, DateTimeKind.Utc).AddTicks(6197));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 52, 54, 735, DateTimeKind.Utc).AddTicks(5247), "$2a$11$jZzaYcWk2PV3Jjpz31QC/.KxCbm1hLJvGMlXP6m/Xo4KyODhb0epa" });
        }
    }
}
