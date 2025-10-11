using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubscriptionPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SubscriptionPayment_User_user_id",
            //    table: "SubscriptionPayment");

            //migrationBuilder.DropIndex(
            //    name: "IX_SubscriptionPayment_user_id",
            //    table: "SubscriptionPayment");

            //migrationBuilder.DropColumn(
            //    name: "user_id",
            //    table: "SubscriptionPayment");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 8, 14, 40, 48, 901, DateTimeKind.Utc).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 8, 14, 40, 48, 901, DateTimeKind.Utc).AddTicks(7361));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 8, 14, 40, 48, 901, DateTimeKind.Utc).AddTicks(6556), "$2a$11$bFyYfwKA/El8vfOEFRGWPu5Ea8dHAWlbWK2LAcI8EyFbzY.goOfMe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "SubscriptionPayment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 8, 5, 59, 49, 670, DateTimeKind.Utc).AddTicks(535));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 8, 5, 59, 49, 670, DateTimeKind.Utc).AddTicks(539));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 8, 5, 59, 49, 669, DateTimeKind.Utc).AddTicks(9684), "$2a$11$Zh8rCER9Cp5EAqF.QT.0cOuyUmcCVUuYObujbgyzNu0wGZhWno4VK" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_SubscriptionPayment_user_id",
            //    table: "SubscriptionPayment",
            //    column: "user_id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SubscriptionPayment_User_user_id",
            //    table: "SubscriptionPayment",
            //    column: "user_id",
            //    principalTable: "User",
            //    principalColumn: "user_id");
        }
    }
}
