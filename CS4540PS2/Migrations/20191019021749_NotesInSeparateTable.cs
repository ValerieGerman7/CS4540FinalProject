using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class NotesInSeparateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CourseNotes",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Note = table.Column<string>(nullable: true),
                    NoteModified = table.Column<DateTime>(nullable: true),
                    CourseInstanceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseNotes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_CourseNotes_CourseInstance_CourseInstanceId",
                        column: x => x.CourseInstanceId,
                        principalTable: "CourseInstance",
                        principalColumn: "CourseInstanceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LONotes",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Note = table.Column<string>(nullable: true),
                    NoteModified = table.Column<DateTime>(nullable: true),
                    NoteUserModifed = table.Column<string>(nullable: true),
                    Loid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LONotes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_LONotes_LearningOutcomes_Loid",
                        column: x => x.Loid,
                        principalTable: "LearningOutcomes",
                        principalColumn: "LOID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseNotes_CourseInstanceId",
                table: "CourseNotes",
                column: "CourseInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_LONotes_Loid",
                table: "LONotes",
                column: "Loid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseNotes");

            migrationBuilder.DropTable(
                name: "LONotes");

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
    }
}
