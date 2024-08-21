using System.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Flashcards.Models;

namespace Flashcards.Tables
{
    public class Stacks
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        public static void InsertStack(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Stacks (Name) VALUES (@Name)";
                var parameters = new { Name = name };
                connection.Execute(query, parameters);
            }
        }

        public static IEnumerable<Stack> GetAllStacks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Stacks";
                return connection.Query<Stack>(query).ToList();
            }
        }

        public static void DeleteStack(string name)
        {

        }
    }
}
