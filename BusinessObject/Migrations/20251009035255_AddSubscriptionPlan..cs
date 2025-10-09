using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_User_user_id",
                table: "SubscriptionPayment");

            migrationBuilder.AddColumn<string>(
                name: "plan_id",
                table: "Subscription",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlan",
                columns: table => new
                {
                    plan_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    monthly_fee = table.Column<double>(type: "float", nullable: false),
                    swaps_included = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    swap_amount = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.plan_id);
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlan",
                columns: new[] { "plan_id", "active", "created_at", "description", "monthly_fee", "name", "swap_amount", "swaps_included" },
                values: new object[,]
                {
                    { "plan-001", true, new DateTime(2025, 10, 9, 3, 52, 54, 735, DateTimeKind.Utc).AddTicks(6193), "Basic battery swap plan", 199000.0, "Basic Plan", 0, "10" },
                    { "plan-002", true, new DateTime(2025, 10, 9, 3, 52, 54, 735, DateTimeKind.Utc).AddTicks(6197), "Premium battery swap plan", 399000.0, "Premium Plan", 0, "25" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 52, 54, 735, DateTimeKind.Utc).AddTicks(5247), "$2a$11$jZzaYcWk2PV3Jjpz31QC/.KxCbm1hLJvGMlXP6m/Xo4KyODhb0epa" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_plan_id",
                table: "Subscription",
                column: "plan_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_SubscriptionPlan_plan_id",
                table: "Subscription",
                column: "plan_id",
                principalTable: "SubscriptionPlan",
                principalColumn: "plan_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_SubscriptionPlan_plan_id",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_User_user_id",
                table: "SubscriptionPayment");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_plan_id",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "plan_id",
                table: "Subscription");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 22, 22, 800, DateTimeKind.Utc).AddTicks(5671), "$2a$11$Z0b85qwvxtD7Fxyz1zl4TuvRjGjGC3J8j.gvnaeIhICNO647jsxWa" });
        }
    }
}
