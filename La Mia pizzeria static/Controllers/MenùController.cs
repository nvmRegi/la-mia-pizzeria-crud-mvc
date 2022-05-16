using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using La_Mia_pizzeria_static.Data;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace La_Mia_pizzeria_static.Controllers
{
    public class MenùController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            //Operazione read
            using (MenùContext db = new MenùContext())
            {
                mymodel.Pizza = db.Pizze.ToList<Pizza>();
                mymodel.Ingrediente = db.Ingrediente.FromSqlRaw("SELECT ingrediente FROM Ingrediente JOIN");
            }
            return View(mymodel);
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



