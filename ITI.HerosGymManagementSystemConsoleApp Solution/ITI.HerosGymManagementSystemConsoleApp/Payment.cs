using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Xml.Linq;
namespace ITI.HerosGymManagementSystemConsoleApp
{
    public class Payment
    {
        #region Fields
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int? User_Id { get; set; }
        public int? Member_Id { get; set; }
      
        SqlConnection connection;
        int UserId;
        #endregion
        #region Methods
        public Payment(SqlConnection _connection,int _UserId)
        {
            connection = _connection;
            UserId = _UserId;
        }
        public void ShowUserMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. show all payments ");
                Console.WriteLine("2. show all payments for specific person");
                Console.WriteLine("3. Exit");
                Console.Write("Enter the number of your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GetAllPayments();
                        break;
                    case "2":
                        GetAllPaymentsByMemberId();
                        break;                
                    case "3":
                        Helper.GetUserTravelOnApp(connection, UserId);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }
            }
        }
        public void CreatePayment(Payment newPayment)
        {
            try
            {
                string query = "INSERT INTO Payments (Amount, Date, Time, User_Id, Member_Id, IsDeleted) " +
                               "VALUES (@Amount, @Date, @Time, @User_Id, @Member_Id)";

                SqlCommand command = new SqlCommand(query, connection);
             
                command.Parameters.AddWithValue("@Amount", newPayment.Amount);
                command.Parameters.AddWithValue("@Date", newPayment.Date);
                command.Parameters.AddWithValue("@Time", newPayment.Time);
                command.Parameters.AddWithValue("@User_Id", newPayment.User_Id);
                command.Parameters.AddWithValue("@Member_Id", newPayment.Member_Id);
              

                int res = command.ExecuteNonQuery();

                if (res > 0)
                {
                    Console.WriteLine("Payment created successfully!");
                    PrintDetails(newPayment);
                }
                else
                    Console.WriteLine("Payment not created created ");
            }catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            ShowUserMenu();
        }

        public void PrintDetails(Payment newPayment)
        {
            Console.WriteLine($"Payment ID: {newPayment.Id}");
            Console.WriteLine($"Amount: {newPayment.Amount}");
            Console.WriteLine($"Date: {newPayment.Date}");
            Console.WriteLine($"Time: {newPayment.Time}");
            Console.WriteLine($"User ID: {newPayment.User_Id}");
            Console.WriteLine($"Member ID: {newPayment.Member_Id}");
            Console.WriteLine();
        }
        public void GetAllPayments()
        {


            Console.WriteLine();

            string sqlQuery = $"SELECT * FROM Payments";

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
            {
                adapter.Fill(dataTable);
            }

            Helper.PrintDataTable(dataTable);
            ShowUserMenu();
        }
        public void GetAllPaymentsByMemberId()
        {
            int MemberId = 0;
            Console.Write("Enter Member ID: ");
            while (!int.TryParse(Console.ReadLine(), out MemberId))
            {
                Console.WriteLine("Invalid Member ID. Please enter a valid integer.");
            }
            Console.WriteLine();

            string selectQuery = $"SELECT * FROM Payments where Member_Id = @Member_Id";
          
            SqlCommand cmd = new SqlCommand(selectQuery, connection);

            cmd.Parameters.AddWithValue("@Member_Id", MemberId);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable resultTable = new DataTable();
            adapter.Fill(resultTable);
            if (resultTable.Rows.Count > 0)
            {
                Helper.PrintDataTable(resultTable);
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
            ShowUserMenu();

        }
        #endregion
    }


}