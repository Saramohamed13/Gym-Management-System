using System;
using System.Collections.Generic;
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
    }
}
