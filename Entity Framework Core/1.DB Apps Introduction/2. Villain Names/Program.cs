using System;
using System.Data.SqlClient;

namespace _2._Villain_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string allVillainsCommand = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                                FROM Villains AS v 
                                                JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                            GROUP BY v.Id, v.Name 
                                              HAVING COUNT(mv.VillainId) > 3 
                                            ORDER BY COUNT(mv.VillainId)";

                using (SqlCommand command = new SqlCommand(allVillainsCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader[0];
                            int count = (int)reader[1];

                            Console.WriteLine($"{name} - {count}");
                        }
                    }
                }
            }
        }
    }
}
