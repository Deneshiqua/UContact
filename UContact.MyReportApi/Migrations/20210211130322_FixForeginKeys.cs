using Microsoft.EntityFrameworkCore.Migrations;

namespace UContact.MyReportApi.Migrations
{
    public partial class FixForeginKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reports",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
