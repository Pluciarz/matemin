using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatemIn.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dzialy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    RodzicId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dzialy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dzialy_Dzialy_RodzicId",
                        column: x => x.RodzicId,
                        principalTable: "Dzialy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rola = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    HasloHash = table.Column<string>(type: "TEXT", nullable: true),
                    NazwaUzytkownika = table.Column<string>(type: "TEXT", nullable: true),
                    DataUrodzenia = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    DataRejestracji = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CzyZablokowany = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zadania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tresc = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DataDodania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DzialId = table.Column<int>(type: "INTEGER", nullable: true),
                    AutorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadania", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadania_Dzialy_DzialId",
                        column: x => x.DzialId,
                        principalTable: "Dzialy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Zadania_Uzytkownicy_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rozwiazania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tresc = table.Column<string>(type: "TEXT", nullable: false),
                    DataDodania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ZadanieId = table.Column<int>(type: "INTEGER", nullable: false),
                    AutorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rozwiazania", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rozwiazania_Uzytkownicy_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rozwiazania_Zadania_ZadanieId",
                        column: x => x.ZadanieId,
                        principalTable: "Zadania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentarze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tresc = table.Column<string>(type: "TEXT", nullable: false),
                    DataDodania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RozwiazanieId = table.Column<int>(type: "INTEGER", nullable: false),
                    AutorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentarze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentarze_Rozwiazania_RozwiazanieId",
                        column: x => x.RozwiazanieId,
                        principalTable: "Rozwiazania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentarze_Uzytkownicy_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oceny",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Wartosc = table.Column<int>(type: "INTEGER", nullable: false),
                    DataDodania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RozwiazanieId = table.Column<int>(type: "INTEGER", nullable: false),
                    AutorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oceny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oceny_Rozwiazania_RozwiazanieId",
                        column: x => x.RozwiazanieId,
                        principalTable: "Rozwiazania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oceny_Uzytkownicy_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dzialy_RodzicId",
                table: "Dzialy",
                column: "RodzicId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentarze_AutorId",
                table: "Komentarze",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentarze_RozwiazanieId",
                table: "Komentarze",
                column: "RozwiazanieId");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_AutorId",
                table: "Oceny",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_RozwiazanieId_AutorId",
                table: "Oceny",
                columns: new[] { "RozwiazanieId", "AutorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rozwiazania_AutorId",
                table: "Rozwiazania",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rozwiazania_ZadanieId",
                table: "Rozwiazania",
                column: "ZadanieId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadania_AutorId",
                table: "Zadania",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadania_DzialId",
                table: "Zadania",
                column: "DzialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentarze");

            migrationBuilder.DropTable(
                name: "Oceny");

            migrationBuilder.DropTable(
                name: "Rozwiazania");

            migrationBuilder.DropTable(
                name: "Zadania");

            migrationBuilder.DropTable(
                name: "Dzialy");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");
        }
    }
}
