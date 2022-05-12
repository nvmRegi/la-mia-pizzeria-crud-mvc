using La_Mia_pizzeria_static.Models;

namespace La_Mia_pizzeria_static.Utils
{
    public static class MenùData
    {
        private static List<Pizza> pizze;

        public static List<Pizza> GetPizze()
        {
            if(MenùData.pizze != null)
            {
                return MenùData.pizze;
            }
            List<Pizza> nuovaListaPizza = new List<Pizza>();
            //Pizze default
            Pizza pizza = new Pizza("Marinara", "Pomodoro - origano - aglio - olio", "./img/pizza-marinara.jpg", 4);
            nuovaListaPizza.Add(pizza);

            Pizza pizza1 = new Pizza("Margherita", "Pomodoro - mozzarella", "./img/pizza-g7672da356_640.jpg", 5);
            nuovaListaPizza.Add(pizza1);

            Pizza pizza2 = new Pizza("Funghi", "Pomodoro - mozzarella - funghi champignon", "./img/pizza-funghi.jpg", 6);
            nuovaListaPizza.Add(pizza2);

            MenùData.pizze = nuovaListaPizza;
            return MenùData.pizze;
        }
    }
}
