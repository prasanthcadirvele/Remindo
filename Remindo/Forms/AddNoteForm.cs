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
        private object titre;

        public AddNoteForm()
        {
            InitializeComponent();
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

                    // Insert new note
                    string noteQuery = "INSERT INTO Note (contenu, dateCreation) VALUES (@Contenu, @DateCreation)";
                    using (MySqlCommand noteCommand = new MySqlCommand(noteQuery, connection))
                    {
                        noteCommand.Parameters.AddWithValue("@Contenu", contenu);
                        noteCommand.Parameters.AddWithValue("@DateCreation", dateCreation);
                        noteCommand.ExecuteNonQuery();
                    }

                    // Insert into Element table with title "Note"
                    string elementQuery = "INSERT INTO Element (titre) VALUES (@Titre)";
                    using (MySqlCommand elementCommand = new MySqlCommand(elementQuery, connection))
                    {
                        elementCommand.Parameters.AddWithValue("@Titre", "Note");
                        elementCommand.ExecuteNonQuery();
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

