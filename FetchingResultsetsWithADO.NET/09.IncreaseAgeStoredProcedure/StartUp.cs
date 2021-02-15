using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _09.IncreaseAgeStoredProcedure
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                 connection.Open();

                using (SqlCommand command = new SqlCommand("EXEC usp_GetOlder @Id", connection))
                {

                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                string minionsQuery = "SELECT * FROM Minions WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(minionsQuery, connection))
                {
                    
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{(string)reader["Name"]} - {(int)reader["Age"]} years old");
                        }
                    }
                }
            }
        }
    }
}
