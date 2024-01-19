using System.Data.SqlClient;
using System.Data;
namespace ITI.HerosGymManagementSystemConsoleApp
{
    class Coaches
    {
        private readonly SqlConnection connection;
        int UserId;

        public Coaches(SqlConnection connection, int _UserId)
        {
            this.connection = connection;
            UserId = _UserId;
        }
        public void ShowUserMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Add Coach");
                Console.WriteLine("2. Search Coach");
                Console.WriteLine("3. Update Coach");
                Console.WriteLine("4. Delete Coach");
                Console.WriteLine("5. Exit");
                Console.Write("Enter the number of your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        VaildUserInput(1);
                        break;
                    case "2":
                        SearchCoach();
                        break;
                    case "3":
                        VaildUserInput(2);
                        break;
                    case "4":
                        DeleteCoach();
                        break;
                    case "5":
                        Helper.GetUserTravelOnApp(connection, UserId);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
        }
        public void VaildUserInput(int Flag)
        {
            string? name;
            string? email;
            int programId, userId, phone, coachId = 0;
            if (Flag == 2)
            {

                Console.Write("Enter Coach ID to Update its data: ");
                while (!int.TryParse(Console.ReadLine(), out coachId))
                {
                    Console.WriteLine("Invalid Coach ID. Please enter a valid integer.");

                }

            }
            while (true)
            {
                Console.Write("Enter Coach Name  correct to move to next step: ");
                name = Console.ReadLine();
                if (Helper.IsValidName(name)) break;

            }
            while (true)
            {
                Console.Write("Enter Coach Email correct to move to next step: ");
                email = Console.ReadLine();
                if (Helper.IsValidEmail(email)) break;
            }

            Console.Write("Enter Program ID: ");

            while (!int.TryParse(Console.ReadLine(), out programId))
            {
                Console.WriteLine("Invalid Program ID. Please enter a valid integer.");

            }

            Console.Write("Enter User ID: ");
            while (!int.TryParse(Console.ReadLine(), out userId))
            {
                Console.WriteLine("Invalid User ID. Please enter a valid integer.");

            }

            Console.Write("Enter Coach Phone: ");
            while (!int.TryParse(Console.ReadLine(), out phone))
            {
                Console.WriteLine("Invalid Phone number. Please enter a valid integer.");

            }

            Console.Write("Enter Coach Address: ");
            string? address = Console.ReadLine();
            if (Flag == 1)
            {
                AddCoach(name, email, programId, userId, phone, address);

            }
            else
            {

                UpdateCoach(coachId, name, email, programId, userId, phone, address);
            }

        }
        public void AddCoach(string name, string email, int programId, int userId, int phone, string address)
        {
            try
            {


                // Insert into Coaches table
                string insertCoachQuery = "INSERT INTO Coaches (Name, Email, Program_Id, User_Id, IsDeleted) VALUES (@Name, @Email, @ProgramId, @UserId, 't'); SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(insertCoachQuery, connection);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ProgramId", programId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                int coachId = Convert.ToInt32(cmd.ExecuteScalar());

                // Insert into Coach_Phones table
                string insertPhoneQuery = "INSERT INTO Coach_Phones (Id, Phone) VALUES (@CoachId, @Phone);";
                SqlCommand phoneCmd = new SqlCommand(insertPhoneQuery, connection);

                phoneCmd.Parameters.AddWithValue("@CoachId", coachId);
                phoneCmd.Parameters.AddWithValue("@Phone", phone);
                int result2 = phoneCmd.ExecuteNonQuery();


                // Insert into Coach_Addresses table
                string insertAddressQuery = "INSERT INTO Coach_Addresses (Id, Address) VALUES (@CoachId, @Address);";
                SqlCommand addressCmd = new SqlCommand(insertAddressQuery, connection);

                addressCmd.Parameters.AddWithValue("@CoachId", coachId);
                addressCmd.Parameters.AddWithValue("@Address", address);
                int result3 = addressCmd.ExecuteNonQuery();
                if (result2 > 0 && result3 > 0)
                    Console.WriteLine($"The coach inserted successfully and its id is {coachId}");
                else
                    Console.WriteLine($"The coach not inserted");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            ShowUserMenu();
        }

        public void DeleteCoach()
        {
            // Update IsDeleted in Coaches table
            try
            {
                int coachId;
                Console.Write("Enter Coach ID: ");
                while (!int.TryParse(Console.ReadLine(), out coachId))
                {
                    Console.WriteLine("Invalid Coach ID. Please enter a valid integer.");

                }


                string updateQuery = "UPDATE Coaches SET IsDeleted = 'f' WHERE Id = @CoachId;";
                SqlCommand cmd = new SqlCommand(updateQuery, connection);
                cmd.Parameters.AddWithValue("@CoachId", coachId);
                int result2 = cmd.ExecuteNonQuery();
                if (result2 > 0)
                    Console.WriteLine($"The coach is deleted successfully ");
                else
                    Console.WriteLine($"The coach not deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            ShowUserMenu();


        }

        public void SearchCoach()
        {
            int coachId;
            Console.Write("Enter Coach ID: ");
            while (!int.TryParse(Console.ReadLine(), out coachId))
            {
                Console.WriteLine("Invalid Coach ID. Please enter a valid integer.");

            }
            try
            {


                string selectQuery = "SELECT C.*, CP.Phone, CA.Address " +
                                     "FROM Coaches C " +
                                     "JOIN Coach_Phones CP ON C.Id = CP.Id " +
                                     "JOIN Coach_Addresses CA ON C.Id = CA.Id " +
                                     "WHERE C.Id = @CoachId AND C.IsDeleted = 't';";

                SqlCommand cmd = new SqlCommand(selectQuery, connection);

                cmd.Parameters.AddWithValue("@CoachId", coachId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable resultTable = new DataTable();
                adapter.Fill(resultTable);
                if (resultTable.Rows.Count > 0)
                {
                    Console.WriteLine($"Coach found - Name: {resultTable.Rows[0]["Name"]}, Email: {resultTable.Rows[0]["Email"]}, Phone: {resultTable.Rows[0]["Phone"]}, Address: {resultTable.Rows[0]["Address"]}");
                }
                else
                {
                    Console.WriteLine("Coach not found.");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            ShowUserMenu();


        }

        public void UpdateCoach(int coachId, string newName, string newEmail, int newProgramId, int newUserId, int newPhone, string newAddress)
        {
            try
            {


                string updateQuery = "UPDATE Coaches SET Name = @NewName, Email = @NewEmail, Program_Id = @NewProgramId, User_Id = @NewUserId " +
                                     "WHERE Id = @CoachId AND IsDeleted = 't';" +
                                     "UPDATE Coach_Phones SET Phone = @NewPhone WHERE Id = @CoachId;" +
                                     "UPDATE Coach_Addresses SET Address = @NewAddress WHERE Id = @CoachId;";
                SqlCommand cmd = new SqlCommand(updateQuery, connection);
                cmd.Parameters.AddWithValue("@CoachId", coachId);
                cmd.Parameters.AddWithValue("@NewName", newName);
                cmd.Parameters.AddWithValue("@NewEmail", newEmail);
                cmd.Parameters.AddWithValue("@NewProgramId", newProgramId);
                cmd.Parameters.AddWithValue("@NewUserId", newUserId);
                cmd.Parameters.AddWithValue("@NewPhone", newPhone);
                cmd.Parameters.AddWithValue("@NewAddress", newAddress);
                int result2 = cmd.ExecuteNonQuery();
                if (result2 > 0)
                    Console.WriteLine($"The coach is updated successfully ");
                else
                    Console.WriteLine($"The coach not updated");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            ShowUserMenu();


        }
    }
}
