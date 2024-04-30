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
            LoadDataGridViewData(utilisateurId);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Actions"].Index)
            {
                int elementId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ElementID"].Value);

                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    string buttonName = dataGridView1.Columns[e.ColumnIndex].Name;

                    if (buttonName == "View")
                    {
                        // Open window to view details of the element with ID elementId
                        ViewElementDetails(elementId);
                    }
                    else if (buttonName == "Delete")
                    {
                        // Remove row from DataGridView
                        dataGridView1.Rows.RemoveAt(e.RowIndex);

                        // Delete corresponding record from the database
                        DeleteElementFromDatabase(elementId);
                    }
                }
            }
        }

        private void LoadDataGridViewData(int utilisateurId)
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

                                // Add buttons to the "Actions" column for each row
                                DataGridViewButtonCell viewButtonCell = new DataGridViewButtonCell();
                                viewButtonCell.Value = "View";
                                row.Cells[2] = viewButtonCell;

                                DataGridViewButtonCell deleteButtonCell = new DataGridViewButtonCell();
                                deleteButtonCell.Value = "Delete";
                                row.Cells[2] = deleteButtonCell;

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
            // Open window to view details of the element with ID elementId
            // Implement this method to open a new window and display details of the element
            MessageBox.Show($"View button clicked for Element ID: {elementId}");
        }

        private void DeleteElementFromDatabase(int elementId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete record from database based on elementId
                    string query = "DELETE FROM Element WHERE elementId = @ElementId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ElementId", elementId);
                        command.ExecuteNonQuery();
                    }

                    // Optionally, delete from other tables as needed
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
