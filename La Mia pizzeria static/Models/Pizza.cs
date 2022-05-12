using System.ComponentModel.DataAnnotations;

namespace La_Mia_pizzeria_static.Models
{
    public class Pizza
    {
        [Required(ErrorMessage ="Il campo nome è obbligatorio")]
        [StringLength(20, ErrorMessage ="Il nome non può avere più di 20 caratteri")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage ="Il campo ingredienti è obbligatorio")]
        public string Ingrediente { get; set; }
        
        [Required(ErrorMessage ="L'URL dell'immagine è obbligatoria")]
        public string Image { get; set; }
        public double Prezzo { get; set; }

        public Pizza()
        {

        }

        public Pizza(string nome, string ingrediente, string image, double prezzo)
        {
            Nome = nome;
            Ingrediente = ingrediente;
            Image = image;
            Prezzo = prezzo;
        }
    }
}
