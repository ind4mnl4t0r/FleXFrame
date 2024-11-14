using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleXFrame.AcademicSuite.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AcademicSuite");

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "AcademicSuite",
                columns: table => new
                {
                    CourseID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CourseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "AcademicSuite",
                columns: table => new
                {
                    StudentID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalIDNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    State = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    GuardianName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    GuardianPhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    GuardianEmail = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    RelationshipToStudent = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EmergencyContactMobile = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                schema: "AcademicSuite",
                columns: table => new
                {
                    BatchID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlannedStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchID);
                    table.ForeignKey(
                        name: "FK_Batches_Courses_CourseID",
                        column: x => x.CourseID,
                        principalSchema: "AcademicSuite",
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseBatches",
                schema: "AcademicSuite",
                columns: table => new
                {
                    CourseBatchID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBatches", x => x.CourseBatchID);
                    table.ForeignKey(
                        name: "FK_CourseBatches_Batches_BatchID",
                        column: x => x.BatchID,
                        principalSchema: "AcademicSuite",
                        principalTable: "Batches",
                        principalColumn: "BatchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseBatches_Courses_CourseID",
                        column: x => x.CourseID,
                        principalSchema: "AcademicSuite",
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentEnrollments",
                schema: "AcademicSuite",
                columns: table => new
                {
                    EnrollmentID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEnrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_Batches_BatchID",
                        column: x => x.BatchID,
                        principalSchema: "AcademicSuite",
                        principalTable: "Batches",
                        principalColumn: "BatchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_Courses_CourseID",
                        column: x => x.CourseID,
                        principalSchema: "AcademicSuite",
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_Students_StudentID",
                        column: x => x.StudentID,
                        principalSchema: "AcademicSuite",
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_CourseID",
                schema: "AcademicSuite",
                table: "Batches",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBatches_BatchID",
                schema: "AcademicSuite",
                table: "CourseBatches",
                column: "BatchID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBatches_CourseID",
                schema: "AcademicSuite",
                table: "CourseBatches",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_BatchID",
                schema: "AcademicSuite",
                table: "StudentEnrollments",
                column: "BatchID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_CourseID",
                schema: "AcademicSuite",
                table: "StudentEnrollments",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_StudentID",
                schema: "AcademicSuite",
                table: "StudentEnrollments",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseBatches",
                schema: "AcademicSuite");

            migrationBuilder.DropTable(
                name: "StudentEnrollments",
                schema: "AcademicSuite");

            migrationBuilder.DropTable(
                name: "Batches",
                schema: "AcademicSuite");

            migrationBuilder.DropTable(
                name: "Students",
                schema: "AcademicSuite");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "AcademicSuite");
        }
    }
}
