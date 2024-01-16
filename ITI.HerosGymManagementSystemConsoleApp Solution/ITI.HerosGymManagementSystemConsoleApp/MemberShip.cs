using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ITI.HerosGymManagementSystemConsoleApp
{
    internal static class MemberShip
    {
        #region Fields
        private static int id;
        private static string name;
        private static int amount;
        private static int period;
        private static char isDeleted = 'f';
        #endregion


        #region Methods

        public static void CreateMemberShip(int User_Id, SqlConnection connection)
        {
            // Creates a new membership

            bool Flag;
            int Holder;


            do
            {
                Console.Write("Enter the Name: ");
                name = Console.ReadLine();
            } while (name is null | name == "");


            do {
                Console.Write("Enter the Amount: ");
                Flag = int.TryParse(Console.ReadLine(), out Holder);
            } while (Holder <= 0 | !Flag);
            amount = Holder;

            do
            {
                Console.Write("Enter the Period[In Months]: ");
                Flag = int.TryParse(Console.ReadLine(), out Holder);
            } while (Holder <= 0 | !Flag);
            period = Holder;

            SqlCommand command = new SqlCommand($"insert into Memberships(Name,Amount,Period, User_Id,IsDeleted) values('{name}',{amount},{period}, {User_Id}, '{isDeleted}')", connection);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"{name} added successfully.");
            else
                Console.WriteLine($"{name} not be added.");

        }




        #endregion
    }
}
