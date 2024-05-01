using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Remindo.Forms
{
    public partial class AddNoteForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int utilisateurId; // Store the logged-in user's utilisateurId

        public AddNoteForm(int utilisateurId)
        {
            InitializeComponent();
            this.utilisateurId = utilisateurId; // Assign the logged-in user's utilisateurId
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string contenu = textBox1.Text;
            DateTime dateCreation = dateTimePicker1.Value;

            // Check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(contenu))
            {
                MessageBox.Show("Veuillez remplir le contenu de la note.");
                return; // Exit the method without adding the note
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
                        elementCommand.Parameters.AddWithValue("@Titre", "Note"); // Specify the titre for Note
                        elementId = Convert.ToInt32(elementCommand.ExecuteScalar());
                    }

                    // Insert into Note table with the obtained elementId
                    string noteQuery = "INSERT INTO Note (elementId, utilisateurId, contenu, dateCreation) " +
                                       "VALUES (@ElementId, @UtilisateurId, @Contenu, @DateCreation)";
                    using (MySqlCommand noteCommand = new MySqlCommand(noteQuery, connection))
                    {
                        noteCommand.Parameters.AddWithValue("@ElementId", elementId); // Use the obtained elementId
                        noteCommand.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        noteCommand.Parameters.AddWithValue("@Contenu", contenu);
                        noteCommand.Parameters.AddWithValue("@DateCreation", dateCreation);
                        noteCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Note ajoutée avec succès !");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Add any validation logic for the content field if needed
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Add any logic for the date picker if needed
        }
    }
}
