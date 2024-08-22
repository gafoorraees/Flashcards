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
    }
}
