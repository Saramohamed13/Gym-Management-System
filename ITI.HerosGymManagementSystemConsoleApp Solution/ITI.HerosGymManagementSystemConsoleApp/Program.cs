using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Threading.Channels;

namespace ITI.HerosGymManagementSystemConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = "Data Source=.;Initial Catalog=Heros_GYM; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();

                int UserId = User.UserCheck(connection);

                if (UserId < 0)
                    throw new ArgumentException("Wrong!!!!!!!");

                MemberShip.CreateMemberShip(UserId, connection);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally { 
                if(connection.State == System.Data.ConnectionState.Open)
                    connection.Close(); 
            }
        }
    }
}
