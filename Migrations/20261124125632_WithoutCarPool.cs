using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zwemclub.Migrations
{
    /// <inheritdoc />
    public partial class WithoutCarPool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GebruikerCarpool");

            migrationBuilder.DropTable(
                name: "Carpool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carpool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OuderId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    WedstrijdId = table.Column<int>(type: "int", nullable: false),
                    AantalBeschikbarePlaatsen = table.Column<int>(type: "int", nullable: false),
                    GeschatUurTerugkomst = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: true),
                    VertrekPlaats = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    VertrekUur = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carpool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carpool_AspNetUsers_OuderId",
                        column: x => x.OuderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carpool_Wedstrijd_WedstrijdId",
                        column: x => x.WedstrijdId,
                        principalTable: "Wedstrijd",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GebruikerCarpool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarpoolId = table.Column<int>(type: "int", nullable: false),
                    GebruikerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AantalPersonen = table.Column<int>(type: "int", nullable: false),
                    DatumAanvraag = table.Column<DateOnly>(type: "date", nullable: false),
                    DatumBevestigd = table.Column<DateOnly>(type: "date", nullable: false),
                    IsBevestigd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GebruikerCarpool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GebruikerCarpool_AspNetUsers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GebruikerCarpool_Carpool_CarpoolId",
                        column: x => x.CarpoolId,
                        principalTable: "Carpool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carpool",
                columns: new[] { "Id", "AantalBeschikbarePlaatsen", "GeschatUurTerugkomst", "IsVerwijderd", "OuderId", "VertrekPlaats", "VertrekUur", "WedstrijdId" },
                values: new object[] { 1, 3, new DateTime(2026, 11, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), false, "zwemclub-ouder", "Parking clubzwembad", new DateTime(2026, 11, 15, 8, 30, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "GebruikerCarpool",
                columns: new[] { "Id", "AantalPersonen", "CarpoolId", "DatumAanvraag", "DatumBevestigd", "GebruikerId", "IsBevestigd" },
                values: new object[] { 1, 1, 1, new DateOnly(2026, 10, 21), new DateOnly(2026, 10, 22), "zwemclub-zwemmer", true });

            migrationBuilder.CreateIndex(
                name: "IX_Carpool_OuderId",
                table: "Carpool",
                column: "OuderId");

            migrationBuilder.CreateIndex(
                name: "IX_Carpool_WedstrijdId",
                table: "Carpool",
                column: "WedstrijdId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerCarpool_CarpoolId",
                table: "GebruikerCarpool",
                column: "CarpoolId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerCarpool_GebruikerId",
                table: "GebruikerCarpool",
                column: "GebruikerId");
        }
    }
}
