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

        public static List<Stack> GetAllStacks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Stacks";
                return connection.Query<Stack>(query).ToList();
            }
        }

        public static int ReturnStackID(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT StackID FROM Stacks WHERE Name = @Name";
                var parameters = new { Name = stackName };
                int stackId = connection.QuerySingle<int>(selectQuery, parameters);

                return stackId;
            }
        }
        public static void DeleteStack(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Stacks WHERE Name = @Name";
                var parameters = new { Name = stackName };
                connection.Execute(query, parameters);
            }
        }
    }
}
