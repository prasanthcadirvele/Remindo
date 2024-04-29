using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Remindo.Class
{
    public class User
    {
        // Properties
        public int UserId { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }

        // Constructor
        public User(string email, string motDePasse)
        {
            Email = email;
            MotDePasse = motDePasse;
        }

        // Method to register a new user
        public void Register()
        {
            // Validate input
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(MotDePasse))
            {
                throw new ArgumentException("Email and password are required.");
            }

            // Check if the email is already registered
            if (IsEmailRegistered(Email))
            {
                throw new InvalidOperationException("This email is already registered.");
            }

            // Hash the password
            string hashedPassword = HashPassword(MotDePasse);

            // Save user details in the database
            SaveUserToDatabase(Email, hashedPassword);
        }

        // Method to check if an email is already registered
        private bool IsEmailRegistered(string email)
        {
            // Connection string
            string connectionString = "server=localhost;database=Remindo;uid=root";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // SQL query to check if the email is already registered
                string query = "SELECT COUNT(*) FROM Utilisateur WHERE email = @Email";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    // Execute the query
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Method to hash the password
        private string HashPassword(string password)
        {
            // Add logic to hash the password (e.g., using SHA256 or bcrypt)
            // For example, you can use a hashing library like BCrypt.Net
            // For demonstration purposes, let's just return the password as is
            return password;
        }

        // Method to save user details in the database
        private void SaveUserToDatabase(string email, string hashedPassword)
        {
            // Connection string
            string connectionString = "server=localhost;database=Remindo;uid=root";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // SQL query to insert user details into the database
                string query = "INSERT INTO Utilisateur (email, motdepasse) VALUES (@Email, @MotDePasse)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@MotDePasse", hashedPassword);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to authenticate user login
        public bool Login()
        {
            // Connection string
            string connectionString = "server=localhost;database=Remindo;uid=root";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // SQL query to check if the provided email and password match
                string query = "SELECT COUNT(*) FROM Utilisateur WHERE email = @Email AND motdepasse = @MotDePasse";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@MotDePasse", HashPassword(MotDePasse));

                    // Execute the query
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Method to logout the user
        public void Logout()
        {
            // Clear user credentials
            Email = null;
            MotDePasse = null;
        }

        // Methods for change password, etc.
        // ...
    }
}
