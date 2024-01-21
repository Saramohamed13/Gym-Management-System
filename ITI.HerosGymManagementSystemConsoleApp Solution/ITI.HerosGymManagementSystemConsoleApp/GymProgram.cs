using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.HerosGymManagementSystemConsoleApp
{
    internal static class GymProgram
    {
        private static string? name;
        private static string? description;
        private static decimal salary;

        public static void ExcutingProgramModelOptions(SqlConnection connection, int UserId)
        {

            int option = Helper.DisplayProgramsOptionsToUser();

            switch (option)
            {
                case 1:
                    CreateProgram(connection, UserId);
                    break;
                case 2:
                    UpdateSpecificProgram(connection, UserId);
                    break;
                case 3:
                    GetAllPrograms(connection, UserId);
                    ExcutingProgramModelOptions(connection, UserId);
                    break;
                case 4:
                    DeleteSpecificProgram(connection, UserId);
                    break;
                case 5:
                    GetDeletedPrograms(connection, UserId);
                    break;
                case 6:
                    Helper.GetUserTravelOnApp(connection, UserId);
                    break;
                default:
                    Console.WriteLine("Enter a valid option..");
                    ExcutingProgramModelOptions(connection, UserId);
                    break;
            }

        }
        public static void CreateProgram(SqlConnection connection, int UserId)
        {

            bool Flag;
            string? DescriptionHolder;
            int Holder;

            Console.Clear();

            if (CheckIfProgramIsExisted(connection) is null)
            {
                ExcutingProgramModelOptions(connection, UserId);
                return;
            }

            do
            {
                Console.Write("Enter the Description: ");
                DescriptionHolder = Console.ReadLine();
            } while (string.IsNullOrEmpty(DescriptionHolder));
            description = DescriptionHolder;

            do
            {
                Console.Write("Enter the Salary: ");
                Flag = int.TryParse(Console.ReadLine(), out Holder);
            } while (Holder <= 0 | !Flag);
            salary = Holder;

            SqlCommand command = new SqlCommand($"insert into Programs(Name,Description,Salary) values('{name}','{description}',{salary})", connection);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"'{name}' added successfully.");
            else
                Console.WriteLine($"'{name}' not be added.");

            Console.WriteLine("_____________________________");
            ExcutingProgramModelOptions(connection, UserId);


        }
        public static void UpdateSpecificProgram(SqlConnection connection, int UserId)
        {
            bool Flag;
            string? DescriptionHolder;
            int Holder;

            Console.Clear();

            GetAllPrograms(connection, UserId);

            if (CheckProgramNameToUpdate(connection) is null)
            {
                Console.WriteLine("There is no program with this name!!\n");
                ExcutingProgramModelOptions(connection, UserId);
                return;
            }

            do
            {
                Console.Write("Enter the Description: ");
                DescriptionHolder = Console.ReadLine();
            } while (string.IsNullOrEmpty(DescriptionHolder));
            description = DescriptionHolder;

            do
            {
                Console.Write("Enter the Salary: ");
                Flag = int.TryParse(Console.ReadLine(), out Holder);
            } while (Holder <= 0 | !Flag);
            salary = Holder;

            SqlCommand command = new SqlCommand($"update Programs\r\nset Name = '{name}', Description = '{description}', Salary = {salary}\r\nwhere Name = '{name}'", connection);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"'{name}' updated successfully.");
            else
                Console.WriteLine($"'{name}' not be updated.");

            Console.WriteLine("_____________________________");
            ExcutingProgramModelOptions(connection, UserId);


        }
        public static string? CheckIfProgramIsExisted(SqlConnection connection)
        {

            do
            {
                Console.Write("Enter the Name: ");
                name = Console.ReadLine();
            } while (name is null | name == "");

            string query = $"select *\r\nfrom Programs\r\nwhere IsDeleted = 'f' and Name = '{name}'";
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
        public static void GetAllPrograms(SqlConnection connection, int UserId)
        {
            Console.Clear();

            string query = "select * from GetAllProgramsData";

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(dataTable);
            }

            Helper.PrintDataTable(dataTable);

            Console.WriteLine("_____________________________");

        }
        public static void DeleteSpecificProgram(SqlConnection connection, int UserId)
        {
            string? input;
            string query;
            int ProgramsInProgramCount;

            Console.Clear();

            Console.WriteLine("Choose a program to delete..");

            GetAllPrograms(connection, UserId);

            do
            {
                Console.Write("Enter the Name : ");
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));

            query = $"select COUNT(p.Id) [Number Of Members]\r\nfrom Programs p, Member_Programs mp\r\nwhere p.Id = mp.Program_Id and p.IsDeleted = 'f' and p.Name = '{input}'";

            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            ProgramsInProgramCount = Convert.ToInt32(result);

            if (ProgramsInProgramCount > 0)
            {
                Console.WriteLine("This membership has already members..\n");
                ExcutingProgramModelOptions(connection, UserId);
                return;
            }
            else
            {
                string deleteQuery = $"update Programs\r\nset IsDeleted = 't', UserDeleted={UserId}\r\nwhere Name = '{input}'";
                command = new SqlCommand(deleteQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine($"'{input}' deleted successfully.");
                else
                    Console.WriteLine($"'{input}' not be found.");

                Console.WriteLine("_____________________________");
                ExcutingProgramModelOptions(connection, UserId);
            }
            Console.WriteLine("_____________________________");

        }
        public static void GetDeletedPrograms(SqlConnection connection, int UserId)
        {
            Console.Clear();

            string query = $"select * from GetAllDeletedProgramsData";

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(dataTable);
            }

            Helper.PrintDataTable(dataTable);

            Console.WriteLine("_____________________________");
            ExcutingProgramModelOptions(connection, UserId);
        }
        public static string? CheckProgramNameToUpdate(SqlConnection connection)
        {
            do
            {
                Console.Write("Enter the Name: ");
                name = Console.ReadLine();
            } while (name is null | name == "");

            string query = $"select *\r\nfrom Programs\r\nwhere IsDeleted = 'f' and Name = '{name}'";
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
    }
}
