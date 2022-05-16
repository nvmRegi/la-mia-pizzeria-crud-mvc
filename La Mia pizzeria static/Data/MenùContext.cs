using La_Mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace La_Mia_pizzeria_static.Data
{
    public class MenùContext : DbContext
    {
        public DbSet<Pizza> Pizze { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=la_mia_pizzeria;Integrated Security=True");
        }

    }
}
