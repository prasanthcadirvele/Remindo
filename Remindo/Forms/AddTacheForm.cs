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

                    // Insert into Tache table
                    string query = "INSERT INTO Tache (utilisateurId, titre, description, dateCreation, DateRealisation, statut) " +
                                   "VALUES (@UtilisateurId, @Titre, @Description, @DateCreation, @DateRealisation, @Statut)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        command.Parameters.AddWithValue("@Titre", titre);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@DateCreation", dateCreation);
                        command.Parameters.AddWithValue("@DateRealisation", dateRealisation);
                        command.Parameters.AddWithValue("@Statut", statut);
                        command.ExecuteNonQuery();
                    }

                    // Insert into Element table
                    query = "INSERT INTO Element (utilisateurId, titre) VALUES (@UtilisateurId, @Titre)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        command.Parameters.AddWithValue("@Titre", "Tache");
                        command.ExecuteNonQuery();
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
