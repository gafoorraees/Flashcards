using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Validation
    {
        public static string ValidString(string input)
        {
            int testNumeric;

            while (string.IsNullOrEmpty(input) || int.TryParse(input, out testNumeric))
            {
                Console.WriteLine("Invalid input. Please enter a valid input consisting of letters only.");
                input = Console.ReadLine();
            }

            return input;
        }
    }
}
