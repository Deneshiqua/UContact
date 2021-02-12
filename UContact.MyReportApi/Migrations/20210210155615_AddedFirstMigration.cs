using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UContact.MyReportApi.Migrations
{
    public partial class AddedFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    ContactCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PhoneNumberCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    StatusId = table.Column<int>(type: "integer", nullable: false, defaultValue: 10),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
