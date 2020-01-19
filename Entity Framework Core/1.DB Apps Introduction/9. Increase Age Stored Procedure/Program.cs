using System;
using System.Data.SqlClient;
using System.Linq;

namespace _9._Increase_Age_Stored_Procedure
{
    public class StartUp
    {
        public static void Main()
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";

            var minionsIds = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var id in minionsIds)
                {
                    UpdateMinion(connection, id);
                }

                GetAllMinions(connection);
            }
        }

        private static void UpdateMinion(SqlConnection connection, int id)
        {
            string executeProcedureQuery = @"EXEC usp_GetOlder @id";

            using (SqlCommand command = new SqlCommand(executeProcedureQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private static void GetAllMinions(SqlConnection connection)
        {
            string getAllMinionsQuery = @"SELECT Name, 
                                                 Age 
                                            FROM Minions";

            using (SqlCommand command = new SqlCommand(getAllMinionsQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = (string)reader[0];
                        int age = (int)reader[1];

                        Console.WriteLine($"{name} - {age}");
                    }
                }
            }
        }
    }
}
