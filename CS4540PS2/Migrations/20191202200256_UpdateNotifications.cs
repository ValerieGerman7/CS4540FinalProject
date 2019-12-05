using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class UpdateNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseInstanceId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Notifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CourseInstanceId",
                table: "Notifications",
                column: "CourseInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CourseInstance_CourseInstanceId",
                table: "Notifications",
                column: "CourseInstanceId",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CourseInstance_CourseInstanceId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CourseInstanceId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CourseInstanceId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Read",
                table: "Notifications");
        }
    }
}
