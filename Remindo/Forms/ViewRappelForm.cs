using MySql.Data.MySqlClient;
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
    public partial class ViewRappelForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int rappelId; // Store the ID of the Rappel to load

        public ViewRappelForm(int rappelId)
        {
            InitializeComponent();
            this.rappelId = rappelId;

            // Load Rappel details when the form is loaded
            LoadRappelDetails();
        }

        private void LoadRappelDetails()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch Rappel details from the database
                    string query = "SELECT titre, dateRappel, description FROM Rappel WHERE rappelId = @RappelId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RappelId", rappelId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Set the details of the Rappel in the text boxes
                                textBox1.Text = reader["titre"].ToString();
                                textBox2.Text = reader["dateRappel"].ToString();
                                textBox3.Text = reader["description"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Rappel not found.");
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add additional logic here if needed
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // You can add additional logic here if needed
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // You can add additional logic here if needed
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
