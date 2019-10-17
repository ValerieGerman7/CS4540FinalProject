using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class NotesModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "LearningOutcomes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NoteModified",
                table: "LearningOutcomes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteUserModifed",
                table: "LearningOutcomes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CourseInstance",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NoteModified",
                table: "CourseInstance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "LearningOutcomes");

            migrationBuilder.DropColumn(
                name: "NoteModified",
                table: "LearningOutcomes");

            migrationBuilder.DropColumn(
                name: "NoteUserModifed",
                table: "LearningOutcomes");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "CourseInstance");

            migrationBuilder.DropColumn(
                name: "NoteModified",
                table: "CourseInstance");
        }
    }
}
