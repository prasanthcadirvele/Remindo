using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Remindo.Forms
{
    public partial class ViewNoteForm : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int noteId; // Store the ID of the note to load

        public ViewNoteForm(int noteId)
        {
            InitializeComponent();
            this.noteId = noteId;

            // Load note details when the form is loaded
            LoadNoteDetails();
        }

        private void LoadNoteDetails()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch note details from the database
                    string query = "SELECT contenu, dateCreation FROM Note WHERE noteId = @NoteId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoteId", noteId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Set the content and creation date of the note in the text boxes
                                textBox1.Text = reader["contenu"].ToString();
                                textBox2.Text = reader["dateCreation"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Note not found.");
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
    }
}
