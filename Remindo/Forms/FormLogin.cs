using MySql.Data.MySqlClient;
using Remindo.Class;
using Remindo.Forms;
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
        // MySQL connection string
        private string connectionString = "server=localhost;database=Remindo;uid=root";

        public FormLogin()
        {
            InitializeComponent();
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
                User user = new User(email, password);
                user.Login();

                // If login successful, redirect to home page
                MessageBox.Show("Login successful");
                Home home = new Home();
                home.Show();
                this.Hide();
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
