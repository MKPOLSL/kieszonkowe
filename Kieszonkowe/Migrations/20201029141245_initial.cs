using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kieszonkowe.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDegrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EducationDegree = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChildRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EducationId = table.Column<Guid>(nullable: true),
                    RegionId = table.Column<Guid>(nullable: true),
                    PlannedAmount = table.Column<int>(nullable: true),
                    ActualAmount = table.Column<int>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildRecords_EducationDegrees_EducationId",
                        column: x => x.EducationId,
                        principalTable: "EducationDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildRecords_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildRecords_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildRecords_EducationId",
                table: "ChildRecords",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildRecords_ParentId",
                table: "ChildRecords",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildRecords_RegionId",
                table: "ChildRecords",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "ChildRecords");

            migrationBuilder.DropTable(
                name: "EducationDegrees");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
