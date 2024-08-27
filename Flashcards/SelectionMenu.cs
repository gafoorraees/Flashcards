using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.Tables;

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
                Console.WriteLine("Type 1 to Manage Stacks");
                Console.WriteLine("Type 2 to Manage Flashcards");
                Console.WriteLine("Type 3 to Start a Study Session");
                Console.WriteLine("Type 4 to close the application");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        UI.ManageStacks();
                        break;
                    case "2":
                        UI.ManageFlashcards();
                        break;
                    case "3":
                        StudySession.StartStudySession();
                        break;
                    case "4":
                        Stacks.DeleteStack();
                        break;
                    case "5":
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                }

            }
        }

    }
}
