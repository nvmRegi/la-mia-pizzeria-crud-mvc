using System.ComponentModel.DataAnnotations;

namespace La_Mia_pizzeria_static.Models
{
    public class Ingrediente
    {
        [Key]
        public int Id { get; set; }
        public string nome { get; set; }

        public List<Pizza> ListaPizze { get; set; }

        public Ingrediente() { }
    }
}
