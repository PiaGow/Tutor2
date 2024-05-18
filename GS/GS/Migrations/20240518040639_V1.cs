using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Idcs = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Idcs);
                });

            migrationBuilder.CreateTable(
                name: "HomeWork",
                columns: table => new
                {
                    Idhk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namehk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Timeend = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Assignmentsubmitted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSubmitted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeWork", x => x.Idhk);
                });

            migrationBuilder.CreateTable(
                name: "Servicer",
                columns: table => new
                {
                    IdService = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicer", x => x.IdService);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Idst = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namest = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Idst);
                });

            migrationBuilder.CreateTable(
                name: "TimeCourse",
                columns: table => new
                {
                    Idtimece = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Timeend = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCourse", x => x.Idtimece);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDCardImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idst = table.Column<int>(type: "int", nullable: false),
                    SubjectIdst = table.Column<int>(type: "int", nullable: false),
                    Idcs = table.Column<int>(type: "int", nullable: false),
                    ClassIdcs = table.Column<int>(type: "int", nullable: false),
                    IdService = table.Column<int>(type: "int", nullable: false),
                    serviceIdService = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Class_ClassIdcs",
                        column: x => x.ClassIdcs,
                        principalTable: "Class",
                        principalColumn: "Idcs",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Servicer_serviceIdService",
                        column: x => x.serviceIdService,
                        principalTable: "Servicer",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Subject_SubjectIdst",
                        column: x => x.SubjectIdst,
                        principalTable: "Subject",
                        principalColumn: "Idst",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assess",
                columns: table => new
                {
                    IdAS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeAS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleAS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assess", x => x.IdAS);
                    table.ForeignKey(
                        name: "FK_Assess_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    IdBill = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalDiscount = table.Column<float>(type: "real", nullable: false),
                    TotalMoney = table.Column<float>(type: "real", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.IdBill);
                    table.ForeignKey(
                        name: "FK_Bill_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Idce = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCourse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Starttime = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false),
                    Endtime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Courseinformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayInWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Idst = table.Column<int>(type: "int", nullable: false),
                    SubjectIdst = table.Column<int>(type: "int", nullable: false),
                    Idtimece = table.Column<int>(type: "int", nullable: false),
                    TimeCourseIdtimece = table.Column<int>(type: "int", nullable: false),
                    Idcs = table.Column<int>(type: "int", nullable: false),
                    ClassIdcs = table.Column<int>(type: "int", nullable: false),
                    Idhk = table.Column<int>(type: "int", nullable: false),
                    homeWorkIdhk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Idce);
                    table.ForeignKey(
                        name: "FK_Course_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Class_ClassIdcs",
                        column: x => x.ClassIdcs,
                        principalTable: "Class",
                        principalColumn: "Idcs",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_HomeWork_homeWorkIdhk",
                        column: x => x.homeWorkIdhk,
                        principalTable: "HomeWork",
                        principalColumn: "Idhk",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Subject_SubjectIdst",
                        column: x => x.SubjectIdst,
                        principalTable: "Subject",
                        principalColumn: "Idst",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_TimeCourse_TimeCourseIdtimece",
                        column: x => x.TimeCourseIdtimece,
                        principalTable: "TimeCourse",
                        principalColumn: "Idtimece",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratting",
                columns: table => new
                {
                    GradeNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseNumber = table.Column<float>(type: "real", nullable: false),
                    ReviewNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratting", x => x.GradeNumber);
                    table.ForeignKey(
                        name: "FK_Ratting_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequiredScore",
                columns: table => new
                {
                    IdRC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPA = table.Column<float>(type: "real", nullable: false),
                    Achievements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredScore", x => x.IdRC);
                    table.ForeignKey(
                        name: "FK_RequiredScore_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    IdTH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBill = table.Column<int>(type: "int", nullable: false),
                    BillIdBill = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.IdTH);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Bill_BillIdBill",
                        column: x => x.BillIdBill,
                        principalTable: "Bill",
                        principalColumn: "IdBill");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassIdcs",
                table: "AspNetUsers",
                column: "ClassIdcs");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_serviceIdService",
                table: "AspNetUsers",
                column: "serviceIdService");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubjectIdst",
                table: "AspNetUsers",
                column: "SubjectIdst");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assess_UserId",
                table: "Assess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserId",
                table: "Bill",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_ClassIdcs",
                table: "Course",
                column: "ClassIdcs");

            migrationBuilder.CreateIndex(
                name: "IX_Course_homeWorkIdhk",
                table: "Course",
                column: "homeWorkIdhk");

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubjectIdst",
                table: "Course",
                column: "SubjectIdst");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TimeCourseIdtimece",
                table: "Course",
                column: "TimeCourseIdtimece");

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserId",
                table: "Course",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratting_UserId",
                table: "Ratting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredScore_UserId",
                table: "RequiredScore",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_BillIdBill",
                table: "TransactionHistory",
                column: "BillIdBill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Assess");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Ratting");

            migrationBuilder.DropTable(
                name: "RequiredScore");

            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "HomeWork");

            migrationBuilder.DropTable(
                name: "TimeCourse");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Servicer");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
