using La_Mia_pizzeria_static.Data;
using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using La_Mia_pizzeria_static.Utils;

namespace La_Mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<Pizza> pizzePreferite = new List<Pizza>();
            List<int> idPizze = PizzePreferite.IDPizze;
            if(idPizze.Count == 0 )
            {
                return Ok(pizzePreferite);
            }
            using (MenùContext db = new MenùContext())
            {
                for(int i = 0; i < idPizze.Count + 1; i++)
                {
                    Pizza daCercare = db.Pizze.FromSqlRaw($"SELECT * FROM Pizze WHERE Id = {idPizze[i]}").First();
                    pizzePreferite.Add(daCercare);
                    foreach (Pizza pizza in pizzePreferite)
                    {
                        int id = pizza.Id;
                        pizza.Ingredienti = db.Ingrediente.FromSqlRaw($"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}").ToList<Ingrediente>();
                    }
                }

            }
            return Ok(pizzePreferite);
        }

        [HttpPost]
        public IActionResult aggiungiPreferito(int idPizza)
        {
            List<int> idPizze = PizzePreferite.IDPizze;
            idPizze.Add(idPizza);
            return Ok(idPizze);
        }
    }
}
