using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using La_Mia_pizzeria_static.Utils;

namespace La_Mia_pizzeria_static.Controllers
{
    public class MenùController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> pizze = MenùData.GetPizze();
            return View(pizze);
        }

        [HttpGet]
        public IActionResult Dettagli(string nome)
        {
            Pizza pizzaFound = GetPizzaByNome(nome);

            if(pizzaFound != null)
            {
                return View("Dettagli", pizzaFound);
            }
            else
            {
                return NotFound("La pizza " + nome + "non è stato trovata");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("FormPizza");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza nuovaPizza)
        {
            if(!ModelState.IsValid)
            {
                return View("FormPost", nuovaPizza);
            }

            Pizza nuovaPizzaConNome = new Pizza(nuovaPizza.Nome, nuovaPizza.Ingrediente, nuovaPizza.Image, nuovaPizza.Prezzo);
            
            MenùData.GetPizze().Add(nuovaPizzaConNome);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Aggiorna(string nome)
        {
            Pizza pizzaToEdit = GetPizzaByNome(nome);

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                return View("Aggiorna", pizzaToEdit);
            }
        }

        [HttpPost]
        public IActionResult Aggiorna(string nome, Pizza model)
        {
            if (!ModelState.IsValid)
            {
                return View("Aggiorna", model);
            }

            Pizza pizzaOriginal = GetPizzaByNome(nome);

            if (pizzaOriginal != null)
            {
                pizzaOriginal.Nome = model.Nome;
                pizzaOriginal.Ingrediente = model.Ingrediente;
                pizzaOriginal.Image = model.Image;
                pizzaOriginal.Prezzo = model.Prezzo;
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        private Pizza GetPizzaByNome(string nome)
        {
            Pizza pizzaFound = null;

            foreach (Pizza pizza in MenùData.GetPizze())
            {
                if (pizza.Nome == nome)
                {
                    pizzaFound = pizza;
                    break;
                }
            }
            return pizzaFound;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Elimina(string nome)
        {
            int indiceDaRimuovere = -1;

            List<Pizza> listaPizze = MenùData.GetPizze();
            for(int i = 0; i < listaPizze.Count; i++)
            {
                if(listaPizze[i].Nome == nome)
                {
                    indiceDaRimuovere = i;
                }
            }

            if(indiceDaRimuovere >= 0)
            {
                MenùData.GetPizze().RemoveAt(indiceDaRimuovere);
                
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}



