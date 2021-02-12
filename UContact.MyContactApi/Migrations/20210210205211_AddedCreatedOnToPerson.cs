using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UContact.MyContactApi.Migrations
{
    public partial class AddedCreatedOnToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Persons",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Persons");
        }
    }
}
