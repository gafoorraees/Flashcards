using Flashcards.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace Flashcards.Tables
{
    public class StudySessions
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
        public static void SaveStudySession(StudySessionDTO session)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO StudySessions (StackID, Date, Score) VALUES (@StackID, @Date, @Score)";
                connection.Execute(insertQuery, new { session.StackID, session.Date, session.Score });
            }
        }
     }
}
