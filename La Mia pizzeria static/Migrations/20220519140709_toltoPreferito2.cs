using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace La_Mia_pizzeria_static.Migrations
{
    public partial class toltoPreferito2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preferito",
                table: "Pizze");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Preferito",
                table: "Pizze",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
