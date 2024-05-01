using Remindo.Class;
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
    public partial class Home : Form
    {
        private readonly string connectionString = "server=localhost;database=Remindo;uid=root;";
        private readonly int utilisateurId;
        public Home(int utilisateurId)
        {
            InitializeComponent();
            this.utilisateurId = utilisateurId; // Store the utilisateurId
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadDataGridView1Data(utilisateurId);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Check if the clicked cell is in the "ActionView" column
                if (e.ColumnIndex == dataGridView1.Columns["ActionView"].Index)
                {
                    // Handle the view action
                    int elementId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ElementID"].Value);
                    ViewElementDetails(elementId);
                }
                // Check if the clicked cell is in the "ActionDelete" column
                else if (e.ColumnIndex == dataGridView1.Columns["ActionDelete"].Index)
                {
                    // Handle the delete action
                    int elementId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ElementID"].Value);
                    DeleteElement(elementId);
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void LoadDataGridView1Data(int utilisateurId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch data from the Element table specific to the logged-in user
                    string query = "SELECT elementId, titre FROM Element WHERE utilisateurId = @UtilisateurId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtilisateurId", utilisateurId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create a new row and add data to it
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dataGridView1);
                                row.Cells[0].Value = reader["elementId"];
                                row.Cells[1].Value = reader["titre"];

                                // Add buttons to the "ActionView" column for each row
                                DataGridViewButtonCell viewButtonCell = new DataGridViewButtonCell();
                                viewButtonCell.Value = "View";
                                row.Cells[2] = viewButtonCell;

                                // Add buttons to the "ActionDelete" column for each row
                                DataGridViewButtonCell deleteButtonCell = new DataGridViewButtonCell();
                                deleteButtonCell.Value = "Delete";
                                row.Cells[3] = deleteButtonCell;

                                // Add the row to the DataGridView
                                dataGridView1.Rows.Add(row);
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


        private void ViewElementDetails(int elementId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Determine the type of element based on the row data
                    string query = "SELECT type FROM Element WHERE elementId = @ElementId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ElementId", elementId);
                        string elementType = command.ExecuteScalar().ToString();

                        // Open the corresponding AddForm
                        Form addForm = null;
                        switch (elementType)
                        {
                            case "Evenement":
                                addForm = new AddEvenementForm(elementId);
                                break;
                            case "Note":
                                addForm = new AddNoteForm(elementId);
                                break;
                            case "Rappel":
                                addForm = new AddRappelForm(elementId);
                                break;
                            case "Tache":
                                addForm = new AddTacheForm(elementId);
                                break;
                            default:
                                MessageBox.Show("Unknown element type.");
                                return;
                        }

                        // Pass the element ID to the AddForm
                        addForm.Tag = elementId;

                        // Disable editing in the AddForm
                        foreach (Control control in addForm.Controls)
                        {
                            if (control is TextBox)
                            {
                                ((TextBox)control).ReadOnly = true;
                            }
                            // Add similar handling for other controls like DateTimePicker, ComboBox, etc.
                        }

                        // Show the AddForm
                        addForm.ShowDialog();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteElement(int elementId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Determine the type of element based on the row data
                    string query = "SELECT type FROM Element WHERE elementId = @ElementId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ElementId", elementId);
                        string elementType = command.ExecuteScalar().ToString();

                        // Delete the record from the appropriate table based on the element type
                        switch (elementType)
                        {
                            case "Evenement":
                                DeleteFromEvenementTable(elementId, connection);
                                break;
                            case "Note":
                                DeleteFromNoteTable(elementId, connection);
                                break;
                            case "Rappel":
                                DeleteFromRappelTable(elementId, connection);
                                break;
                            case "Tache":
                                DeleteFromTacheTable(elementId, connection);
                                break;
                            default:
                                MessageBox.Show("Unknown element type.");
                                return;
                        }

                        // Delete the record from the Element table
                        query = "DELETE FROM Element WHERE elementId = @ElementId";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteFromEvenementTable(int elementId, MySqlConnection connection)
        {
            // Delete record from the Evenement table based on elementId
            string query = "DELETE FROM Evenement WHERE elementId = @ElementId";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ElementId", elementId);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteFromNoteTable(int elementId, MySqlConnection connection)
        {
            // Delete record from the Note table based on elementId
            string query = "DELETE FROM Note WHERE elementId = @ElementId";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ElementId", elementId);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteFromRappelTable(int elementId, MySqlConnection connection)
        {
            // Delete record from the Rappel table based on elementId
            string query = "DELETE FROM Rappel WHERE elementId = @ElementId";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ElementId", elementId);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteFromTacheTable(int elementId, MySqlConnection connection)
        {
            // Delete record from the Tache table based on elementId
            string query = "DELETE FROM Tache WHERE elementId = @ElementId";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ElementId", elementId);
                command.ExecuteNonQuery();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open the form to add an evenement
            AddEvenementForm addEvenementForm = new AddEvenementForm(utilisateurId);
            addEvenementForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddTacheForm addTacheForm = new AddTacheForm(utilisateurId);
            addTacheForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddNoteForm addNoteForm = new AddNoteForm(utilisateurId);
            addNoteForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddRappelForm addRappelForm = new AddRappelForm(utilisateurId);
            addRappelForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create an instance of the User class
            User user = new User(null, null);

            // Call the Logout method to clear user credentials
            user.Logout();

            // Redirect to the login form
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
            this.Hide(); // Hide the current form
        }
    }
}
