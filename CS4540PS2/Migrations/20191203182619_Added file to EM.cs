using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540PS2.Migrations
{
    public partial class AddedfiletoEM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "EvaluationMetrics",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "EvaluationMetrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "EvaluationMetrics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "EvaluationMetrics");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "EvaluationMetrics");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "EvaluationMetrics");
        }
    }
}
