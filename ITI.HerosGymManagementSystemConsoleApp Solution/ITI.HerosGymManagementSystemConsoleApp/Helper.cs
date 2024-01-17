using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                    break;
                case 2:
                    break;
                case 3:
                    MemberShip.ExcutingMemberShipModelOptions(connection, UserId);
                    break;
                case 4:
                    break;
                case 5:
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

    }
}
