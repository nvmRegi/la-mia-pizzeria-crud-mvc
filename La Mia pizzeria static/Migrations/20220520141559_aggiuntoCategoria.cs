using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace La_Mia_pizzeria_static.Migrations
{
    public partial class aggiuntoCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Pizze",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizze_CategoriaId",
                table: "Pizze",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizze_Categoria_CategoriaId",
                table: "Pizze",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizze_Categoria_CategoriaId",
                table: "Pizze");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Pizze_CategoriaId",
                table: "Pizze");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Pizze");
        }
    }
}
