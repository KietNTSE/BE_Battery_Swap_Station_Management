using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffAbsenceAndStationStaffOverride : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffAbsense",
                columns: table => new
                {
                    staff_absence_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAbsense", x => x.staff_absence_id);
                    table.ForeignKey(
                        name: "FK_StaffAbsense_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StationStaffOverride",
                columns: table => new
                {
                    station_staff_override_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    station_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    shift_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationStaffOverride", x => x.station_staff_override_id);
                    table.ForeignKey(
                        name: "FK_StationStaffOverride_Station_station_id",
                        column: x => x.station_id,
                        principalTable: "Station",
                        principalColumn: "station_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StationStaffOverride_User_user_id",
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
                value: new DateTime(2025, 10, 13, 15, 30, 16, 462, DateTimeKind.Utc).AddTicks(6732));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2025, 10, 13, 15, 30, 16, 462, DateTimeKind.Utc).AddTicks(6736));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 13, 15, 30, 16, 462, DateTimeKind.Utc).AddTicks(5861), "$2a$11$z9z0hL9WrBcAmcaKJ7W8POeNLKFrc/JKlDYks00wS3r.26uWqxvE2" });

            migrationBuilder.CreateIndex(
                name: "IX_StaffAbsense_date",
                table: "StaffAbsense",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAbsense_user_id_date",
                table: "StaffAbsense",
                columns: new[] { "user_id", "date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StationStaffOverride_station_id_date",
                table: "StationStaffOverride",
                columns: new[] { "station_id", "date" });

            migrationBuilder.CreateIndex(
                name: "IX_StationStaffOverride_user_id_date",
                table: "StationStaffOverride",
                columns: new[] { "user_id", "date" },
                unique: true,
                filter: "[shift_id] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StationStaffOverride_user_id_date_shift_id",
                table: "StationStaffOverride",
                columns: new[] { "user_id", "date", "shift_id" },
                unique: true,
                filter: "[shift_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffAbsense");

            migrationBuilder.DropTable(
                name: "StationStaffOverride");

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
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2025, 10, 12, 13, 26, 17, 919, DateTimeKind.Utc).AddTicks(6290), "$2a$11$FqGipGgJP6iQ1IO916t4f.xWTrGbQV1n9UK80Hp3MjMiKEkLYs0FS" });
        }
    }
}
