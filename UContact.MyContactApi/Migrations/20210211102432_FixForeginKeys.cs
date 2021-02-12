using Microsoft.EntityFrameworkCore.Migrations;

namespace UContact.MyContactApi.Migrations
{
    public partial class FixForeginKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoType",
                table: "PersonInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InfoType",
                table: "PersonInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
