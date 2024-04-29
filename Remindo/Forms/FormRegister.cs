﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;


namespace Remindo
{
    public partial class FormRegister : Form
    {
        // MySQL connection string
        private string connectionString = "server=localhost;database=Remindo;uid=root";
        public FormRegister()
        {
            InitializeComponent();
        }
        // Event handler for the Register button click
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = textConfirmPassword.Text;

            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }

            // Hash the password for security before storing it in the database
            string hashedPassword = HashPassword(password);

            // Insert the user's email and hashed password into the database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Utilisateur (email, motdepasse) VALUES (@Email, @Password)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", hashedPassword);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!");
                        // Clear the textboxes after successful registration
                        txtEmail.Clear();
                        txtPassword.Clear();
                        textConfirmPassword.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Event handler for the Clear button click
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all textboxes
            txtEmail.Clear();
            txtPassword.Clear();
            textConfirmPassword.Clear();
        }

        // Event handler for the Label6 (Redirect to Login Form) click
        private void label6_Click(object sender, EventArgs e)
        {
            // Redirect to the Login form
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
            this.Hide();
        }

        // Method to hash the password using SHA256 algorithm
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Add any code you want to execute when the form loads
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle password visibility based on checkbox state
            if (checkBoxShowPassword.Checked)
            {
                // Show password
                txtPassword.PasswordChar = '\0'; // Null character (reveals password)
            }
            else
            {
                // Hide password (mask with '*')
                txtPassword.PasswordChar = '*';
            }

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}