using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleXFrameCore.UserAuth.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserAuth");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "UserAuth",
                columns: table => new
                {
                    PermissionID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PermissionStatus = table.Column<int>(type: "int", nullable: false),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "UserAuth",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UserAuth",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "UserAuth",
                columns: table => new
                {
                    RolePermissionID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PermissionID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RolePermissionID);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalSchema: "UserAuth",
                        principalTable: "Permissions",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "UserAuth",
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "UserAuth",
                columns: table => new
                {
                    UserRoleID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsPrimaryRole = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleID);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "UserAuth",
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "UserAuth",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionID",
                schema: "UserAuth",
                table: "RolePermissions",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleID",
                schema: "UserAuth",
                table: "RolePermissions",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                schema: "UserAuth",
                table: "UserRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserID",
                schema: "UserAuth",
                table: "UserRoles",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "UserAuth");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "UserAuth");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "UserAuth");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "UserAuth");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "UserAuth");
        }
    }
}
