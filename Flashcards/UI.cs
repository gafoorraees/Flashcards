using Flashcards.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public static void IndividualStacks()
        {
            var menu = new SelectionMenu();
            
            DisplayStacks();
           
            Console.WriteLine("Please type in the name of the stack that you would like to view");
            var stackName = Console.ReadLine().Trim();
            var stackId = Stacks.ReturnStackID(stackName);

            DisplayFlashcards(stackId);

            Console.WriteLine("Would you like to modify the cards in this stack?");
            Console.WriteLine("1. Add flashcard to stack.");
            Console.WriteLine("2. Update flashcard in stack.");
            Console.WriteLine("3. Delete flashcard in stack.");
            Console.WriteLine("4. Change to another stack.");
            Console.WriteLine("5. Return to Main Menu.");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        UserInput.CreateFlashcard(stackId);
                        break;
                }
            }
        }

        public static void ManageStacks()
        {
            var menu = new SelectionMenu();

            Console.WriteLine("Manage Stacks:");
            Console.WriteLine("1. Create Stack");
            Console.WriteLine("2. Delete Stack");
            Console.WriteLine("3. View Individual Stacks");
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
                        IndividualStacks();
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
        public static void DisplayFlashcards()
        {
            Console.WriteLine("Enter the stack name for which you would like to see flashcards:");
            DisplayStacks();

            var stackName = Console.ReadLine().Trim();
            var stackID = Stacks.ReturnStackID(stackName);
            
            var flashcards = FlashcardsTable.GetFlashcardsFromStack(stackID);

            if (flashcards.Count == 0)
            {
                Console.WriteLine("No available flascards in this stack. Please add flashcards first.");
                return;
            }

            Console.WriteLine($"Flashcards available in the stack '{stackName}':");

            foreach (var flashcard in flashcards)
            {
                Console.WriteLine($"ID: {flashcard.DisplayID}. Question: {flashcard.Question}. Answer: {flashcard.Answer}");
            }  
        }

        public static void DisplayFlashcards(int stackId)
        {
            var flashcards = FlashcardsTable.GetFlashcardsFromStack(stackId);
            var stackName = Stacks.ReturnStackName(stackId);

            if (flashcards.Count == 0)
            {
                Console.WriteLine("No available flascards in this stack. Please add flashcards first.");
                return;
            }

            Console.WriteLine($"Flashcards available in the stack '{stackName}':");

            foreach (var flashcard in flashcards)
            {
                Console.WriteLine($"ID: {flashcard.DisplayID}. Question: {flashcard.Question}. Answer: {flashcard.Answer}");
            }
        }

        public static void ManageFlashcards()
        {
            var menu = new SelectionMenu();

            
        }
    }
}
