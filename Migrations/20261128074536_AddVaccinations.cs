using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatiTakip.Migrations
{
    /// <inheritdoc />
    public partial class AddVaccinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PetId = table.Column<int>(type: "INTEGER", nullable: false),
                    VaccineName = table.Column<string>(type: "TEXT", nullable: false),
                    DateAdministered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NextDoseDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccinations_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_PetId",
                table: "Vaccinations",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaccinations");
        }
    }
}
