using System;
using MySql.Data.MySqlClient;

namespace Remindo.Repositories
{
    public class UserManager
    {
        private string connectionString;

        public UserManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public (bool isAuthenticated, int utilisateurId) AuthenticateUser(string email, string password)
        {
            // Implement user authentication logic
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open connection
                    connection.Open();

                    // Check if email and password match
                    string query = "SELECT utilisateurId FROM Utilisateur WHERE email = @Email AND motdepasse = @Password";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        int utilisateurId = Convert.ToInt32(command.ExecuteScalar());

                        if (utilisateurId > 0)
                        {
                            // User authenticated successfully
                            return (true, utilisateurId);
                        }
                    }

                    return (false, -1);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return (false, -1);
            }
        }

        public bool RegisterUser(string email, string password)
        {
            // Implement user registration logic
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open connection
                    connection.Open();

                    // Check if email already exists
                    string query = "SELECT COUNT(*) FROM Utilisateur WHERE email = @Email";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            // Email already exists
                            return false;
                        }
                    }

                    // Insert new user
                    query = "INSERT INTO Utilisateur (email, motdepasse) VALUES (@Email, @Password)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
