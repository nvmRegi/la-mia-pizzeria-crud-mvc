using La_Mia_pizzeria_static.Models;

namespace La_Mia_pizzeria_static.Utils
{
    public class PizzePreferite
    {
        private static List<FavouritesDataInput> ListaPizzeConId = new List<FavouritesDataInput>();
        public PizzePreferite() { }

        public static List<FavouritesDataInput> IDPizze => ListaPizzeConId;
    }
}
