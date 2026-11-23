using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Zwemclub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Geslacht",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geslacht", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Onderwerp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Waarde = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plaats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Straat = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HuisNummer = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Gemeente = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Land = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plaats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReeksType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReeksType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Voornaam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Geboortedatum = table.Column<DateOnly>(type: "date", nullable: true),
                    GeslachtId = table.Column<int>(type: "int", nullable: true),
                    UniekNummer = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FederatieNummer = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ProfielFoto = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActief = table.Column<bool>(type: "bit", nullable: false),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Geslacht_GeslachtId",
                        column: x => x.GeslachtId,
                        principalTable: "Geslacht",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Wedstrijd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Datum = table.Column<DateOnly>(type: "date", nullable: false),
                    DeadlineInschrijven = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlWedstrijd = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PlaatsId = table.Column<int>(type: "int", nullable: false),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wedstrijd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wedstrijd_Plaats_PlaatsId",
                        column: x => x.PlaatsId,
                        principalTable: "Plaats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerGroep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ZwemmerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DatumVanaf = table.Column<DateOnly>(type: "date", nullable: false),
                    DatumTot = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerGroep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerGroep_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainerGroep_AspNetUsers_ZwemmerId",
                        column: x => x.ZwemmerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoogdZwemmer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoogdId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ZwemmerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsMailGestuurd = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoogdZwemmer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoogdZwemmer_AspNetUsers_VoogdId",
                        column: x => x.VoogdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VoogdZwemmer_AspNetUsers_ZwemmerId",
                        column: x => x.ZwemmerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Carpool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OuderId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    WedstrijdId = table.Column<int>(type: "int", nullable: false),
                    AantalBeschikbarePlaatsen = table.Column<int>(type: "int", nullable: false),
                    VertrekPlaats = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    VertrekUur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeschatUurTerugkomst = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: true)
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
                name: "WedstrijdReeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeslachtId = table.Column<int>(type: "int", nullable: false),
                    ReeksTypeId = table.Column<int>(type: "int", nullable: false),
                    WedstrijdId = table.Column<int>(type: "int", nullable: false),
                    Startuur = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MaxAantalInschrijvingen = table.Column<int>(type: "int", nullable: false),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WedstrijdReeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WedstrijdReeks_Geslacht_GeslachtId",
                        column: x => x.GeslachtId,
                        principalTable: "Geslacht",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WedstrijdReeks_ReeksType_ReeksTypeId",
                        column: x => x.ReeksTypeId,
                        principalTable: "ReeksType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WedstrijdReeks_Wedstrijd_WedstrijdId",
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
                    GebruikerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CarpoolId = table.Column<int>(type: "int", nullable: false),
                    IsBevestigd = table.Column<bool>(type: "bit", nullable: false),
                    DatumAanvraag = table.Column<DateOnly>(type: "date", nullable: false),
                    DatumBevestigd = table.Column<DateOnly>(type: "date", nullable: false),
                    AantalPersonen = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "InschrijvingWedstrijd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZwemmerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    WedstrijdReeksId = table.Column<int>(type: "int", nullable: false),
                    DatumInschrijving = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumAnnulatie = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RedenAnnulatie = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DatumDefinitief = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BehaaldeTijd = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    BehaaldePlaats = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsVerwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InschrijvingWedstrijd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InschrijvingWedstrijd_AspNetUsers_ZwemmerId",
                        column: x => x.ZwemmerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InschrijvingWedstrijd_WedstrijdReeks_WedstrijdReeksId",
                        column: x => x.WedstrijdReeksId,
                        principalTable: "WedstrijdReeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "rol-ouder", "rol-ouder-v1", "Ouder", "OUDER" },
                    { "rol-trainer", "rol-trainer-v1", "Trainer", "TRAINER" },
                    { "rol-voorzitter", "rol-voorzitter-v1", "Voorzitter", "VOORZITTER" },
                    { "rol-zwemmer", "rol-zwemmer-v1", "Zwemmer", "ZWEMMER" }
                });

            migrationBuilder.InsertData(
                table: "Geslacht",
                columns: new[] { "Id", "IsVerwijderd", "Naam" },
                values: new object[,]
                {
                    { 1, false, "Man" },
                    { 2, false, "Vrouw" },
                    { 3, false, "Andere" }
                });

            migrationBuilder.InsertData(
                table: "Mail",
                columns: new[] { "Id", "Body", "IsVerwijderd", "Naam", "Onderwerp" },
                values: new object[] { 1, "Je inschrijving voor de wedstrijd is bevestigd.", false, "Inschrijving bevestigd", "Je inschrijving bij Zwemclub" });

            migrationBuilder.InsertData(
                table: "Parameter",
                columns: new[] { "Id", "Naam", "Waarde" },
                values: new object[] { 1, "Clubnaam", "Zwemclub" });

            migrationBuilder.InsertData(
                table: "Plaats",
                columns: new[] { "Id", "Gemeente", "HuisNummer", "IsVerwijderd", "Land", "Naam", "Postcode", "Straat" },
                values: new object[] { 1, "Leuven", "6", false, "België", "Sportoase Leuven", "3001", "Philipssite" });

            migrationBuilder.InsertData(
                table: "ReeksType",
                columns: new[] { "Id", "IsVerwijderd", "Naam" },
                values: new object[,]
                {
                    { 1, false, "50 meter vrije slag" },
                    { 2, false, "100 meter schoolslag" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FederatieNummer", "Geboortedatum", "GeslachtId", "IsActief", "IsVerwijderd", "LockoutEnabled", "LockoutEnd", "Naam", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfielFoto", "SecurityStamp", "TwoFactorEnabled", "UniekNummer", "UserName", "Voornaam" },
                values: new object[,]
                {
                    { "zwemclub-ouder", 0, "ouder-concurrency-v1", "ouder@zwemclub.example", false, null, new DateOnly(1985, 3, 12), 2, true, false, false, null, "Peeters", "OUDER@ZWEMCLUB.EXAMPLE", "OUDER@ZWEMCLUB.EXAMPLE", null, null, false, null, "ouder-security-v1", false, "ZC-0001", "ouder@zwemclub.example", "Sofie" },
                    { "zwemclub-ouder-2", 0, "ouder-2-concurrency-v1", "ouder-2@zwemclub.example", false, null, new DateOnly(1982, 5, 10), 1, true, false, false, null, "De Smet", "OUDER-2@ZWEMCLUB.EXAMPLE", "OUDER-2@ZWEMCLUB.EXAMPLE", null, null, false, null, "ouder-2-security-v1", false, "ZC-0010", "ouder-2@zwemclub.example", "Koen" },
                    { "zwemclub-ouder-3", 0, "ouder-3-concurrency-v1", "ouder-3@zwemclub.example", false, null, new DateOnly(1984, 8, 16), 2, true, false, false, null, "Willems", "OUDER-3@ZWEMCLUB.EXAMPLE", "OUDER-3@ZWEMCLUB.EXAMPLE", null, null, false, null, "ouder-3-security-v1", false, "ZC-0011", "ouder-3@zwemclub.example", "Sara" },
                    { "zwemclub-ouder-4", 0, "ouder-4-concurrency-v1", "ouder-4@zwemclub.example", false, null, new DateOnly(1983, 12, 7), 3, true, false, false, null, "Maes", "OUDER-4@ZWEMCLUB.EXAMPLE", "OUDER-4@ZWEMCLUB.EXAMPLE", null, null, false, null, "ouder-4-security-v1", false, "ZC-0012", "ouder-4@zwemclub.example", "Robin" },
                    { "zwemclub-trainer", 0, "trainer-concurrency-v1", "trainer@zwemclub.example", false, null, null, 1, true, false, false, null, "Janssens", "TRAINER@ZWEMCLUB.EXAMPLE", "TRAINER@ZWEMCLUB.EXAMPLE", null, null, false, null, "trainer-security-v1", false, "ZC-0003", "trainer@zwemclub.example", "Bram" },
                    { "zwemclub-trainer-2", 0, "trainer-2-concurrency-v1", "trainer-2@zwemclub.example", false, null, new DateOnly(1990, 3, 25), 2, true, false, false, null, "Van den Broeck", "TRAINER-2@ZWEMCLUB.EXAMPLE", "TRAINER-2@ZWEMCLUB.EXAMPLE", null, null, false, null, "trainer-2-security-v1", false, "ZC-0013", "trainer-2@zwemclub.example", "Julie" },
                    { "zwemclub-trainer-3", 0, "trainer-3-concurrency-v1", "trainer-3@zwemclub.example", false, null, new DateOnly(1988, 10, 12), 1, true, false, false, null, "Verstraeten", "TRAINER-3@ZWEMCLUB.EXAMPLE", "TRAINER-3@ZWEMCLUB.EXAMPLE", null, null, false, null, "trainer-3-security-v1", false, "ZC-0014", "trainer-3@zwemclub.example", "Tom" },
                    { "zwemclub-voorzitter", 0, "voorzitter-concurrency-v1", "voorzitter@zwemclub.example", false, null, null, 2, true, false, false, null, "Vermeulen", "VOORZITTER@ZWEMCLUB.EXAMPLE", "VOORZITTER@ZWEMCLUB.EXAMPLE", null, null, false, null, "voorzitter-security-v1", false, "ZC-0004", "voorzitter@zwemclub.example", "An" },
                    { "zwemclub-zwemmer", 0, "zwemmer-concurrency-v1", "zwemmer@zwemclub.example", false, "VZF-0002", new DateOnly(2012, 6, 20), 1, true, false, false, null, "Peeters", "ZWEMMER@ZWEMCLUB.EXAMPLE", "ZWEMMER@ZWEMCLUB.EXAMPLE", null, null, false, null, "zwemmer-security-v1", false, "ZC-0002", "zwemmer@zwemclub.example", "Noah" },
                    { "zwemclub-zwemmer-2", 0, "zwemmer-2-concurrency-v1", "zwemmer-2@zwemclub.example", false, "VZF-0005", new DateOnly(2011, 2, 14), 1, true, false, false, null, "De Smet", "ZWEMMER-2@ZWEMCLUB.EXAMPLE", "ZWEMMER-2@ZWEMCLUB.EXAMPLE", null, null, false, null, "zwemmer-2-security-v1", false, "ZC-0005", "zwemmer-2@zwemclub.example", "Lars" },
                    { "zwemclub-zwemmer-3", 0, "zwemmer-3-concurrency-v1", "zwemmer-3@zwemclub.example", false, "VZF-0006", new DateOnly(2012, 7, 9), 2, true, false, false, null, "Willems", "ZWEMMER-3@ZWEMCLUB.EXAMPLE", "ZWEMMER-3@ZWEMCLUB.EXAMPLE", null, null, false, null, "zwemmer-3-security-v1", false, "ZC-0006", "zwemmer-3@zwemclub.example", "Emma" },
                    { "zwemclub-zwemmer-4", 0, "zwemmer-4-concurrency-v1", "zwemmer-4@zwemclub.example", false, "VZF-0007", new DateOnly(2011, 11, 3), 3, true, false, false, null, "Maes", "ZWEMMER-4@ZWEMCLUB.EXAMPLE", "ZWEMMER-4@ZWEMCLUB.EXAMPLE", null, null, false, null, "zwemmer-4-security-v1", false, "ZC-0007", "zwemmer-4@zwemclub.example", "Alex" },
                    { "zwemclub-zwemmer-5", 0, "zwemmer-5-concurrency-v1", "zwemmer-5@zwemclub.example", false, "VZF-0008", new DateOnly(2013, 1, 18), 1, true, false, false, null, "Jacobs", "ZWEMMER-5@ZWEMCLUB.EXAMPLE", "ZWEMMER-5@ZWEMCLUB.EXAMPLE", null, null, false, null, "zwemmer-5-security-v1", false, "ZC-0008", "zwemmer-5@zwemclub.example", "Milan" },
                    { "zwemclub-zwemmer-6", 0, "zwemmer-6-concurrency-v1", "zwemmer-6@zwemclub.example", false, "VZF-0009", new DateOnly(2012, 9, 22), 2, true, false, false, null, "Claes", "ZWEMMER-6@ZWEMCLUB.EXAMPLE", "ZWEMMER-6@ZWEMCLUB.EXAMPLE", null, null, false, null, "zwemmer-6-security-v1", false, "ZC-0009", "zwemmer-6@zwemclub.example", "Louise" }
                });

            migrationBuilder.InsertData(
                table: "Wedstrijd",
                columns: new[] { "Id", "Datum", "DeadlineInschrijven", "IsVerwijderd", "Naam", "PlaatsId", "UrlWedstrijd" },
                values: new object[] { 1, new DateOnly(2026, 11, 15), new DateTime(2026, 11, 8, 20, 0, 0, 0, DateTimeKind.Unspecified), false, "Zwemclub herfstmeeting", 1, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "rol-ouder", "zwemclub-ouder" },
                    { "rol-ouder", "zwemclub-ouder-2" },
                    { "rol-ouder", "zwemclub-ouder-3" },
                    { "rol-ouder", "zwemclub-ouder-4" },
                    { "rol-trainer", "zwemclub-trainer" },
                    { "rol-trainer", "zwemclub-trainer-2" },
                    { "rol-trainer", "zwemclub-trainer-3" },
                    { "rol-voorzitter", "zwemclub-voorzitter" },
                    { "rol-zwemmer", "zwemclub-zwemmer" },
                    { "rol-zwemmer", "zwemclub-zwemmer-2" },
                    { "rol-zwemmer", "zwemclub-zwemmer-3" },
                    { "rol-zwemmer", "zwemclub-zwemmer-4" },
                    { "rol-zwemmer", "zwemclub-zwemmer-5" },
                    { "rol-zwemmer", "zwemclub-zwemmer-6" }
                });

            migrationBuilder.InsertData(
                table: "Carpool",
                columns: new[] { "Id", "AantalBeschikbarePlaatsen", "GeschatUurTerugkomst", "IsVerwijderd", "OuderId", "VertrekPlaats", "VertrekUur", "WedstrijdId" },
                values: new object[] { 1, 3, new DateTime(2026, 11, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), false, "zwemclub-ouder", "Parking clubzwembad", new DateTime(2026, 11, 15, 8, 30, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "TrainerGroep",
                columns: new[] { "Id", "DatumTot", "DatumVanaf", "TrainerId", "ZwemmerId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2027, 8, 31), new DateOnly(2026, 9, 1), "zwemclub-trainer", "zwemclub-zwemmer" },
                    { 2, new DateOnly(2027, 8, 31), new DateOnly(2026, 9, 1), "zwemclub-trainer-2", "zwemclub-zwemmer-2" },
                    { 3, new DateOnly(2027, 8, 31), new DateOnly(2026, 9, 1), "zwemclub-trainer-2", "zwemclub-zwemmer-3" },
                    { 4, new DateOnly(2027, 8, 31), new DateOnly(2026, 9, 1), "zwemclub-trainer-3", "zwemclub-zwemmer-4" },
                    { 5, new DateOnly(2027, 8, 31), new DateOnly(2026, 9, 1), "zwemclub-trainer-3", "zwemclub-zwemmer-5" },
                    { 6, new DateOnly(2027, 8, 31), new DateOnly(2026, 9, 1), "zwemclub-trainer-2", "zwemclub-zwemmer-6" }
                });

            migrationBuilder.InsertData(
                table: "VoogdZwemmer",
                columns: new[] { "Id", "IsMailGestuurd", "VoogdId", "ZwemmerId" },
                values: new object[,]
                {
                    { 1, false, "zwemclub-ouder", "zwemclub-zwemmer" },
                    { 2, false, "zwemclub-ouder-2", "zwemclub-zwemmer-2" },
                    { 3, false, "zwemclub-ouder-3", "zwemclub-zwemmer-3" },
                    { 4, false, "zwemclub-ouder-4", "zwemclub-zwemmer-4" },
                    { 5, false, "zwemclub-ouder-2", "zwemclub-zwemmer-5" },
                    { 6, false, "zwemclub-ouder-3", "zwemclub-zwemmer-6" }
                });

            migrationBuilder.InsertData(
                table: "WedstrijdReeks",
                columns: new[] { "Id", "GeslachtId", "IsVerwijderd", "MaxAantalInschrijvingen", "ReeksTypeId", "Startuur", "WedstrijdId" },
                values: new object[,]
                {
                    { 1, 1, false, 24, 1, "10:00", 1 },
                    { 2, 2, false, 24, 1, "10:15", 1 },
                    { 3, 3, false, 24, 1, "10:30", 1 },
                    { 4, 1, false, 24, 2, "11:00", 1 },
                    { 5, 2, false, 24, 2, "11:15", 1 },
                    { 6, 3, false, 24, 2, "11:30", 1 }
                });

            migrationBuilder.InsertData(
                table: "GebruikerCarpool",
                columns: new[] { "Id", "AantalPersonen", "CarpoolId", "DatumAanvraag", "DatumBevestigd", "GebruikerId", "IsBevestigd" },
                values: new object[] { 1, 1, 1, new DateOnly(2026, 10, 21), new DateOnly(2026, 10, 22), "zwemclub-zwemmer", true });

            migrationBuilder.InsertData(
                table: "InschrijvingWedstrijd",
                columns: new[] { "Id", "BehaaldePlaats", "BehaaldeTijd", "DatumAnnulatie", "DatumDefinitief", "DatumInschrijving", "IsVerwijderd", "RedenAnnulatie", "WedstrijdReeksId", "ZwemmerId" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new DateTime(2026, 10, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 1, "zwemclub-zwemmer" },
                    { 2, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 4, "zwemclub-zwemmer" },
                    { 3, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 1, "zwemclub-zwemmer-2" },
                    { 4, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 4, "zwemclub-zwemmer-2" },
                    { 5, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 2, "zwemclub-zwemmer-3" },
                    { 6, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 5, "zwemclub-zwemmer-3" },
                    { 7, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 3, "zwemclub-zwemmer-4" },
                    { 8, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 6, "zwemclub-zwemmer-4" },
                    { 9, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 1, "zwemclub-zwemmer-5" },
                    { 10, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 4, "zwemclub-zwemmer-5" },
                    { 11, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 2, "zwemclub-zwemmer-6" },
                    { 12, null, null, null, new DateTime(2026, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), false, null, 5, "zwemclub-zwemmer-6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GeslachtId",
                table: "AspNetUsers",
                column: "GeslachtId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UniekNummer",
                table: "AspNetUsers",
                column: "UniekNummer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_InschrijvingWedstrijd_WedstrijdReeksId",
                table: "InschrijvingWedstrijd",
                column: "WedstrijdReeksId");

            migrationBuilder.CreateIndex(
                name: "IX_InschrijvingWedstrijd_ZwemmerId",
                table: "InschrijvingWedstrijd",
                column: "ZwemmerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerGroep_TrainerId",
                table: "TrainerGroep",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerGroep_ZwemmerId",
                table: "TrainerGroep",
                column: "ZwemmerId");

            migrationBuilder.CreateIndex(
                name: "IX_VoogdZwemmer_VoogdId",
                table: "VoogdZwemmer",
                column: "VoogdId");

            migrationBuilder.CreateIndex(
                name: "IX_VoogdZwemmer_ZwemmerId",
                table: "VoogdZwemmer",
                column: "ZwemmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wedstrijd_PlaatsId",
                table: "Wedstrijd",
                column: "PlaatsId");

            migrationBuilder.CreateIndex(
                name: "IX_WedstrijdReeks_GeslachtId",
                table: "WedstrijdReeks",
                column: "GeslachtId");

            migrationBuilder.CreateIndex(
                name: "IX_WedstrijdReeks_ReeksTypeId",
                table: "WedstrijdReeks",
                column: "ReeksTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WedstrijdReeks_WedstrijdId",
                table: "WedstrijdReeks",
                column: "WedstrijdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GebruikerCarpool");

            migrationBuilder.DropTable(
                name: "InschrijvingWedstrijd");

            migrationBuilder.DropTable(
                name: "Mail");

            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "TrainerGroep");

            migrationBuilder.DropTable(
                name: "VoogdZwemmer");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carpool");

            migrationBuilder.DropTable(
                name: "WedstrijdReeks");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ReeksType");

            migrationBuilder.DropTable(
                name: "Wedstrijd");

            migrationBuilder.DropTable(
                name: "Geslacht");

            migrationBuilder.DropTable(
                name: "Plaats");
        }
    }
}
