using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.HerosGymManagementSystemConsoleApp
{
    internal static class User
    {
        public static int UserCheck(SqlConnection connection)
        {
            // Check for user access and return its id if exist , not exist is -1

            string? name;
            string password;

            do
            {
                Console.Write("Enter the User Name: ");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));


            // Encode Input Password
            password = Helper.GetHiddenInput();



            string command = $"select Id, Name, Password from Users where IsDeleted = 'f'";

            SqlCommand sqlCommand = new SqlCommand(command, connection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                        if (name == reader.GetString(1) && password == reader.GetString(2))
                        {
                            Console.WriteLine($"\nWelcome back ya {reader.GetString(1)}");
                            return reader.GetInt32(0);
                        }
                    return -1;
                }
                else
                    return -1;
            }












        }


    }
}
