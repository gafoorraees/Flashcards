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

        public static string ReturnStackName(int stackId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Name FROM Stacks WHERE ID = @StackId";
                var parameters = new { StackId = stackId };
                string stackName = connection.QuerySingle<string>(selectQuery, parameters);

                return stackName;
            }
        }
        public static void DeleteStack()
        {
            UI.DisplayStacks();

            Console.WriteLine("Please enter the name of the stack that you would like to delete. Warning: All flashcards linked to the stack will be deleted.");
            string stackDelete = Console.ReadLine().Trim();

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Stacks WHERE Name = @Name";
                var parameters = new { Name = stackDelete };
                connection.Execute(query, parameters);
            }
        }
    }
}
