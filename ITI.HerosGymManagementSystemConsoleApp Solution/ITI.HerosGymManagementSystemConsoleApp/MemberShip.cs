﻿using System;
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
        #endregion

        #region Methods

        public static void ExcutingMemberShipModelOptions(SqlConnection connection, int UserId)
        {

            int option = Helper.DisplayMemberShipOptionsToUser();

            switch (option)
            {
                case 1:
                    CreateMemberShip(connection, UserId);
                    break;
                case 2:
                    UpdateSpecificMemberShip(connection, UserId);
                    break;
                case 3:
                    GetAllMemberShips(connection, UserId);
                    ExcutingMemberShipModelOptions(connection, UserId);
                    break;
                case 4:
                    DeleteSpecificMembership(connection, UserId);
                    break;
                case 5:
                    GetDeletedMemberships(connection, UserId);
                    break;
                case 6:
                    Helper.GetUserTravelOnApp(connection, UserId);
                    break;
                default:
                    Console.WriteLine("Enter a valid option..");
                    ExcutingMemberShipModelOptions(connection, UserId);
                    break;
            }

        }
        public static void GetAllMemberShips(SqlConnection connection, int UserId)
        {
            Console.Clear();
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
        public static void CreateMemberShip(SqlConnection connection, int UserId)
        {
            // Creates a new membership

            bool Flag;
            int Holder;

            Console.Clear();

            if (CheckIfMemberShipIsExisted(connection) is null)
            {
                ExcutingMemberShipModelOptions(connection, UserId);
                return;
            }

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

            SqlCommand command = new SqlCommand($"insert into Memberships(Name,Amount,Period, User_Id) values('{name}',{amount},{period}, {UserId})", connection);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"'{name}' added successfully.");
            else
                Console.WriteLine($"'{name}' not be added.");

            Console.WriteLine("_____________________________");
            ExcutingMemberShipModelOptions(connection, UserId);


        }
        public static void UpdateSpecificMemberShip(SqlConnection connection, int UserId)
        {
            bool Flag;
            int Holder;

            Console.Clear();

            GetAllMemberShips(connection, UserId);

            if (CheckMembershipNameToUpdate(connection) is null)
            {
                Console.WriteLine("There is no membership with this name!!\n");
                ExcutingMemberShipModelOptions(connection, UserId);
                return;
            }

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


            SqlCommand command = new SqlCommand($"update Memberships\r\nset Name = '{name}', Amount = {amount}, Period = {period}\r\nwhere Name = '{name}'", connection);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"'{name}' updated successfully.");
            else
                Console.WriteLine($"'{name}' not be updated.");

            Console.WriteLine("_____________________________");
            ExcutingMemberShipModelOptions(connection, UserId);


        }
        public static void DeleteSpecificMembership(SqlConnection connection, int UserId)
        {
            string? input;
            string query;
            int MembersInMembershipCount;

            Console.Clear();

            Console.WriteLine("Choose a membership to delete..");

            GetAllMemberShips(connection, UserId);

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
                Console.WriteLine("This membership has already members..\n");
                ExcutingMemberShipModelOptions(connection, UserId);
                return;
            }
            else
            {
                string deleteQuery = $"update Memberships\r\nset IsDeleted = 't', UserDeleted={UserId}\r\nwhere Name = '{input}'";
                command = new SqlCommand(deleteQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine($"'{input}' deleted successfully.");
                else
                    Console.WriteLine($"'{input}' not be found.");

                Console.WriteLine("_____________________________");
                ExcutingMemberShipModelOptions(connection, UserId);
            }
            Console.WriteLine("_____________________________");

        }
        public static void GetDeletedMemberships(SqlConnection connection, int UserId)
        {
            Console.Clear();

            // GET all deleted Memberships
            string query = $"select * from GetAllDeletedMemberShipsData";

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(dataTable);
            }

            Helper.PrintDataTable(dataTable);

            Console.WriteLine("_____________________________");
            ExcutingMemberShipModelOptions(connection, UserId);
        }
        public static string? CheckIfMemberShipIsExisted(SqlConnection connection)
        {

            do
            {
                Console.Write("Enter the Name: ");
                name = Console.ReadLine();
            } while (name is null | name == "");

            string query = $"select *\r\nfrom Memberships\r\nwhere IsDeleted = 'f' and Name = '{name}'";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.GetString(1)?.ToLower().Trim() == name?.ToLower().Trim())
                    {
                        Console.WriteLine("There is already membership with this name..");
                        return null;
                    }
                }
                return name;
            }
        }
        public static string? CheckMembershipNameToUpdate(SqlConnection connection)
        {
            do
            {
                Console.Write("Enter the Name: ");
                name = Console.ReadLine();
            } while (name is null | name == "");

            string query = $"select *\r\nfrom Memberships\r\nwhere IsDeleted = 'f' and Name = '{name}'";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.GetString(1)?.ToLower().Trim() == name?.ToLower().Trim())
                    {
                        return name;
                    }
                }
                return null;
            }
        }

        #endregion

    }

}

