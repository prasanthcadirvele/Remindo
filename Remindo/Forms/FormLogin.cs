﻿using MySql.Data.MySqlClient;
using Remindo.Class;
using Remindo.Forms;
using Remindo.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remindo
{
    public partial class FormLogin : Form
    {
        private UserManager userManager;

        public FormLogin()
        {
            InitializeComponent();
            // Initialize the UserManager with the connection string
            string connectionString = "server=localhost;database=Remindo;uid=root;";
            userManager = new UserManager(connectionString);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {
            // Redirect to registration form
            FormRegister registerForm = new FormRegister();
            registerForm.Show();
            this.Hide();
        }

        private void checkBoxShowPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            // Show or hide password based on checkbox state
            txtPassword.UseSystemPasswordChar = !checkBoxShowPassword.Checked;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            try
            {
                // Authenticate the user
                var (isAuthenticated, utilisateurId) = userManager.AuthenticateUser(email, password);

                if (isAuthenticated)
                {
                    // If login successful, redirect to home page
                    MessageBox.Show("Login successful");
                    Home home = new Home(utilisateurId);
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear textboxes
            txtEmail.Text = "";
            txtPassword.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
