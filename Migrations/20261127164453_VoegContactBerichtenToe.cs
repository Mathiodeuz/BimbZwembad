using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zwemclub.Migrations
{
    /// <inheritdoc />
    public partial class VoegContactBerichtenToe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactBerichten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Onderwerp = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Bericht = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    VerzondenOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AfzenderIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBeantwoord = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactBerichten", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactBerichten");
        }
    }
}
