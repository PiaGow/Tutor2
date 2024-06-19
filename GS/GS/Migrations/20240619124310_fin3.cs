using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Migrations
{
    /// <inheritdoc />
    public partial class fin3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoursesStudentIdcourses",
                table: "HomeWork",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeWork_CoursesStudentIdcourses",
                table: "HomeWork",
                column: "CoursesStudentIdcourses");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWork_CoursesStudents_CoursesStudentIdcourses",
                table: "HomeWork",
                column: "CoursesStudentIdcourses",
                principalTable: "CoursesStudents",
                principalColumn: "Idcourses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWork_CoursesStudents_CoursesStudentIdcourses",
                table: "HomeWork");

            migrationBuilder.DropIndex(
                name: "IX_HomeWork_CoursesStudentIdcourses",
                table: "HomeWork");

            migrationBuilder.DropColumn(
                name: "CoursesStudentIdcourses",
                table: "HomeWork");
        }
    }
}
