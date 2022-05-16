using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using La_Mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace La_Mia_pizzeria_static.Controllers
{
    public class MenùController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> Pizze = new List<Pizza>();
            //List<Ingrediente> Ingredienti = new List<Ingrediente>();
            //Operazione read
            using (MenùContext db = new MenùContext())
            {
                Pizze = db.Pizze.ToList<Pizza>();
                foreach(Pizza pizza in Pizze)
                {
                    int id = pizza.Id;
                    pizza.Ingredienti = db.Ingrediente.FromSqlRaw($"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}").ToList<Ingrediente>();
                }
            }
            return View(Pizze);
        }

        [HttpGet]
        public IActionResult Dettagli(int id)
        {
            using(MenùContext db = new MenùContext())
            {
                try
                {
                    Pizza pizzaFound = db.Pizze
                        .Where(pizza => pizza.Id == id)
                        .First();
                    pizzaFound.Ingredienti = db.Ingrediente.FromSqlRaw($"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}").ToList<Ingrediente>();
                    
                    return View("Dettagli", pizzaFound);
                }catch(InvalidOperationException ex)
                {
                    return NotFound("La pizza non è stata trovata");
                } catch(Exception ex)
                {
                    return BadRequest();
                }
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
                return View("FormPizza", nuovaPizza);
            }

            using(MenùContext db = new MenùContext())
            {
                Pizza nuovaPizzaConNome = new Pizza(nuovaPizza.Nome, nuovaPizza.Image, nuovaPizza.Prezzo);

                db.Pizze.Add(nuovaPizzaConNome);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Aggiorna(int id)
        {
            Pizza pizzaToEdit = null;

            using(MenùContext db = new MenùContext())
            {
                pizzaToEdit = db.Pizze
                    .Where(pizza => pizza.Id == id)
                    .First();
            }

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                return View("aggiorna", pizzaToEdit);
            }
        }

        [HttpPost]
        public IActionResult Aggiorna(int id, Pizza model)
        {
            if (!ModelState.IsValid)
            {
                return View("Aggiorna", model);
            }

            Pizza pizzaToEdit = null;

            using(MenùContext db = new MenùContext())
            {
                pizzaToEdit = db.Pizze
                    .Where(pizza => pizza.Id == id)
                    .First();

                if(pizzaToEdit != null)
                {
                    pizzaToEdit.Nome = model.Nome;
                    pizzaToEdit.Ingredienti = model.Ingredienti;
                    pizzaToEdit.Image = model.Image;
                    pizzaToEdit.Prezzo = model.Prezzo;

                    db.SaveChanges();

                    return RedirectToAction("index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Elimina(int id)
        {
            using(MenùContext db = new MenùContext())
            {
                Pizza pizzaToDelete = db.Pizze
                    .Where(pizza => pizza.Id == id)
                    .First();
                
                if(pizzaToDelete != null)
                {
                    db.Pizze.Remove(pizzaToDelete);
                    db.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}



