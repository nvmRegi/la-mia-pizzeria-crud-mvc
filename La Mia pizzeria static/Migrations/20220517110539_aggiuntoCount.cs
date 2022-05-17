using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace La_Mia_pizzeria_static.Migrations
{
    public partial class aggiuntoCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Pizze",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Pizze");
        }
    }
}
