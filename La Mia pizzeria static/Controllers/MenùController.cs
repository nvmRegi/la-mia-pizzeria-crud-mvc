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
            Pizza pizzaFound = null;

            foreach(Pizza pizza in MenùData.GetPizze())
            {
                if(pizza.Nome == nome)
                {
                    pizzaFound = pizza;
                    break;
                }
            }

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
    }


}
