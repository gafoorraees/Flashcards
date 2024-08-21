using System.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Flashcards.Models;

namespace Flashcards.Tables
{
    public class Flashcards
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        public static void InsertFlashcard(string question, string answer, string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT StackID FROM Stacks WHERE Name = @Name";
                var parameters = new { Name = stackName };
                int stackId = connection.QuerySingle<int>(selectQuery, parameters);

                string getMaxDisplayIdQuery = "SELECT ISNULL(MAX(DisplayID), 0) + 1 FROM Flashcards WHERE StackID = @StackID";
                var displayId = connection.QuerySingle<int>(getMaxDisplayIdQuery, new { StackID = stackId });
             

                string insertQuery = @"INSERT INTO Flashcards (Question, Answer, StackID)
                                       VALUES (@Question, @Answer, @StackID)";
                
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
    }
}
