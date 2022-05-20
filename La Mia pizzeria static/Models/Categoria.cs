using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace La_Mia_pizzeria_static.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Il campo è obbligatorio")]
        [StringLength(75, ErrorMessage = "Il nome della categoria non può superare i 75 caratteri")]
        public string NomeCategoria { get; set; }

        [JsonIgnore]
        public List<Pizza> pizze { get; set; }

        public Categoria() { }
    }
}
