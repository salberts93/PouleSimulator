using PouleSimulator.App.Menus;

namespace PouleSimulator.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            int userInput = 0;

            while (userInput != 5)
            {
                userInput = mainMenu.Display();
                mainMenu.NavigateTo(userInput);
            }
        }
    }
}
