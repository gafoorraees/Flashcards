using Flashcards.Tables;
using Flashcards.Models;
using System.Diagnostics.Eventing.Reader;


namespace Flashcards
{
    internal class UserInput
    {
        public static void CreateStack()
        {
            Console.Clear();

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

        public static void CreateFlashcard(int stackId)
        {
            Console.Clear();

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

            return string.IsNullOrEmpty(answer) ? currentAnswer : answer;
        }
    }
}
