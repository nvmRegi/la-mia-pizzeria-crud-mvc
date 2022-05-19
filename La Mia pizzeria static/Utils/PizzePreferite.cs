namespace La_Mia_pizzeria_static.Utils
{
    public class PizzePreferite
    {
        private static List<int> ListaPizzeConId = new List<int>();
        public PizzePreferite() { }

        public static List<int> IDPizze => ListaPizzeConId;
    }
}
