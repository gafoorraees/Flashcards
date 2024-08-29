using Flashcards.Tables;
using Spectre.Console;
using Spectre.Console;

namespace Flashcards
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseSetup.EnsureDatabaseSetup();

            var menu = new SelectionMenu();
          
            menu.Menu();
        }
    }
}