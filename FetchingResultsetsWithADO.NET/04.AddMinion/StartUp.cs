using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _04.AddMinion
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] miniinInfo = Console.ReadLine().Split();
            string[] villainInfo = Console.ReadLine().Split();

            string minionName = miniinInfo[1];
            int age = int.Parse(miniinInfo[2]);
            string town = miniinInfo[3];

            string villainName = villainInfo[1];

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int? townId = GetTownByName(town, connection);

                if (townId == null)
                {
                    AddTown(connection, town);
                }

                townId = GetTownByName(town, connection);

                AddMinions(connection, minionName, age, townId);

                int? villainId = GetVillainByName(connection, villainName);

                if (villainId == null)
                {
                    AddVillain(connection, villainName);
                }

                villainId = GetVillainByName(connection, villainName);

                int minionId = GetMinionByName(connection,minionName);

                AddMinionVillain(connection, villainId, minionId, minionName, villainName);
            }
        }

        private static void AddMinionVillain(SqlConnection connection, int? villainId, int minionId, string minionName, string villainName)
        {
            string insertMinionVillain = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";

            using (SqlCommand command = new SqlCommand(insertMinionVillain, connection))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                command.Parameters.AddWithValue("@minionId", minionId);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
        }

        private static int GetMinionByName(SqlConnection connection, string minionName)
        {
            string minionNameQuery = @"SELECT Id FROM Minions WHERE Name = @Name";

            using (SqlCommand command = new SqlCommand(minionNameQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", minionName);
                return (int)command.ExecuteScalar();
            }
        }

        private static void AddVillain(SqlConnection connection, string villainName)
        {
            string insertVillain = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

            using (SqlCommand command = new SqlCommand(insertVillain, connection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Villain {villainName} was added to the database.");
        }

        private static int? GetVillainByName(SqlConnection connection, string villainName)
        {
            string villainIdQuery = "SELECT Id FROM Villains WHERE Name = @Name";

            using (SqlCommand command = new SqlCommand(villainIdQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", villainName);
                return (int?)command.ExecuteScalar();
            }
        }

        private static void AddMinions(SqlConnection connection, string minionName, int age, int? townId)
        {
            string insertMinion = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

            using (SqlCommand command = new SqlCommand(insertMinion, connection))
            {
                command.Parameters.AddWithValue("@name", minionName);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();
            }
        }

        private static int? GetTownByName(string town, SqlConnection connection)
        {
            string townIdQuery = "SELECT Id FROM Towns WHERE Name = @townName";

            using (SqlCommand command = new SqlCommand(townIdQuery, connection))
            {
                command.Parameters.AddWithValue("@townName", town);
                return (int?)command.ExecuteScalar();
            }
        }

        private static void AddTown(SqlConnection connection, string town)
        {
            string insertTown = "INSERT INTO Towns (Name) VALUES (@townName)";

            using (SqlCommand command = new SqlCommand(insertTown, connection))
            {
                command.Parameters.AddWithValue("@townName", town);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Town {town} was added to the database.");
        }
    }
}
