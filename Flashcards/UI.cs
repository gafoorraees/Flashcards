using Flashcards.Tables;
using Spectre.Console;

namespace Flashcards
{
    internal class UI
    {
        public static void DisplayStacks()
        {
            var menu = new SelectionMenu();
            var stacks = Stacks.GetAllStacks();

            if (stacks.Count == 0)
            {
                Console.WriteLine("No available stacks. Please create a stack first. Press Enter to return the main menu.\n");
                Console.ReadLine();
                menu.Menu();
            }

            AnsiConsole.Write(new Markup("[bold underline]Available Stacks[/]\n"));

            var table = new Table();

            table.AddColumn("Stack");

            table.Border = TableBorder.Ascii;
            table.ShowHeaders = true;
            table.BorderColor(Color.Grey);
            table.ShowFooters = false;
            table.ShowRowSeparators();

            foreach (var stack in stacks)
            { 
                table.AddRow(stack.Name);
        
            }

            AnsiConsole.Write(table);
        }

        public static void IndividualStacks()
        {
            Console.Clear();
            var menu = new SelectionMenu();

            DisplayStacks();

            Console.WriteLine("Please type in the name of the stack that you would like to view\n");

            var stackName = Console.ReadLine().Trim();
            var stackId = Stacks.ReturnStackID(stackName);

            if (stackId == 0)
            {
                Console.WriteLine("Stack not found. Please try again.\n");
                IndividualStacks();
                return;
            }

            DisplayFlashcards(stackId);

            while (true)
            { 
                Console.WriteLine("\nWould you like to modify the cards in this stack? Choose a number below:\n");
                Console.WriteLine("1. Add flashcard to stack.");
                Console.WriteLine("2. Update flashcard in stack.");
                Console.WriteLine("3. Delete flashcard in stack.");
                Console.WriteLine("4. Change to another stack.");
                Console.WriteLine("5. Return to Main Menu.");

                string input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.\n");
                    continue;
                }

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            UserInput.CreateFlashcard(stackId);
                            break;
                        case 2:
                            FlashcardsTable.UpdateFlashcard(stackId);
                            break;
                        case 3:
                            FlashcardsTable.DeleteFlashcard(stackId);
                            break;
                        case 4:
                            IndividualStacks();
                            return;
                        case 5:
                            menu.Menu();
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try agian.\n");
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.\n");
                }
           }
        }

        public static void ManageStacks()
        {
            Console.Clear();
            var menu = new SelectionMenu();

            Console.WriteLine("Manage Stacks:\n");
            Console.WriteLine("1. Create Stack");
            Console.WriteLine("2. Delete Stack");
            Console.WriteLine("3. View Individual Stacks");
            Console.WriteLine("4. Return to Main Menu");

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
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        ManageStacks();
                        break;
                }
            }
        }
        public static void DisplayFlashcards()
        {
            Console.Clear();
            Console.WriteLine("Enter the stack name for which you would like to see flashcards:\n");
            DisplayStacks();

            var stackName = Console.ReadLine().Trim();
            var stackID = Stacks.ReturnStackID(stackName);
            
            var flashcards = FlashcardsTable.GetFlashcardsFromStack(stackID);

            if (flashcards.Count == 0)
            {
                Console.WriteLine("\nNo available flascards in this stack. Please add flashcards first.\n");
                return;
            }

            AnsiConsole.Write(new Markup($"[bold underline]Flashcards for stack: {stackName}[/]\n"));

            var table = new Table();

            table.AddColumn(new TableColumn("[bold]ID[/]"));
            table.AddColumn(new TableColumn("[bold]Question[/]"));
            table.AddColumn(new TableColumn("[bold]Answer[/]"));

            table.Border(TableBorder.Ascii);
            table.ShowHeaders = true;
            table.BorderColor(Color.Grey);
            table.ShowFooters = false;

            foreach (var flashcard in flashcards)
            {
                table.AddRow(
                    flashcard.DisplayID.ToString(),
                    flashcard.Question,
                    flashcard.Answer
                );
            }

            AnsiConsole.Write(table);
        }

        public static void DisplayFlashcards(int stackId)
        {
            Console.Clear();
            var flashcards = FlashcardsTable.GetFlashcardsFromStack(stackId);
            var stackName = Stacks.ReturnStackName(stackId);

            if (flashcards.Count == 0)
            {
                Console.WriteLine("No available flascards in this stack. Please add flashcards first.\n");
                return;
            }

            AnsiConsole.Write(new Markup($"[bold underline]Flashcards for stack: {stackName}[/]\n"));

            var table = new Table();

            table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Question[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Answer[/]").Centered());

            table.Border(TableBorder.Ascii);
            table.ShowHeaders = true;
            table.BorderColor(Color.Grey);
            table.ShowFooters = false;

            foreach (var flashcard in flashcards)
            {
                table.AddRow(
                    flashcard.DisplayID.ToString(),
                    flashcard.Question,
                    flashcard.Answer
 );
            }

            AnsiConsole.Write(table);
        }

        public static void ManageFlashcards()
        {
            Console.Clear();
            IndividualStacks();
        }
    }
}
