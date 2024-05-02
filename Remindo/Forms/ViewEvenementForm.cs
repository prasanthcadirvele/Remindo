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
    public partial class ViewEvenementForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int elementId;

        public ViewEvenementForm(int elementId)
        {
            InitializeComponent();
            this.elementId = elementId;

            // Call method to load Evenement details
            LoadEvenementDetails();
        }

        private void LoadEvenementDetails()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch Evenement details based on elementId
                    string query = "SELECT titre, dateDebut, dateFin, description FROM Evenement WHERE elementId = @ElementId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ElementId", elementId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Display Evenement details in textboxes
                                textBox1.Text = reader["titre"].ToString();
                                textBox2.Text = reader["dateDebut"].ToString();
                                textBox3.Text = reader["dateFin"].ToString();
                                textBox4.Text = reader["description"].ToString();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
