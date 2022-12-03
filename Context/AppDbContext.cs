using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace APICatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Password=Andritzsin1*;Persist Security Info=True;User ID=sa;Initial Catalog=APICatalogo;Data Source=POAN21120086");
        //}


    }
}
