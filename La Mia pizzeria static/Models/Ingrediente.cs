using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace La_Mia_pizzeria_static.Models
{
    public class Ingrediente
    {
        [Key]
        public int Id { get; set; }
        public string nome { get; set; }

        //Foreign Key
        [JsonIgnore]
        public List<Pizza> ListaPizze { get; set; }
        public Ingrediente() { }

        public Ingrediente(string nome)
        {
            this.nome = nome;
        }
    }
}
