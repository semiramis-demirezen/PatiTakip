using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatiTakip.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Appointments",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Appointments");
        }
    }
}
