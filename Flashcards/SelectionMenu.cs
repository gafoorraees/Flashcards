using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class SelectionMenu
    {
        internal void Menu()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("Choose");
                Console.WriteLine("Type 1 to Create a stack");
                Console.WriteLine("Type 2 to Create a flashcard");
                Console.WriteLine("Type 3 to view all stacks");
                Console.WriteLine("Type 4 to close the application");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        UserInput.CreateStack();
                        break;
                    case "2":
                        UserInput.CreateFlashcard();
                        break;
                    case "3":
                        UI.DisplayStacks();
                        break;
                    case "4":
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                }

            }
        }

    }
}
