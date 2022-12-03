using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome, ImagemURL) Values('Bebidas', 'bebidas.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemURL) Values('Lanches', 'lanches.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemURL) Values('Sobremesas', 'sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
