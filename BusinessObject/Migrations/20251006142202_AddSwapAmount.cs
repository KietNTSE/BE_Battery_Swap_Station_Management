using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddSwapAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "swap_amount",
                table: "SubscriptionPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "serial_no",
                table: "Battery",
                type: "int",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                columns: new[] { "created_at", "swap_amount" },
                values: new object[] { new DateTime(2025, 10, 6, 14, 22, 2, 221, DateTimeKind.Utc).AddTicks(7479), 0 });

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                columns: new[] { "created_at", "swap_amount" },
                values: new object[] { new DateTime(2025, 10, 6, 14, 22, 2, 221, DateTimeKind.Utc).AddTicks(7483), 0 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 6, 14, 22, 2, 221, DateTimeKind.Utc).AddTicks(6542), "$2a$11$TC0F9RUk7Z9MZpLfTpr4S.hhI.nkhE4f5MU6gepAXSOs08PtFDMOe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "swap_amount",
                table: "SubscriptionPlan");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "serial_no",
                table: "Battery",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 255);

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
    }
}
