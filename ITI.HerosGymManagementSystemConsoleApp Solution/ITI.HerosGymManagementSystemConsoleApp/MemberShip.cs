using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace ITI.HerosGymManagementSystemConsoleApp
{
    internal static class MemberShip
    {
        #region Fields
        private static string? name;
        private static int amount;
        private static int period;
        private static char isDeleted = 'f';
        #endregion

        #region Methods
        public static void ExcutingMemberShipModelOptions(SqlConnection connection, int UserId)
        {
            #region Display MemberShips Options To The User

            int option;


            Console.WriteLine("Choose One Option..");
            Console.WriteLine("[1] Create a new Membership.");
            Console.WriteLine("[2] Read All Memberships.");
            Console.WriteLine("[3] Delete a specific Membership.");
            Console.WriteLine("[4] Read All Deleted Memberships.");

            int.TryParse(Console.ReadLine(), out option);

            #endregion

            switch (option)
            {
                case 1:
                    CreateMemberShip(UserId, connection);
                    ExcutingMemberShipModelOptions(connection, UserId);
                    break;
                case 2:
                    GetAllMemberShips(connection);
                    ExcutingMemberShipModelOptions(connection, UserId);
                    break;
                case 3:
                    DeleteSpecificMembership(connection);
                    ExcutingMemberShipModelOptions(connection, UserId);
                    break;
                case 4:
                    GetDeletedMemberships(connection);
                    ExcutingMemberShipModelOptions(connection, UserId);
                    break;
                default:
                    Console.WriteLine("Enter a valid option..");
                    ExcutingMemberShipModelOptions(connection, UserId);
                    break;
            }
        }

        public static void GetAllMemberShips(SqlConnection connection)
        {
            // Dispaly all info about all memberships
            string query = "select * from GetAllMemberShipsData";

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(dataTable);
            }

            Helper.PrintDataTable(dataTable);

            Console.WriteLine("_____________________________");
        }

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


            do
            {
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

            Console.WriteLine("_____________________________");

        }

        public static void DeleteSpecificMembership(SqlConnection connection)
        {
            string? input;
            string query;
            int MembersInMembershipCount;
            Console.WriteLine("Choose a membership to delete..");

            GetAllMemberShips(connection);

            do
            {
                Console.Write("Enter the Name : ");
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));

            query = $"select COUNT(m.Id) [Number Of Members]\r\nfrom Members m, Memberships ms\r\nwhere ms.Id = m.Membership_Id and m.IsDeleted = 'f' and ms.Name = '{input}'";

            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            MembersInMembershipCount = Convert.ToInt32(result);

            if (MembersInMembershipCount > 0)
            {
                Console.WriteLine("This membership has already members..");
                return;
            }
            else
            {
                string deleteQuery = $"update Memberships\r\nset IsDeleted = 't'\r\nwhere Name = '{input}'";
                command = new SqlCommand(deleteQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine($"{input} updated successfully.");
                else
                    Console.WriteLine($"{input} not be found.");

                Console.WriteLine("_____________________________");
            }






            Console.WriteLine("_____________________________");
        }

        public static void GetDeletedMemberships(SqlConnection connection)
        {
            // GET all deleted Memberships
            string query = $"select * from GetAllDeletedMemberShipsData";

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(dataTable);
            }

            Helper.PrintDataTable(dataTable);

            Console.WriteLine("_____________________________");
        }
        #endregion






    }

}

