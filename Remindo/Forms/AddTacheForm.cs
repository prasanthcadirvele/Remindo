using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Remindo.Forms
{
    public partial class AddTacheForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int utilisateurId; // Store the logged-in user's utilisateurId

        public AddTacheForm(int utilisateurId)
        {
            InitializeComponent();

            // Assign the logged-in user's utilisateurId
            this.utilisateurId = utilisateurId;

            // Populate the combo box with status options
            PopulateStatusOptions();
        }

        private void PopulateStatusOptions()
        {
            // Clear existing items
            comboBox1.Items.Clear();

            // Add the desired status options
            comboBox1.Items.AddRange(new string[] { "Pas commencé", "En cours", "Terminé" });

            // Set default selection
            comboBox1.SelectedIndex = 0; // Set the default selection to "Pas commencé"
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titre = textBox1.Text;
            string description = textBox2.Text;
            DateTime dateCreation = dateTimePicker1.Value;
            DateTime dateRealisation = dateTimePicker2.Value;
            string statut = comboBox1.Text;

            // Check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(titre) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(statut))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return; // Exit the method without adding the Tache
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
                        elementCommand.Parameters.AddWithValue("@Titre", "Tache"); // Specify the titre for Tache
                        elementId = Convert.ToInt32(elementCommand.ExecuteScalar());
                    }

                    // Insert into Tache table with the obtained elementId
                    string tacheQuery = "INSERT INTO Tache (elementId, utilisateurId, titre, description, dateCreation, DateRealisation, statut) " +
                                        "VALUES (@ElementId, @UtilisateurId, @Titre, @Description, @DateCreation, @DateRealisation, @Statut)";
                    using (MySqlCommand tacheCommand = new MySqlCommand(tacheQuery, connection))
                    {
                        tacheCommand.Parameters.AddWithValue("@ElementId", elementId); // Use the obtained elementId
                        tacheCommand.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        tacheCommand.Parameters.AddWithValue("@Titre", titre);
                        tacheCommand.Parameters.AddWithValue("@Description", description);
                        tacheCommand.Parameters.AddWithValue("@DateCreation", dateCreation);
                        tacheCommand.Parameters.AddWithValue("@DateRealisation", dateRealisation);
                        tacheCommand.Parameters.AddWithValue("@Statut", statut);
                        tacheCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Tache ajoutée avec succès !");
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

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
