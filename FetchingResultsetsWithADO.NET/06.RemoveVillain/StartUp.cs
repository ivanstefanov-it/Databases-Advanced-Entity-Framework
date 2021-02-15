using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _06.RemoveVillain
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                string villainQuery = @"SELECT Name FROM Villains WHERE Id = @villainId";
                string villainName;

                using (SqlCommand command = new SqlCommand(villainQuery, connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    villainName = (string)command.ExecuteScalar();

                    if (villainName == null)
                    {
                        Console.WriteLine("No such villain was found.");
                        return;
                    }
                }

                int affectedRows = DeleteMinionsVillainsById(connection, villainId);
                DeleteVillainById(connection, villainId);

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{affectedRows} minions were released.");
            }
        }

        private static void DeleteVillainById(SqlConnection connection, int villainId)
        {
            string deleteVillainQuery = @"DELETE FROM Villains
      WHERE Id = @villainId";

            using (SqlCommand command = new SqlCommand(deleteVillainQuery, connection))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }
        }

        private static int DeleteMinionsVillainsById(SqlConnection connection, int villainId)
        {
            string deleteVillainQuery = @"DELETE FROM MinionsVillains 
      WHERE VillainId = @villainId";

            using (SqlCommand command = new SqlCommand(deleteVillainQuery, connection))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                return command.ExecuteNonQuery();
            }
        }
    }
}
