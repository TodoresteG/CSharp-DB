using System;
using System.Data.SqlClient;

namespace _6._Remove_Villain
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

                string findVillain = @"SELECT Name FROM Villains WHERE Id = @villainId";

                string villainName = "";

                using (SqlCommand command = new SqlCommand(findVillain, connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    villainName = (string)command.ExecuteScalar();
                }

                if (String.IsNullOrEmpty(villainName))
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }

                string deleteMinions = @"DELETE FROM MinionsVillains 
                                            WHERE VillainId = @villainId";
                int deletedMinions = -1;

                using (SqlCommand command = new SqlCommand(deleteMinions, connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    deletedMinions = command.ExecuteNonQuery();
                }

                string deleteVillain = @"DELETE FROM Villains
                                        WHERE Id = @villainId";

                using (SqlCommand command = new SqlCommand(deleteVillain, connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{deletedMinions} minions were released.");
            }
        }
    }
}
