using System;
using System.Data.SqlClient;

namespace _3._Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";

            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string findVillain = "SELECT Name FROM Villains WHERE Id = @Id";
                string villainName = "";

                using (SqlCommand command = new SqlCommand(findVillain, connection))
                {
                    command.Parameters.AddWithValue("@Id", villainId);
                    villainName = (string)command.ExecuteScalar();

                    if (String.IsNullOrEmpty(villainName))
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                        return;
                    }

                    Console.WriteLine($"Villain: {villainName}");
                }

                string findMinions = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

                using (SqlCommand command = new SqlCommand(findMinions, connection))
                {
                    command.Parameters.AddWithValue("@Id", villainId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("(no minions)");
                            return;
                        }

                        while (reader.Read())
                        {
                            long rowNum = (long)reader["RowNum"];
                            string name = (string)reader["Name"];
                            int age = (int)reader["Age"];

                            Console.WriteLine($"{rowNum}. {name} {age}");
                        }
                    }
                }
            }
        }
    }
}
