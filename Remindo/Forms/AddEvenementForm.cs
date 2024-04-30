﻿using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Remindo.Forms
{
    public partial class AddEvenementForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int utilisateurId; // Store the logged-in user's utilisateurId

        public AddEvenementForm(int utilisateurId)
        {
            InitializeComponent();

            // Assign the logged-in user's utilisateurId
            this.utilisateurId = utilisateurId;
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

            // Check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return; // Exit the method without adding the event
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open connection
                    connection.Open();

                    // Insert new evenement
                    string query = "INSERT INTO Evenement (utilisateurId, titre, dateDebut, dateFin, description) " +
                                   "VALUES (@UtilisateurId, @Titre, @DateDebut, @DateFin, @Description)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
                        command.Parameters.AddWithValue("@Titre", titre);
                        command.Parameters.AddWithValue("@DateDebut", dateDebut);
                        command.Parameters.AddWithValue("@DateFin", dateFin);
                        command.Parameters.AddWithValue("@Description", description);
                        command.ExecuteNonQuery();
                    }

                    // Insert into Element table
                    query = "INSERT INTO Element (utilisateurId, titre) VALUES (@UtilisateurId, @Titre)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtilisateurId", utilisateurId); // Add utilisateurId parameter
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

