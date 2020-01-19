using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace _8._Increase_Minion_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";

            int[] minionsId = Console.ReadLine().Split().Select(int.Parse).ToArray();

            foreach (var id in minionsId)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateMinion = @" UPDATE Minions
                                        SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                        WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(updateMinion, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string showAllMinions = @"SELECT m.Name, m.Age FROM Minions m";

                using (SqlCommand command = new SqlCommand(showAllMinions, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                        }
                    }
                }
            }
        }
    }
}
