using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaxSlotStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maximum_slot",
                table: "Station");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 5, 8, 54, 29, 238, DateTimeKind.Utc).AddTicks(4831));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 5, 8, 54, 29, 238, DateTimeKind.Utc).AddTicks(4855));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 5, 8, 54, 29, 238, DateTimeKind.Utc).AddTicks(3861), "$2a$11$BE.C08cdY5Ptt5nCnnD2MOE0FQSQodqmicVb.UwFe.xv8hcae7ZpC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "maximum_slot",
                table: "Station",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
