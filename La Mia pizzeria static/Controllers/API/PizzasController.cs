using La_Mia_pizzeria_static.Data;
using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace La_Mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]")] //per accedere all'API
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string? search) //ho creato una web API
        {
            List<Pizza> Pizze = new List<Pizza>();
            //Operazione read
            using (MenùContext db = new MenùContext())
            {
                if(search != null)
                {
                    Pizze = db.Pizze.Where(pizza => pizza.Nome.Contains(search)).ToList();
                    foreach (Pizza pizza in Pizze)
                    {
                        int id = pizza.Id;
                        pizza.Ingredienti = db.Ingrediente.FromSqlRaw($"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}").ToList<Ingrediente>();
                    }
                }
                else
                {
                    Pizze = db.Pizze.ToList<Pizza>();
                    foreach (Pizza pizza in Pizze)
                    {
                        int id = pizza.Id;
                        pizza.Ingredienti = db.Ingrediente.FromSqlRaw($"SELECT * FROM Ingrediente JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId WHERE ListaPizzeId = {id}").ToList<Ingrediente>();
                    }
                }
            }
            return Ok(Pizze); //lista di pizze in json
        }
    }
}
