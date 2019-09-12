using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseInstance",
                columns: table => new
                {
                    CourseInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Department = table.Column<string>(maxLength: 5, nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Semester = table.Column<string>(maxLength: 6, nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInstance", x => x.CourseInstanceID);
                });

            migrationBuilder.CreateTable(
                name: "LearningOutcomes",
                columns: table => new
                {
                    LOID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CourseInstanceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__76F319DDB98F0B5B", x => x.LOID);
                    table.ForeignKey(
                        name: "FK__LearningO__Cours__2C3393D0",
                        column: x => x.CourseInstanceID,
                        principalTable: "CourseInstance",
                        principalColumn: "CourseInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationMetrics",
                columns: table => new
                {
                    EMID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    LOID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Evaluati__258EC8E008589A09", x => x.EMID);
                    table.ForeignKey(
                        name: "FK__Evaluation__LOID__2F10007B",
                        column: x => x.LOID,
                        principalTable: "LearningOutcomes",
                        principalColumn: "LOID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SampleFiles",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    EMID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SampleFi__CA195970E10633C4", x => x.SID);
                    table.ForeignKey(
                        name: "FK__SampleFile__EMID__31EC6D26",
                        column: x => x.EMID,
                        principalTable: "EvaluationMetrics",
                        principalColumn: "EMID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__CourseIn__EA334D099E6B4C3E",
                table: "CourseInstance",
                columns: new[] { "Department", "Number", "Semester", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationMetrics_LOID",
                table: "EvaluationMetrics",
                column: "LOID");

            migrationBuilder.CreateIndex(
                name: "IX_LearningOutcomes_CourseInstanceID",
                table: "LearningOutcomes",
                column: "CourseInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_SampleFiles_EMID",
                table: "SampleFiles",
                column: "EMID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleFiles");

            migrationBuilder.DropTable(
                name: "EvaluationMetrics");

            migrationBuilder.DropTable(
                name: "LearningOutcomes");

            migrationBuilder.DropTable(
                name: "CourseInstance");
        }
    }
}
