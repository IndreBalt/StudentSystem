using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentCode);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    LectureName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LectureStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    LectureEndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.LectureName);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentNumber);
                    table.ForeignKey(
                        name: "FK_Students_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentLectures",
                columns: table => new
                {
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LectureName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentLectures", x => new { x.DepartmentCode, x.LectureName });
                    table.ForeignKey(
                        name: "FK_DepartmentLectures_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentLectures_Lectures_LectureName",
                        column: x => x.LectureName,
                        principalTable: "Lectures",
                        principalColumn: "LectureName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLecture",
                columns: table => new
                {
                    StudentNumber = table.Column<int>(type: "int", nullable: false),
                    LectureName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLecture", x => new { x.LectureName, x.StudentNumber });
                    table.ForeignKey(
                        name: "FK_StudentLecture_Lectures_LectureName",
                        column: x => x.LectureName,
                        principalTable: "Lectures",
                        principalColumn: "LectureName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLecture_Students_StudentNumber",
                        column: x => x.StudentNumber,
                        principalTable: "Students",
                        principalColumn: "StudentNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentCode", "DepartmentName" },
                values: new object[,]
                {
                    { "CS1234", "ComputerScience" },
                    { "MTH567", "Mathematics" }
                });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "LectureName", "LectureEndTime", "LectureStartTime" },
                values: new object[,]
                {
                    { "Algorithms", new TimeOnly(11, 30, 0), new TimeOnly(10, 0, 0) },
                    { "Calculus", new TimeOnly(13, 30, 0), new TimeOnly(12, 0, 0) },
                    { "DataStructures", new TimeOnly(15, 30, 0), new TimeOnly(14, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "DepartmentLectures",
                columns: new[] { "DepartmentCode", "LectureName" },
                values: new object[,]
                {
                    { "CS1234", "Algorithms" },
                    { "CS1234", "DataStructures" },
                    { "MTH567", "Calculus" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentNumber", "DepartmentCode", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 12345678, "CS1234", "john.smith@example.com", "John", "Smith" },
                    { 87654321, "MTH567", "alice.johnson@example.com", "Alice", "Johnson" }
                });

            migrationBuilder.InsertData(
                table: "StudentLecture",
                columns: new[] { "LectureName", "StudentNumber" },
                values: new object[,]
                {
                    { "Algorithms", 12345678 },
                    { "Calculus", 87654321 },
                    { "DataStructures", 12345678 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLectures_LectureName",
                table: "DepartmentLectures",
                column: "LectureName");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLecture_StudentNumber",
                table: "StudentLecture",
                column: "StudentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentCode",
                table: "Students",
                column: "DepartmentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentLectures");

            migrationBuilder.DropTable(
                name: "StudentLecture");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
