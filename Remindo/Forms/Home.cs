using Remindo.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remindo.Forms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open the form to add an evenement
            AddEvenementForm addEvenementForm = new AddEvenementForm();
            addEvenementForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddNoteForm addNoteForm = new AddNoteForm();
            addNoteForm.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            // Create an instance of the User class
            User user = new User(null, null);

            // Call the Logout method to clear user credentials
            user.Logout();

            // Redirect to the login form
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
            this.Hide(); // Hide the current form
        }

    }
}
