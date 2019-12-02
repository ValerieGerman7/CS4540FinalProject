using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class UpdateNotificationsKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Notifications_NotificationId",
                table: "Notifications",
                column: "NotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Notifications_NotificationId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "Notifications");
        }
    }
}
