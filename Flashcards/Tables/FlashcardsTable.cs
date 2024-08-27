using System.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Flashcards.Models;
using System.Reflection.Metadata.Ecma335;

namespace Flashcards.Tables
{
    public class FlashcardsTable
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        public static void InsertFlashcard(string question, string answer, int stackId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string getMaxDisplayIdQuery = "SELECT ISNULL(MAX(DisplayID), 0) + 1 FROM Flashcards WHERE StackID = @StackID";
                var displayId = connection.QuerySingle<int>(getMaxDisplayIdQuery, new { StackID = stackId });

                string insertQuery = @"INSERT INTO Flashcards (Question, Answer, StackID, DisplayID)
                                       VALUES (@Question, @Answer, @StackID, @DisplayID)";

                var flashcardParameters = new
                {
                    Question = question,
                    Answer = answer,
                    StackID = stackId,
                    DisplayID = displayId
                };

                connection.Execute(insertQuery, flashcardParameters);
            }
        }
        public static List<Flashcard> GetFlashcardsFromStack(int stackId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string selectQuery = @"
                    SELECT DisplayID, Question, Answer
                    FROM Flashcards
                    WHERE StackID = @StackID";

                var parameters = new { StackID = stackId };

                return connection.Query<Flashcard>(selectQuery, parameters).ToList();
            }
        }

        public static void UpdateFlashcard(int stackID)
        {
            while (true)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var flashcard = connection.QuerySingleOrDefault<Flashcard>(
                        "SELECT * FROM Flashcards WHERE DisplayID = @FlashcardID", new
                        {
                            FlashcardID = stackID
                        });

                    if (flashcard == null)
                    {
                        Console.WriteLine("Flashcard not found. Please try again.");
                        continue;
                    }

                    string question = UserInput.GetQuestion("Please enter the updated question", true, flashcard.Question);

                    string answer = UserInput.GetAnswer("Please enter the updated answer", true, flashcard.Answer);

                    string updateSql = @"
                            UPDATE Flashcards
                            SET Question = @Question, Answer = @Answer
                            WHERE DisplayID = @FlashcardID";

                    connection.Execute(updateSql, new
                    {
                        Question = question,
                        Answer = answer,
                        FlashcardID = stackID
                    });

                    Console.WriteLine("Flashcard updated successfully.");
                    break;
                }
            }
        }

        public static void DeleteFlashcard(int stackId)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Please enter the ID of the flashcard that you want to remove: ");
            var input = Console.ReadLine().Trim();

            if (int.TryParse(input, out int flashcardID))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string deleteQuery = "DELETE FROM Flashcards WHERE DisplayID = @DisplayID";
                    var parameters = new { DisplayID = flashcardID };
                    connection.Execute(deleteQuery, parameters);

                    string reorderQuery = @"
                        WITH OrderedFlashcards AS (
                            SELECT ID, ROW_NUMBER() OVER (ORDER BY ID) AS NewDisplayID
                            FROM Flashcards
                            WHERE StackID = @StackID
                        )
                        UPDATE Flashcards
                        SET DisplayID = OrderedFlashcards.NewDisplayID
                        FROM Flashcards
                        JOIN OrderedFlashcards ON Flashcards.ID = OrderedFlashcards.ID";

                    connection.Execute(reorderQuery, new { StackID = stackId });
                }

                Console.WriteLine("Flashcard deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer.");
            }
        }
    }
}

