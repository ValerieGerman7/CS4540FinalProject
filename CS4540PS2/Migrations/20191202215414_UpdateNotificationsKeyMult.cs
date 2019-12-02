using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class UpdateNotificationsKeyMult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Notifications_NotificationId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Notifica__1788CCAC810E6D4F",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationId",
                table: "Notifications",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Notifica__1788CCAC810E6D4F",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Notifica__1788CCAC810E6D4F",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationId",
                table: "Notifications",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Notifications_NotificationId",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Notifica__1788CCAC810E6D4F",
                table: "Notifications",
                column: "UserID");
        }
    }
}
