using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace La_Mia_pizzeria_static.Migrations
{
    public partial class aggiuntoIngredienti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingrediente",
                table: "Pizze");

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ingrediente = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientePizza",
                columns: table => new
                {
                    IngredientiId = table.Column<int>(type: "int", nullable: false),
                    ListaPizzeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientePizza", x => new { x.IngredientiId, x.ListaPizzeId });
                    table.ForeignKey(
                        name: "FK_IngredientePizza_Ingrediente_IngredientiId",
                        column: x => x.IngredientiId,
                        principalTable: "Ingrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientePizza_Pizze_ListaPizzeId",
                        column: x => x.ListaPizzeId,
                        principalTable: "Pizze",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientePizza_ListaPizzeId",
                table: "IngredientePizza",
                column: "ListaPizzeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientePizza");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.AddColumn<string>(
                name: "Ingrediente",
                table: "Pizze",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
