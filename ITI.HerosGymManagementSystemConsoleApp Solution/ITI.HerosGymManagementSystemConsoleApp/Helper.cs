using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITI.HerosGymManagementSystemConsoleApp
{
    internal static class Helper
    {
        public static string GetHiddenInput()
        {

            Console.Write("Enter the User Password: ");
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsPunctuation(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }
        public static void PrintDataTable(DataTable dataTable)
        {

            foreach (DataColumn column in dataTable.Columns)
            {
                Console.Write($"{column.ColumnName,-20}");
            }
            Console.WriteLine("\n");


            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write($"{item,-20}");
                }
                Console.WriteLine();
            }
        }
        public static void GetUserTravelOnApp(SqlConnection connection, int UserId)
        {
            int option;
            Console.WriteLine("[1] Members\n[2] Coaches\n[3] Memberships\n[4] Programs\n[5] Payments\n[6] Users\n[7] Exist..");

            do
            {
                Console.WriteLine("Choose One Option:");
            } while (!int.TryParse(Console.ReadLine(), out option));

            Console.Clear();

            switch (option)
            {
                case 1:
                    {
                        Members members = new Members(connection, UserId);
                        members.Options();
                    }
                    break;
                case 2:
                    {
                        Coaches coaches = new Coaches(connection, UserId);
                        coaches.ShowUserMenu();
                    }
                    break;
                case 3:
                    MemberShip.ExcutingMemberShipModelOptions(connection, UserId);
                    break;
                case 4:
                    GymProgram.ExcutingProgramModelOptions(connection, UserId);
                    break;
                case 5:
                    {
                        Payment payment =new Payment(connection, UserId);
                        payment.ShowUserMenu();
                    }
                    break;
                case 6:
                    User.ExcutingUserModelOptions(connection, UserId);
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    break;

            }
        }
        public static int DisplayMemberShipOptionsToUser()
        {
            int option;

            Console.WriteLine("Choose One Option..");
            Console.WriteLine("[1] Create a new Membership.");
            Console.WriteLine("[2] Update a specific Membership.");
            Console.WriteLine("[3] Read All Memberships.");
            Console.WriteLine("[4] Delete a specific Membership.");
            Console.WriteLine("[5] Read All Deleted Memberships.");
            Console.WriteLine("[6] Return..");

            int.TryParse(Console.ReadLine(), out option);

            Console.Clear();

            return option;

        }
        public static int DisplayProgramsOptionsToUser()
        {
            int option;

            Console.WriteLine("Choose One Option..");
            Console.WriteLine("[1] Create a new Program.");
            Console.WriteLine("[2] Update a specific program.");
            Console.WriteLine("[3] Read all programs.");
            Console.WriteLine("[4] Delete a specific program.");
            Console.WriteLine("[5] Read All Deleted programs.");
            Console.WriteLine("[6] Return..");

            int.TryParse(Console.ReadLine(), out option);

            Console.Clear();

            return option;

        }
        public static int DisplayUsersOptionsToUser()
        {
            int option;

            Console.WriteLine("Choose One Option..");
            Console.WriteLine("[1] Show all users.");
            Console.WriteLine("[2] Edit your info.");
            Console.WriteLine("[3] Return..");

            int.TryParse(Console.ReadLine(), out option);

            Console.Clear();
            return option;
        }
        public static bool IsValidEmail(string email)
        {


            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }
        public static bool IsValidName(string name)
        {


            string pattern = @"^[a-zA-Z\s'-]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(name);
        }
        public static string? GetUserName(SqlConnection connection, int UserId)
        {
            SqlCommand command = new SqlCommand($"select Name from Users where Id = {UserId}", connection);
            return command.ExecuteScalar().ToString();
        }


        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Validate the phone number format based on your specific requirements
            // For example, you might want to check if it contains only digits and has a specific length

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            foreach (char digit in phoneNumber)
            {
                if (!char.IsDigit(digit))
                {
                    return false;
                }
            }

            // Add more conditions as needed
            // For example, you can check if it has a specific length

            return true;
        }
    }
}
