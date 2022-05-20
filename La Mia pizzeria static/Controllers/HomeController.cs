using La_Mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace La_Mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Menù()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PizzePreferite()
        {
            return View("PizzePreferite");
        }
    }
}