using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using La_Mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace La_Mia_pizzeria_static.Controllers
{
    public class MenùController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> Pizze = new List<Pizza>();
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
            using (MenùContext db = new MenùContext())
            {
                List<Categoria> categories = db.Categoria.ToList();

                PizzaCategoria model = new PizzaCategoria();
                model.Pizza = new Pizza();
                model.Categorias = categories;
                return View("FormPizza", model);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategoria data)
        {
            if(!ModelState.IsValid)
            {
                using (MenùContext db = new MenùContext())
                {
                    List<Categoria> categories = db.Categoria.ToList();
                    data.Categorias = categories;
                }
                return View("FormPizza", data);
            }

            using(MenùContext db = new MenùContext())
            {
                List<Ingrediente> ingredientiPizza = data.Pizza.Ingredienti;

                Pizza nuovaPizzaConNome = new Pizza();
                nuovaPizzaConNome.Nome = data.Pizza.Nome;
                nuovaPizzaConNome.Ingredienti = data.Pizza.Ingredienti;
                nuovaPizzaConNome.Image = data.Pizza.Image;
                nuovaPizzaConNome.Prezzo = data.Pizza.Prezzo;
                nuovaPizzaConNome.Categoria = data.Pizza.Categoria;
                db.Pizze.Add(nuovaPizzaConNome);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Aggiorna(int id)
        {
            Pizza pizzaToEdit = null;
            List<Categoria> categorias = new List<Categoria>();

            using(MenùContext db = new MenùContext())
            {
                pizzaToEdit = db.Pizze
                    .Where(pizza => pizza.Id == id)
                    .First();
                pizzaToEdit.Ingredienti = db.Ingrediente.FromSqlRaw($"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}").ToList<Ingrediente>();
                categorias = db.Categoria.ToList<Categoria>();
            }

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                PizzaCategoria model = new PizzaCategoria();
                model.Pizza = pizzaToEdit;
                model.Categorias = categorias;
                return View("Aggiorna", model);
            }
        }

        [HttpPost]
        public IActionResult Aggiorna(int id, PizzaCategoria model)
        {
            if (!ModelState.IsValid)
            {
                using(MenùContext db = new MenùContext())
                {
                    List<Categoria> categorias = db.Categoria.ToList();
                    model.Categorias = categorias;
                }
                return View("Aggiorna", model);
            }

            Pizza pizzaToEdit = null;
            
            using(MenùContext db = new MenùContext())
            {
                pizzaToEdit = db.Pizze
                    .Where(pizza => pizza.Id == id)
                    .First();
                pizzaToEdit.Ingredienti = db.Ingrediente.FromSqlRaw($"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}").ToList<Ingrediente>();

                if (pizzaToEdit != null)
                {
                    pizzaToEdit.Nome = model.Pizza.Nome;
                    pizzaToEdit.Image = model.Pizza.Image;
                    pizzaToEdit.Prezzo = model.Pizza.Prezzo;                    
                    pizzaToEdit.Ingredienti = model.Pizza.Ingredienti;


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

        [HttpPost]
        public IActionResult AggiungiIngredienteAllaLista(string ingrediente)
        {
            using (MenùContext db = new MenùContext())
            {
                Ingrediente nuovoIngrediente = new Ingrediente(ingrediente);

                List<Ingrediente> ingredientiDaDB = db.Ingrediente.ToList();

                if (!ingredientiDaDB.Contains(nuovoIngrediente))
                {
                    db.Ingrediente.Add(nuovoIngrediente);
                    db.SaveChanges();

                    return RedirectToAction("FormPizza");
                }
                return RedirectToAction("FormPizza");
            }
            //List<Ingrediente> listaIngredienti;
            //Pizza pizzaToEdit = null;
            //using (MenùContext db = new MenùContext())
            //{
            //    pizzaToEdit = db.Pizze.Where(pizza => pizza.Id == id);
            //    string sql = $"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}";
            //    listaIngredienti = db.Ingrediente.FromSqlRaw(sql).ToList<Ingrediente>();
                
            //    if(listaIngredienti == null)
            //    {
                    
            //    }

            //    //if (!listaIngredienti)
            //    //{
            //    //    listaIngredienti.Add(ingrediente);
            //    //}
            //}
        }
    }
}



