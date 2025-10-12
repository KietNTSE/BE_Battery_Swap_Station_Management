using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar_url",
                table: "User",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PasswordResetToken",
                columns: table => new
                {
                    reset_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    otp_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    consumed = table.Column<bool>(type: "bit", nullable: false),
                    attempt_count = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetToken", x => x.reset_id);
                    table.ForeignKey(
                        name: "FK_PasswordResetToken_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2025, 10, 12, 13, 26, 17, 919, DateTimeKind.Utc).AddTicks(7138));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 12, 13, 26, 17, 919, DateTimeKind.Utc).AddTicks(7141));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "avatar_url", "created_at", "password" },
                values: new object[] { null, new DateTime(2025, 10, 12, 13, 26, 17, 919, DateTimeKind.Utc).AddTicks(6290), "$2a$11$FqGipGgJP6iQ1IO916t4f.xWTrGbQV1n9UK80Hp3MjMiKEkLYs0FS" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetToken_email_created_at",
                table: "PasswordResetToken",
                columns: new[] { "email", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetToken_user_id",
                table: "PasswordResetToken",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResetToken");

            migrationBuilder.DropColumn(
                name: "avatar_url",
                table: "User");

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
        }
    }
}
