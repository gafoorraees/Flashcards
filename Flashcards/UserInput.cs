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

        public static void UpdateStack()
        {

        }

        public static void CreateFlashcard()
        {
            UI.DisplayStacks();

            Console.WriteLine("Please type in the name of the stack that you want to add a flashcard to:");
            string stackAdd = Console.ReadLine().Trim();
            int stackId = Stacks.ReturnStackID(stackAdd);

            string question = GetQuestion("Enter the question: ", false);
            string answer = GetAnswer("Enter the answer: ", false);

            Flashcard flashcard = new Flashcard()
            {
                Question = question,
                Answer = answer,
                StackID = stackId
                
            };

            FlashcardsTable.InsertFlashcard(question, answer, stackId);

        }

        public static void CreateFlashcard(int stackId)
        {
            string question = GetQuestion("Enter the question: ", false);
            string answer = GetAnswer("Enter the answer: ", false);

            Flashcard flaschard = new Flashcard()
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

        public static string GetQuestion(string prompt, bool isUpdate, string currentQuestion = "")
        {
            Console.WriteLine(isUpdate ?
                $"{prompt} (current: {currentQuestion}, press Enter to keep current):" :
                prompt);

            string question = Console.ReadLine();
            
            if (!isUpdate)
            {
                Validation.ValidString(question);
            }

            return string.IsNullOrEmpty(question) ? currentQuestion : question;
        }

        public static string GetAnswer(string prompt, bool isUpdate, string currentAnswer = "")
        {
            Console.WriteLine(isUpdate ?
                $"{prompt} (current: {currentAnswer}, press Enter to keep current):" :
                prompt);

            string answer = Console.ReadLine();

            if (!isUpdate)
            {
                Validation.ValidString(answer);
            }

            return string.IsNullOrEmpty(answer) ? currentAnswer : answer;
        }
    }
}
