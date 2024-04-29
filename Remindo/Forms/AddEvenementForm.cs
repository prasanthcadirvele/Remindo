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
    public partial class AddEvenementForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";

        public AddEvenementForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titre = "Evenement"; // Default value for now
            DateTime dateDebut = dateTimePicker2.Value;
            DateTime dateFin = dateTimePicker3.Value;
            string description = textBox2.Text;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open connection
                    connection.Open();

                    // Insert new evenement
                    string query = "INSERT INTO Evenement (titre, dateDebut, dateFin, description) " +
                                   "VALUES (@Titre, @DateDebut, @DateFin, @Description)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Titre", titre);
                        command.Parameters.AddWithValue("@DateDebut", dateDebut);
                        command.Parameters.AddWithValue("@DateFin", dateFin);
                        command.Parameters.AddWithValue("@Description", description);
                        command.ExecuteNonQuery();
                    }

                    // Insert into Element table
                    query = "INSERT INTO Element (titre) VALUES (@Titre)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Titre", titre);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Evenement ajouté avec succès !");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddEvenementForm_Load(object sender, EventArgs e)
        {

        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
