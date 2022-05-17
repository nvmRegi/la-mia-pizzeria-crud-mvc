using La_Mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;

namespace La_Mia_pizzeria_static.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Il campo nome è obbligatorio")]
        [StringLength(20, ErrorMessage ="Il nome non può avere più di 20 caratteri")]
        public string Nome { get; set; }

        //[Required(ErrorMessage = "Il campo ingredienti è obbligatorio")]
        ////[Column(TypeName="text")]
        public List<Ingrediente> Ingredienti { get; set; }

        [Required(ErrorMessage ="L'URL dell'immagine è obbligatoria")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il campo prezzo è obbligatorio")]
        public double Prezzo { get; set; }

        public Pizza() { }

        public Pizza(string nome, List<Ingrediente> ingredienti, string image, double prezzo)
        {
            this.Nome = nome;
            this.Ingredienti = ingredienti;
            this.Image = image;
            this.Prezzo = prezzo;
        }
    }
}
