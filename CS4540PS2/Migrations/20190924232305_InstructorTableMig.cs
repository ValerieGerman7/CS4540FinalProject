using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class InstructorTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Evaluation__LOID__2F10007B",
                table: "EvaluationMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK__LearningO__Cours__2C3393D0",
                table: "LearningOutcomes");

            migrationBuilder.DropForeignKey(
                name: "FK__SampleFile__EMID__31EC6D26",
                table: "SampleFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SampleFi__CA195970E10633C4",
                table: "SampleFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Learning__76F319DDB98F0B5B",
                table: "LearningOutcomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Evaluati__258EC8E008589A09",
                table: "EvaluationMetrics");

            migrationBuilder.RenameIndex(
                name: "UQ__CourseIn__EA334D099E6B4C3E",
                table: "CourseInstance",
                newName: "UQ__CourseIn__EA334D09ED3CADE0");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SampleFi__CA195970D4F455B9",
                table: "SampleFiles",
                column: "SID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Learning__76F319DDBAB9F1A4",
                table: "LearningOutcomes",
                column: "LOID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Evaluati__258EC8E02063D158",
                table: "EvaluationMetrics",
                column: "EMID");

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    IKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseInstanceID = table.Column<int>(nullable: false),
                    InstructorLoginEmail = table.Column<string>(nullable: false),
                    InstructorTitle = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Instruct__8D7A08C6F8CFADF9", x => x.IKey);
                    table.ForeignKey(
                        name: "FK__Instructo__Cours__412EB0B6",
                        column: x => x.CourseInstanceID,
                        principalTable: "CourseInstance",
                        principalColumn: "CourseInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Instruct__9386E1D4FC8546A5",
                table: "Instructors",
                columns: new[] { "CourseInstanceID", "InstructorLoginEmail" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Evaluation__LOID__29572725",
                table: "EvaluationMetrics",
                column: "LOID",
                principalTable: "LearningOutcomes",
                principalColumn: "LOID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__LearningO__Cours__267ABA7A",
                table: "LearningOutcomes",
                column: "CourseInstanceID",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__SampleFile__EMID__2C3393D0",
                table: "SampleFiles",
                column: "EMID",
                principalTable: "EvaluationMetrics",
                principalColumn: "EMID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Evaluation__LOID__29572725",
                table: "EvaluationMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK__LearningO__Cours__267ABA7A",
                table: "LearningOutcomes");

            migrationBuilder.DropForeignKey(
                name: "FK__SampleFile__EMID__2C3393D0",
                table: "SampleFiles");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SampleFi__CA195970D4F455B9",
                table: "SampleFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Learning__76F319DDBAB9F1A4",
                table: "LearningOutcomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Evaluati__258EC8E02063D158",
                table: "EvaluationMetrics");

            migrationBuilder.RenameIndex(
                name: "UQ__CourseIn__EA334D09ED3CADE0",
                table: "CourseInstance",
                newName: "UQ__CourseIn__EA334D099E6B4C3E");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SampleFi__CA195970E10633C4",
                table: "SampleFiles",
                column: "SID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Learning__76F319DDB98F0B5B",
                table: "LearningOutcomes",
                column: "LOID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Evaluati__258EC8E008589A09",
                table: "EvaluationMetrics",
                column: "EMID");

            migrationBuilder.AddForeignKey(
                name: "FK__Evaluation__LOID__2F10007B",
                table: "EvaluationMetrics",
                column: "LOID",
                principalTable: "LearningOutcomes",
                principalColumn: "LOID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__LearningO__Cours__2C3393D0",
                table: "LearningOutcomes",
                column: "CourseInstanceID",
                principalTable: "CourseInstance",
                principalColumn: "CourseInstanceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__SampleFile__EMID__31EC6D26",
                table: "SampleFiles",
                column: "EMID",
                principalTable: "EvaluationMetrics",
                principalColumn: "EMID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
