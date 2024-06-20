using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Migrations
{
    /// <inheritdoc />
    public partial class fin4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserClass");

            migrationBuilder.DropTable(
                name: "ApplicationUserSubject");

            migrationBuilder.DropTable(
                name: "ClassSubject");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Class",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ApplicationUserId",
                table: "Subjects",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_ApplicationUserId",
                table: "Class",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_AspNetUsers_ApplicationUserId",
                table: "Class",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_ApplicationUserId",
                table: "Subjects",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_AspNetUsers_ApplicationUserId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_ApplicationUserId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ApplicationUserId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Class_ApplicationUserId",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Class");

            migrationBuilder.CreateTable(
                name: "ApplicationUserClass",
                columns: table => new
                {
                    ClassesIdcs = table.Column<int>(type: "int", nullable: false),
                    applicationsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserClass", x => new { x.ClassesIdcs, x.applicationsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserClass_AspNetUsers_applicationsId",
                        column: x => x.applicationsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserClass_Class_ClassesIdcs",
                        column: x => x.ClassesIdcs,
                        principalTable: "Class",
                        principalColumn: "Idcs",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSubject",
                columns: table => new
                {
                    SubjectsIdst = table.Column<int>(type: "int", nullable: false),
                    applicationsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSubject", x => new { x.SubjectsIdst, x.applicationsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSubject_AspNetUsers_applicationsId",
                        column: x => x.applicationsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSubject_Subjects_SubjectsIdst",
                        column: x => x.SubjectsIdst,
                        principalTable: "Subjects",
                        principalColumn: "Idst",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSubject",
                columns: table => new
                {
                    classesIdcs = table.Column<int>(type: "int", nullable: false),
                    subjectsIdst = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubject", x => new { x.classesIdcs, x.subjectsIdst });
                    table.ForeignKey(
                        name: "FK_ClassSubject_Class_classesIdcs",
                        column: x => x.classesIdcs,
                        principalTable: "Class",
                        principalColumn: "Idcs",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSubject_Subjects_subjectsIdst",
                        column: x => x.subjectsIdst,
                        principalTable: "Subjects",
                        principalColumn: "Idst",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserClass_applicationsId",
                table: "ApplicationUserClass",
                column: "applicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSubject_applicationsId",
                table: "ApplicationUserSubject",
                column: "applicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_subjectsIdst",
                table: "ClassSubject",
                column: "subjectsIdst");
        }
    }
}
