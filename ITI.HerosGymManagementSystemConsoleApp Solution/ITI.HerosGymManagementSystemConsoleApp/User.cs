using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                            Console.Clear();
                            Console.WriteLine($"Welcome back ya {reader.GetString(1)}");
                            Console.WriteLine("------------------------\n");
                            return reader.GetInt32(0);
                        }
                    return -1;
                }
                else
                    return -1;
            }

        }
        public static void ExcutingUserModelOptions(SqlConnection connection, int UserId)
        {
            int option = Helper.DisplayUsersOptionsToUser();

            switch (option)
            {
                case 1:
                    ShowAllUsers(connection, UserId);
                    break;
                case 2:
                    EditASpecificUser(connection, UserId);
                    break;
                case 3:
                    Helper.GetUserTravelOnApp(connection, UserId);
                    break;
                default:
                    Console.WriteLine("Enter a valid option..");
                    ExcutingUserModelOptions(connection, UserId);
                    break;
            }
        }
        public static void ShowAllUsers(SqlConnection connection, int UserId)
        {
            Console.Clear();
            // Dispaly all info about all users
            string query = $"select Name[UserName] from Users where IsDeleted='f'";

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(dataTable);
            }

            Helper.PrintDataTable(dataTable);

            Console.WriteLine("_____________________________");
            ExcutingUserModelOptions(connection, UserId);
        }
        public static void EditASpecificUser(SqlConnection connection, int UserId)
        {
            string? NewName;
            string NewPassword;
            Console.Write("Enter The New Name: ");
            NewName = Console.ReadLine();
            NewPassword = Helper.GetHiddenInput();

            string query = $"update Users set Name='{NewName}', Password='{NewPassword}' where Id = {UserId}";

            SqlCommand command = new SqlCommand(query, connection);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"\n'{NewName}' updated successfully.");
            else
                Console.WriteLine($"\n'{NewName}' not be updated.");

            Console.WriteLine("_____________________________");
            ExcutingUserModelOptions(connection, UserId);




        }

    }
}
