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

                    // Insert into Element table to get elementId
                    string elementQuery = "INSERT INTO Element (utilisateurId, titre) VALUES (@UtilisateurId, @Titre); SELECT LAST_INSERT_ID();";
                    int elementId;
                    using (MySqlCommand elementCommand = new MySqlCommand(elementQuery, connection))
                    {
                        elementCommand.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        elementCommand.Parameters.AddWithValue("@Titre", "Rappel"); // Specify the titre for Rappel
                        elementId = Convert.ToInt32(elementCommand.ExecuteScalar());
                    }

                    // Insert into Rappel table with the obtained elementId
                    string rappelQuery = "INSERT INTO Rappel (elementId, utilisateurId, titre, description, dateRappel) " +
                                         "VALUES (@ElementId, @UtilisateurId, @Titre, @Description, @DateRappel)";
                    using (MySqlCommand rappelCommand = new MySqlCommand(rappelQuery, connection))
                    {
                        rappelCommand.Parameters.AddWithValue("@ElementId", elementId); // Use the obtained elementId
                        rappelCommand.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        rappelCommand.Parameters.AddWithValue("@Titre", titre);
                        rappelCommand.Parameters.AddWithValue("@Description", description);
                        rappelCommand.Parameters.AddWithValue("@DateRappel", dateRappel);
                        rappelCommand.ExecuteNonQuery();
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
