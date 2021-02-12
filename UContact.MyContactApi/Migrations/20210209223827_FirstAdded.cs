using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UContact.MyContactApi.Migrations
{
    public partial class FirstAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    InfoTypeId = table.Column<int>(type: "integer", nullable: false),
                    InfoType = table.Column<int>(type: "integer", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonInfos_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonInfos_PersonId",
                table: "PersonInfos",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonInfos");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
