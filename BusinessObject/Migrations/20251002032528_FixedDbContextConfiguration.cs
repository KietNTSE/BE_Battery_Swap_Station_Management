using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class FixedDbContextConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battery_BatteryType_battery_type_id",
                table: "Battery");

            migrationBuilder.DropForeignKey(
                name: "FK_Battery_Station_station_id",
                table: "Battery");

            migrationBuilder.DropForeignKey(
                name: "FK_Battery_User_user_id",
                table: "Battery");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Battery_battery_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_StationStaff_station_staff_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Station_station_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_User_user_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Vehicle_vehicle_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BatteryType_battery_type_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Battery_battery_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Station_station_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_user_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Vehicle_VehiclesId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_BatterySwap_swap_id",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_user_id",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Station_station_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_user_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_StationStaff_Station_station_id",
                table: "StationStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_StationStaff_User_user_id",
                table: "StationStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_SubscriptionPlan_plan_id",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_User_user_id",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_Subscription_subscription_id",
                table: "SubscriptionPayment");

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
                name: "FK_Vehicle_BatteryType_battery_type_id",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_User_user_id",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Booking",
                newName: "booking_date");

            migrationBuilder.RenameColumn(
                name: "VehiclesId",
                table: "Booking",
                newName: "BatteryTypeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_VehiclesId",
                table: "Booking",
                newName: "IX_Booking_BatteryTypeId1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Vehicle",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "BatteryId1",
                table: "Vehicle",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SupportTicket",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "StationId1",
                table: "SupportTicket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "monthly_fee",
                table: "SubscriptionPlan",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPlan",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPayment",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "SubscriptionPayment",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "SubscriptionPayment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "assigned_at",
                table: "StationStaff",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                table: "StationInventory",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                table: "Station",
                type: "float(18)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                table: "Station",
                type: "float(18)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "reserved_at",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "Payment",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "BatterySwapSwapId",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "booking_id",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "complete_at",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "confirm_by",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time_slot",
                table: "Booking",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "swapped_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "StationId1",
                table: "BatterySwap",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Battery",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "booking_id");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-001",
                column: "created_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlan",
                keyColumn: "plan_id",
                keyValue: "plan-002",
                column: "created_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "user_id",
                keyValue: "admin-001",
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$d/sRJKd6IlnlAljPYrDA0euQwkwTs9SXKdYnEnHvA16WXpNsmGM4K" });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_BatteryId1",
                table: "Vehicle",
                column: "BatteryId1");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_StationId1",
                table: "SupportTicket",
                column: "StationId1");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayment_UserId1",
                table: "SubscriptionPayment",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BatterySwapSwapId",
                table: "Payment",
                column: "BatterySwapSwapId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Station_Date_TimeSlot",
                table: "Booking",
                columns: new[] { "station_id", "booking_date", "time_slot" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_vehicle_id",
                table: "Booking",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_BatterySwap_StationId1",
                table: "BatterySwap",
                column: "StationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Battery_BatteryType_battery_type_id",
                table: "Battery",
                column: "battery_type_id",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battery_Station_station_id",
                table: "Battery",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battery_User_user_id",
                table: "Battery",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Battery_battery_id",
                table: "BatterySwap",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_StationStaff_station_staff_id",
                table: "BatterySwap",
                column: "station_staff_id",
                principalTable: "StationStaff",
                principalColumn: "station_staff_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Station_StationId1",
                table: "BatterySwap",
                column: "StationId1",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Station_station_id",
                table: "BatterySwap",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_User_user_id",
                table: "BatterySwap",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Vehicle_vehicle_id",
                table: "BatterySwap",
                column: "vehicle_id",
                principalTable: "Vehicle",
                principalColumn: "vehicles_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BatteryType_BatteryTypeId1",
                table: "Booking",
                column: "BatteryTypeId1",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Station_station_id",
                table: "Booking",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_user_id",
                table: "Booking",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Vehicle_vehicle_id",
                table: "Booking",
                column: "vehicle_id",
                principalTable: "Vehicle",
                principalColumn: "vehicles_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_BatterySwap_BatterySwapSwapId",
                table: "Payment",
                column: "BatterySwapSwapId",
                principalTable: "BatterySwap",
                principalColumn: "swap_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_BatterySwap_swap_id",
                table: "Payment",
                column: "swap_id",
                principalTable: "BatterySwap",
                principalColumn: "swap_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_user_id",
                table: "Payment",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Station_station_id",
                table: "Review",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_user_id",
                table: "Review",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StationStaff_Station_station_id",
                table: "StationStaff",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StationStaff_User_user_id",
                table: "StationStaff",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_SubscriptionPlan_plan_id",
                table: "Subscription",
                column: "plan_id",
                principalTable: "SubscriptionPlan",
                principalColumn: "plan_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_user_id",
                table: "Subscription",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPayment_Subscription_subscription_id",
                table: "SubscriptionPayment",
                column: "subscription_id",
                principalTable: "Subscription",
                principalColumn: "subscription_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPayment_User_UserId1",
                table: "SubscriptionPayment",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPayment_User_user_id",
                table: "SubscriptionPayment",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

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
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_User_user_id",
                table: "SupportTicket",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_BatteryType_battery_type_id",
                table: "Vehicle",
                column: "battery_type_id",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_BatteryId1",
                table: "Vehicle",
                column: "BatteryId1",
                principalTable: "Battery",
                principalColumn: "battery_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_User_user_id",
                table: "Vehicle",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battery_BatteryType_battery_type_id",
                table: "Battery");

            migrationBuilder.DropForeignKey(
                name: "FK_Battery_Station_station_id",
                table: "Battery");

            migrationBuilder.DropForeignKey(
                name: "FK_Battery_User_user_id",
                table: "Battery");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Battery_battery_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_StationStaff_station_staff_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Station_StationId1",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Station_station_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_User_user_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_BatterySwap_Vehicle_vehicle_id",
                table: "BatterySwap");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BatteryType_BatteryTypeId1",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BatteryType_battery_type_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Battery_battery_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Station_station_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_user_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Vehicle_vehicle_id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_BatterySwap_BatterySwapSwapId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_BatterySwap_swap_id",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_user_id",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Station_station_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_user_id",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_StationStaff_Station_station_id",
                table: "StationStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_StationStaff_User_user_id",
                table: "StationStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_SubscriptionPlan_plan_id",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_User_user_id",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPayment_Subscription_subscription_id",
                table: "SubscriptionPayment");

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
                name: "FK_SupportTicket_User_user_id",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_BatteryType_battery_type_id",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_BatteryId1",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_User_user_id",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_BatteryId1",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_SupportTicket_StationId1",
                table: "SupportTicket");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayment_UserId1",
                table: "SubscriptionPayment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BatterySwapSwapId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_Station_Date_TimeSlot",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_vehicle_id",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_BatterySwap_StationId1",
                table: "BatterySwap");

            migrationBuilder.DropColumn(
                name: "BatteryId1",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "StationId1",
                table: "SupportTicket");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SubscriptionPayment");

            migrationBuilder.DropColumn(
                name: "BatterySwapSwapId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "complete_at",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "confirm_by",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "time_slot",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "StationId1",
                table: "BatterySwap");

            migrationBuilder.RenameColumn(
                name: "booking_date",
                table: "Booking",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "BatteryTypeId1",
                table: "Booking",
                newName: "VehiclesId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BatteryTypeId1",
                table: "Booking",
                newName: "IX_Booking_VehiclesId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Vehicle",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SupportTicket",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "monthly_fee",
                table: "SubscriptionPlan",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPlan",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "SubscriptionPayment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "SubscriptionPayment",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "assigned_at",
                table: "StationStaff",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                table: "StationInventory",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                table: "Station",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                table: "Station",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Review",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "reserved_at",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "Payment",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "booking_id",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "swapped_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "BatterySwap",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Battery",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                columns: new[] { "station_id", "user_id", "vehicle_id", "battery_id", "battery_type_id" });

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
                name: "FK_Battery_BatteryType_battery_type_id",
                table: "Battery",
                column: "battery_type_id",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Battery_Station_station_id",
                table: "Battery",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Battery_User_user_id",
                table: "Battery",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_Battery_battery_id",
                table: "BatterySwap",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatterySwap_StationStaff_station_staff_id",
                table: "BatterySwap",
                column: "station_staff_id",
                principalTable: "StationStaff",
                principalColumn: "station_staff_id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_BatterySwap_Vehicle_vehicle_id",
                table: "BatterySwap",
                column: "vehicle_id",
                principalTable: "Vehicle",
                principalColumn: "vehicles_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BatteryType_battery_type_id",
                table: "Booking",
                column: "battery_type_id",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Battery_battery_id",
                table: "Booking",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Station_station_id",
                table: "Booking",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_user_id",
                table: "Booking",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Vehicle_VehiclesId",
                table: "Booking",
                column: "VehiclesId",
                principalTable: "Vehicle",
                principalColumn: "vehicles_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_BatterySwap_swap_id",
                table: "Payment",
                column: "swap_id",
                principalTable: "BatterySwap",
                principalColumn: "swap_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_user_id",
                table: "Payment",
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
                name: "FK_StationStaff_Station_station_id",
                table: "StationStaff",
                column: "station_id",
                principalTable: "Station",
                principalColumn: "station_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationStaff_User_user_id",
                table: "StationStaff",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_SubscriptionPlan_plan_id",
                table: "Subscription",
                column: "plan_id",
                principalTable: "SubscriptionPlan",
                principalColumn: "plan_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_user_id",
                table: "Subscription",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPayment_Subscription_subscription_id",
                table: "SubscriptionPayment",
                column: "subscription_id",
                principalTable: "Subscription",
                principalColumn: "subscription_id",
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
                name: "FK_Vehicle_BatteryType_battery_type_id",
                table: "Vehicle",
                column: "battery_type_id",
                principalTable: "BatteryType",
                principalColumn: "battery_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Battery_battery_id",
                table: "Vehicle",
                column: "battery_id",
                principalTable: "Battery",
                principalColumn: "battery_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_User_user_id",
                table: "Vehicle",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
