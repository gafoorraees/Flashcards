using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.Models;

namespace Flashcards
{
    internal class UserInput
    {
        public void CreateStack()
        {
            string stackName = GetStackName("Enter the stack name: ");

            Stack stack = new Stack()
            {
                Name = stackName
            };
            
        }

        public void CreateFlashcard()
        {
            string question = GetQuestion("Enter the question: ");
            string answer = GetAnswer("Enter the answer: ");
            // which stack should this card go to? list stacks with displayid. insert into that stack

            Flashcard flashcard = new Flashcard()
            {
                Question = question,
                Answer = answer
            };

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
