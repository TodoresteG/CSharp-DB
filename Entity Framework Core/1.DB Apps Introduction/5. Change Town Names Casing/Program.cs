using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _5._Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";

            string country = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateTownNames = @"UPDATE Towns
                                            SET Name = UPPER(Name)
                                            WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

                using (SqlCommand command = new SqlCommand(updateTownNames, connection))
                {
                    command.Parameters.AddWithValue("@countryName", country);

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows <= 0)
                    {
                        Console.WriteLine("No town names were affected.");
                        return;
                    }

                    Console.WriteLine($"{affectedRows} town names were affected.");
                }

                string findTowns = @"SELECT t.Name 
                                    FROM Towns as t
                                    JOIN Countries AS c ON c.Id = t.CountryCode
                                    WHERE c.Name = @countryName";

                using (SqlCommand command = new SqlCommand(findTowns, connection))
                {
                    command.Parameters.AddWithValue("@countryName", country);
                    List<string> towns = new List<string>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            towns.Add((string)reader["Name"]);
                        }
                    }

                    Console.WriteLine($"[{String.Join(", ", towns)}]");
                }
            }
        }
    }
}
