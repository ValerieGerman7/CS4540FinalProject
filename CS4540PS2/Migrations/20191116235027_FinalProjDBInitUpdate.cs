using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class FinalProjDBInitUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseNotes_CourseInstance_CourseInstanceId",
                table: "CourseNotes");

            migrationBuilder.DropForeignKey(
                name: "FK__Evaluation__LOID__29572725",
                table: "EvaluationMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK__Instructo__Cours__412EB0B6",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK__LearningO__Cours__267ABA7A",
                table: "LearningOutcomes");

            migrationBuilder.DropForeignKey(
                name: "FK_LONotes_LearningOutcomes_Loid",
                table: "LONotes");

            migrationBuilder.DropForeignKey(
                name: "FK__SampleFile__EMID__2C3393D0",
                table: "SampleFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SampleFi__CA195970D4F455B9",
                table: "SampleFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LONotes",
                table: "LONotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Learning__76F319DDBAB9F1A4",
                table: "LearningOutcomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Instruct__8D7A08C6F8CFADF9",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "UQ__Instruct__9386E1D4FC8546A5",
                table: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Evaluati__258EC8E02063D158",
                table: "EvaluationMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseNotes",
                table: "CourseNotes");

            migrationBuilder.DropColumn(
                name: "NoteUserModifed",
                table: "LONotes");

            migrationBuilder.DropColumn(
                name: "InstructorLoginEmail",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "InstructorTitle",
                table: "Instructors");

            migrationBuilder.RenameColumn(
                name: "Loid",
                table: "LONotes",
                newName: "LOID");

            migrationBuilder.RenameIndex(
                name: "IX_LONotes_Loid",
                table: "LONotes",
                newName: "IX_LONotes_LOID");

            migrationBuilder.RenameColumn(
                name: "CourseInstanceId",
                table: "CourseNotes",
                newName: "CourseInstanceID");

            migrationBuilder.RenameIndex(
                name: "IX_CourseNotes_CourseInstanceId",
                table: "CourseNotes",
                newName: "IX_CourseNotes_CourseInstanceID");

            migrationBuilder.RenameIndex(
                name: "UQ__CourseIn__EA334D09ED3CADE0",
                table: "CourseInstance",
                newName: "UQ__CourseIn__EA334D090513E077");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NoteModified",
                table: "LONotes",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LOID",
                table: "LONotes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteUserModified",
                table: "LONotes",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Instructors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NoteModified",
                table: "CourseNotes",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseInstanceID",
                table: "CourseNotes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "CourseInstance",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "CourseInstance",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__SampleFi__CA19597019002ED2",
                table: "SampleFiles",
                column: "SID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__LONotes__EACE355F806B76E7",
                table: "LONotes",
                column: "NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Learning__76F319DDE8C12190",
                table: "LearningOutcomes",
                column: "LOID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Instruct__8D7A08C617B65975",
                table: "Instructors",
                column: "IKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Evaluati__258EC8E09224BB94",
                table: "EvaluationMetrics",
                column: "EMID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CourseNo__EACE355FFDB79055",
                table: "CourseNotes",
                column: "NoteId");

            migrationBuilder.CreateTable(
                name: "CourseStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseSt__C8EE204329C5561A", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 5, nullable: false),
                    Name = table.Column<string>(maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__A25C5AA69D8CA09E", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "UserLocator",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserLoginEmail = table.Column<string>(maxLength: 450, nullable: false),
                    UserTitle = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLocator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    Sender = table.Column<int>(nullable: false),
                    Receiver = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Messages__Receiv__44FF419A",
                        column: x => x.Receiver,
                        principalTable: "UserLocator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Messages__Sender__440B1D61",
                        column: x => x.Sender,
                        principalTable: "UserLocator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    DateNotified = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__1788CCAC810E6D4F", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__412EB0B6",
                        column: x => x.UserID,
                        principalTable: "UserLocator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_UserID",
                table: "Instructors",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Instruct__E0CD20A3F46E21E2",
                table: "Instructors",
                columns: new[] { "CourseInstanceID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstance_StatusID",
                table: "CourseInstance",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Receiver",
                table: "Messages",
                column: "Receiver");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Sender",
                table: "Messages",
                column: "Sender");

            migrationBuilder.AddForeignKey(
                name: "FK__CourseIns__Depar__2B3F6F97",
                table: "CourseInstance",
                column: "Department",
                principalTable: "Departments",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseIns__Statu__2A4B4B5E",
                table: "CourseInstance",
                column: "StatusID",
                principalTable: "CourseStatus",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseNot__Cours__3B75D760",
                table: "CourseNotes",
                column: "CourseInstanceID",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Evaluation__LOID__30F848ED",
                table: "EvaluationMetrics",
                column: "LOID",
                principalTable: "LearningOutcomes",
                principalColumn: "LOID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Instructo__Cours__37A5467C",
                table: "Instructors",
                column: "CourseInstanceID",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Instructo__UserI__38996AB5",
                table: "Instructors",
                column: "UserID",
                principalTable: "UserLocator",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__LearningO__Cours__2E1BDC42",
                table: "LearningOutcomes",
                column: "CourseInstanceID",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__LONotes__LOID__3E52440B",
                table: "LONotes",
                column: "LOID",
                principalTable: "LearningOutcomes",
                principalColumn: "LOID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__SampleFile__EMID__33D4B598",
                table: "SampleFiles",
                column: "EMID",
                principalTable: "EvaluationMetrics",
                principalColumn: "EMID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CourseIns__Depar__2B3F6F97",
                table: "CourseInstance");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseIns__Statu__2A4B4B5E",
                table: "CourseInstance");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseNot__Cours__3B75D760",
                table: "CourseNotes");

            migrationBuilder.DropForeignKey(
                name: "FK__Evaluation__LOID__30F848ED",
                table: "EvaluationMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK__Instructo__Cours__37A5467C",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK__Instructo__UserI__38996AB5",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK__LearningO__Cours__2E1BDC42",
                table: "LearningOutcomes");

            migrationBuilder.DropForeignKey(
                name: "FK__LONotes__LOID__3E52440B",
                table: "LONotes");

            migrationBuilder.DropForeignKey(
                name: "FK__SampleFile__EMID__33D4B598",
                table: "SampleFiles");

            migrationBuilder.DropTable(
                name: "CourseStatus");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "UserLocator");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SampleFi__CA19597019002ED2",
                table: "SampleFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__LONotes__EACE355F806B76E7",
                table: "LONotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Learning__76F319DDE8C12190",
                table: "LearningOutcomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Instruct__8D7A08C617B65975",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_UserID",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "UQ__Instruct__E0CD20A3F46E21E2",
                table: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Evaluati__258EC8E09224BB94",
                table: "EvaluationMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CourseNo__EACE355FFDB79055",
                table: "CourseNotes");

            migrationBuilder.DropIndex(
                name: "IX_CourseInstance_StatusID",
                table: "CourseInstance");

            migrationBuilder.DropColumn(
                name: "NoteUserModified",
                table: "LONotes");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "CourseInstance");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "CourseInstance");

            migrationBuilder.RenameColumn(
                name: "LOID",
                table: "LONotes",
                newName: "Loid");

            migrationBuilder.RenameIndex(
                name: "IX_LONotes_LOID",
                table: "LONotes",
                newName: "IX_LONotes_Loid");

            migrationBuilder.RenameColumn(
                name: "CourseInstanceID",
                table: "CourseNotes",
                newName: "CourseInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseNotes_CourseInstanceID",
                table: "CourseNotes",
                newName: "IX_CourseNotes_CourseInstanceId");

            migrationBuilder.RenameIndex(
                name: "UQ__CourseIn__EA334D090513E077",
                table: "CourseInstance",
                newName: "UQ__CourseIn__EA334D09ED3CADE0");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NoteModified",
                table: "LONotes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Loid",
                table: "LONotes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "NoteUserModifed",
                table: "LONotes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstructorLoginEmail",
                table: "Instructors",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstructorTitle",
                table: "Instructors",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NoteModified",
                table: "CourseNotes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseInstanceId",
                table: "CourseNotes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK__SampleFi__CA195970D4F455B9",
                table: "SampleFiles",
                column: "SID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LONotes",
                table: "LONotes",
                column: "NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Learning__76F319DDBAB9F1A4",
                table: "LearningOutcomes",
                column: "LOID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Instruct__8D7A08C6F8CFADF9",
                table: "Instructors",
                column: "IKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Evaluati__258EC8E02063D158",
                table: "EvaluationMetrics",
                column: "EMID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseNotes",
                table: "CourseNotes",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "UQ__Instruct__9386E1D4FC8546A5",
                table: "Instructors",
                columns: new[] { "CourseInstanceID", "InstructorLoginEmail" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseNotes_CourseInstance_CourseInstanceId",
                table: "CourseNotes",
                column: "CourseInstanceId",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Evaluation__LOID__29572725",
                table: "EvaluationMetrics",
                column: "LOID",
                principalTable: "LearningOutcomes",
                principalColumn: "LOID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Instructo__Cours__412EB0B6",
                table: "Instructors",
                column: "CourseInstanceID",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__LearningO__Cours__267ABA7A",
                table: "LearningOutcomes",
                column: "CourseInstanceID",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LONotes_LearningOutcomes_Loid",
                table: "LONotes",
                column: "Loid",
                principalTable: "LearningOutcomes",
                principalColumn: "LOID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__SampleFile__EMID__2C3393D0",
                table: "SampleFiles",
                column: "EMID",
                principalTable: "EvaluationMetrics",
                principalColumn: "EMID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
