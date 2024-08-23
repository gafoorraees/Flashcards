using Flashcards.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class UI
    {
        public static void DisplayStacks()
        {
            var stacks = Stacks.GetAllStacks();

            if (stacks.Count == 0)
            {
                Console.WriteLine("No available stacks. Please create a stack first.");
                return;
            }

            Console.WriteLine("Available stacks:");

            foreach (var stack in stacks)
            {
                Console.WriteLine($"Name: {stack.Name}");

            }
        }

        public static void ManageStacks()
        {
            var menu = new SelectionMenu();

            Console.WriteLine("Manage Stacks:");
            Console.WriteLine("1. Create Stack");
            Console.WriteLine("2. Delete Stack");
            Console.WriteLine("3. View All Stacks");
            Console.WriteLine("4. Go Back");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        UserInput.CreateStack();
                        break;
                    case 2:
                        Stacks.DeleteStack();
                        break;
                    case 3:
                        DisplayStacks();
                        break;
                    case 4:
                        menu.Menu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        ManageStacks();
                        break;
                }
            }
        }

        public static void ManageFlashcards()
        {
            var menu = new SelectionMenu();
        }
    }
}
