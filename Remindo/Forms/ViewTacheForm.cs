using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Remindo.Forms
{
    public partial class ViewTacheForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int tacheId; // Store the ID of the Tache to load

        public ViewTacheForm(int tacheId)
        {
            InitializeComponent();
            this.tacheId = tacheId;

            // Load Tache details when the form is loaded
            LoadTacheDetails();
        }

        private void LoadTacheDetails()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch Tache details from the database
                    string query = "SELECT titre, dateCreation, DateRealisation, statut, description FROM Tache WHERE tacheId = @TacheId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TacheId", tacheId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Set the details of the Tache in the text boxes
                                textBox1.Text = reader["titre"].ToString();
                                textBox2.Text = reader["dateCreation"].ToString();
                                textBox3.Text = reader["DateRealisation"].ToString();
                                textBox4.Text = reader["statut"].ToString();
                                textBox5.Text = reader["description"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Tache not found.");
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // You can add additional logic here if needed
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // You can add additional logic here if needed
        }
    }
}
