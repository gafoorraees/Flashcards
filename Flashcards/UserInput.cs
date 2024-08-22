using Flashcards.Tables;
using Flashcards.Models;
using System.Diagnostics.Eventing.Reader;


namespace Flashcards
{
    internal class UserInput
    {
        public static void CreateStack()
        {
            var stackList = Stacks.GetAllStacks();
            string stackName;

            while (true)
            {
                stackName = GetStackName("Enter the stack name: ");

                if (stackList.Any(stack => stack.Name.Equals(stackName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("You have already created a stack with that name. Please choose a different name.");
                }
                else
                {
                    break;
                }
            }

            Stack stack = new Stack()
            {
                Name = stackName
            };

            Stacks.InsertStack(stackName);
        }

        public static void CreateFlashcard()
        {
            UI.DisplayStacks();

            Console.WriteLine("Please type in the name of the stack that you want to add a flashcard to:");
            string stackAdd = Console.ReadLine().Trim();
            int stackId = Stacks.ReturnStackID(stackAdd);

            string question = GetQuestion("Enter the question: ");
            string answer = GetAnswer("Enter the answer: ");

            Flashcard flashcard = new Flashcard()
            {
                Question = question,
                Answer = answer,
                StackID = stackId
                
            };

            FlashcardsTable.InsertFlashcard(question, answer, stackId);

        }
        internal static string GetStackName(string prompt)
        {
            Console.WriteLine(prompt);
            string stack = Validation.ValidString(Console.ReadLine());

            return stack;
        }

        internal static string GetQuestion(string prompt)
        {
            Console.WriteLine(prompt);
            string question = Validation.ValidString(Console.ReadLine());

            return question;
        }

        internal static string GetAnswer(string prompt)
        {
            Console.WriteLine(prompt);
            string answer = Validation.ValidString(Console.ReadLine());

            return answer;
        }
    }
}
