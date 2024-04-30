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
    public partial class AddRappelForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int utilisateurId; // Store the logged-in user's utilisateurId

        public AddRappelForm(int utilisateurId)
        {
            InitializeComponent();
            this.utilisateurId = utilisateurId; // Assign the logged-in user's utilisateurId
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titre = textBox1.Text;
            string description = textBox2.Text;
            DateTime dateRappel = dateTimePicker1.Value;

            // Check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(titre) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return; // Exit the method without adding the Rappel
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open connection
                    connection.Open();

                    // Insert into Rappel table
                    string query = "INSERT INTO Rappel (utilisateurId, titre, description, dateRappel) VALUES (@UtilisateurId, @Titre, @Description, @DateRappel)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        command.Parameters.AddWithValue("@Titre", titre);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@DateRappel", dateRappel);
                        command.ExecuteNonQuery();
                    }

                    // Insert into Element table
                    query = "INSERT INTO Element (utilisateurId, titre) VALUES (@UtilisateurId, @Titre)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        command.Parameters.AddWithValue("@Titre", "Rappel");
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Rappel ajouté avec succès !");
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AddRappelForm_Load(object sender, EventArgs e)
        {

        }
    }
}
