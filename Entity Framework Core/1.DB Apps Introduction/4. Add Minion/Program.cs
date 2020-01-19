using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _4._Add_Minion
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";

            string[] minionsInfo = Console.ReadLine().Split();
            string[] villainInfo = Console.ReadLine().Split();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string findTown = @"SELECT Id FROM Towns WHERE Name = @townName";

                int? townId = FindTownId(findTown, connection, minionsInfo);

                if (townId == null)
                {
                    string insertIntoTowns = @"INSERT INTO Towns (Name) VALUES (@townName)";

                    using (SqlCommand command = new SqlCommand(insertIntoTowns, connection))
                    {
                        command.Parameters.AddWithValue("@townName", minionsInfo[3]);

                        command.ExecuteNonQuery();
                        Console.WriteLine($"Town {minionsInfo[3]} was added to the database.");
                    }

                    townId = FindTownId(findTown, connection, minionsInfo);
                }

                string findVillainId = @"SELECT Id FROM Villains WHERE Name = @Name";
                int? villainId = FindVillainId(findVillainId, connection, villainInfo);

                if (villainId == null)
                {
                    string insertVillain = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

                    using (SqlCommand command = new SqlCommand(insertVillain, connection))
                    {
                        command.Parameters.AddWithValue("@villainName", villainInfo[1]);
                        command.ExecuteNonQuery();
                        Console.WriteLine($"Villain {villainInfo[1]} was added to the database.");
                    }

                    villainId = FindVillainId(findVillainId, connection, villainInfo);
                }

                string findMinionId = @"SELECT Id FROM Minions WHERE Name = @Name";
                int? minionId = FindMinionId(findMinionId, connection, minionsInfo);

                if (minionId == null || minionId <= 0)
                {
                    string insertMinion = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@nam, @age, @townId)";

                    using (SqlCommand command = new SqlCommand(insertMinion, connection))
                    {
                        command.Parameters.AddWithValue("@nam", minionsInfo[1]);
                        command.Parameters.AddWithValue("@age", minionsInfo[2]);
                        command.Parameters.AddWithValue("@townId", townId);

                        command.ExecuteNonQuery();
                    }

                    minionId = FindMinionId(findMinionId, connection, minionsInfo);
                }


                string minionToVillain = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";

                using (SqlCommand command = new SqlCommand(minionToVillain, connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    command.Parameters.AddWithValue("@minionId", minionId);

                    command.ExecuteNonQuery();
                    Console.WriteLine($"Successfully added {minionsInfo[1]} to be minion of {villainInfo[1]}.");
                }
            }
        }

        public static int? FindTownId(string cmdText, SqlConnection connection, string[] minionsInfo)
        {
            using (SqlCommand command = new SqlCommand(cmdText, connection))
            {
                command.Parameters.AddWithValue("@townName", minionsInfo[3]);

                return (int?)command.ExecuteScalar();
            }
        }

        public static int? FindVillainId(string cmdText, SqlConnection connection, string[] villainInfo)
        {
            using (SqlCommand command = new SqlCommand(cmdText, connection))
            {
                command.Parameters.AddWithValue("@Name", villainInfo[1]);
                return (int?)command.ExecuteScalar();
            }
        }

        public static int? FindMinionId(string cmdText, SqlConnection connection, string[] minionInfo)
        {
            using (SqlCommand command = new SqlCommand(cmdText, connection))
            {
                command.Parameters.AddWithValue("@Name", minionInfo[1]);
                return (int?)command.ExecuteScalar();
            }
        }
    }
}
