﻿using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using La_Mia_pizzeria_static.Data;

namespace La_Mia_pizzeria_static.Controllers
{
    public class MenùController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            //Operazione read
            List<Pizza> pizze = new List<Pizza>();

            using (MenùContext db = new MenùContext())
            {
                pizze = db.Pizze.ToList<Pizza>();
            }
            return View(pizze);
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
                Pizza nuovaPizzaConNome = new Pizza(nuovaPizza.Nome, nuovaPizza.Ingrediente, nuovaPizza.Image, nuovaPizza.Prezzo);

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
                    pizzaToEdit.Ingrediente = model.Ingrediente;
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



