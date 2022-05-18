using Microsoft.AspNetCore.Mvc;

namespace La_Mia_pizzeria_static.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
