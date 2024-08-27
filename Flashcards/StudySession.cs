using Dapper;
using Flashcards.Models;
using Flashcards.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.Tables;

namespace Flashcards
{
    public class StudySession
    {
        public static void StartStudySession()
        {
            Console.WriteLine("Please enter the name of the stack that you would like to study: ");
            UI.DisplayStacks();

            var stackName = Console.ReadLine().Trim();
            var stackID = Stacks.ReturnStackID(stackName);

            if (stackID == 0)
            {
                Console.WriteLine("Stack not found. Please try again.\n");
                StartStudySession();
                return;
            }

            List<Flashcard> flashcards = FlashcardsTable.GetFlashcardsFromStack(stackID);
            List<FlashcardDTO> flashcardDTOS = flashcards.Select(f => MapToDTO(f)).ToList();

            var studySession = new StudySessionDTO
            {
                StackID = stackID,
                Date = DateTime.Now,
                Score = 0
            };

            if (flashcardDTOS.Count == 0)
            {
                Console.WriteLine("No flashcards available in this stack. Please add flashcards first.");
                return;
            }

            foreach (var flashcard in flashcardDTOS)
            {
                Console.WriteLine($"Question: {flashcard.Question}");

                Console.WriteLine("Your answer: ");
                string userAnswer = Console.ReadLine().Trim();

                if (string.Equals(userAnswer, flashcard.Answer, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Correct!");
                    studySession.Score++;
                }
                else
                {
                    Console.WriteLine($"Incorrect. The correct answer is: {flashcard.Answer}");
                }
            }

            Console.WriteLine($"You have completed all cards for this stack! Your score: {studySession.Score}/{flashcardDTOS.Count}");

            StudySessions.SaveStudySession(studySession);
        }

        public static FlashcardDTO MapToDTO(Flashcard flashcard)
        {
            return new FlashcardDTO
            {
                Question = flashcard.Question,
                Answer = flashcard.Answer
            };
        }
    }
}
