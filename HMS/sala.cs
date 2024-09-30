using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HMS
{
    public partial class sala : Form
    {
        private string email;
        public sala(string userEmail)
        {
            InitializeComponent();
            this.email = userEmail;
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");

        private void sala_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True"))
            {
                // SQL query to fetch the username using the email
                string query = "SELECT name FROM register WHERE email = @Email";

                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        // Add the email parameter to the query
                        cmd.Parameters.AddWithValue("@Email", email);

                        // Open the connection
                        con.Open();

                        // Execute the query and get the username
                        string username = (string)cmd.ExecuteScalar();

                        // Check if username was found
                        if (!string.IsNullOrEmpty(username))
                        {
                            // Display the username in a label (assuming labelUsername exists on the form)
                            label11.Text = username;  // Assuming this is where you want to show the username
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        // Close the connection
                        con.Close();
                    }
                }
            }
            LoadStudentUsn();
            getcell();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                con.Open();

                // Create the SQL command
                SqlCommand cmd = new SqlCommand("INSERT INTO Salary_tbl (Salid, SalEmpld, SalEmpName, SalEmpPosition, SalMonth ,Amount) VALUES (@Salid, @SalEmpld, @SalEmpName, @SalEmpPosition, @SalMonth ,@Amount)", con);

                // Add parameters
                cmd.Parameters.AddWithValue("@Salid", gunaLineTextBox1.Text);
                cmd.Parameters.AddWithValue("@SalEmpld", Convert.ToInt32(comboBox1.Text));  // Assuming comboBox1 contains numeric Employee ID
                cmd.Parameters.AddWithValue("@SalEmpName", gunaLineTextBox2.Text);
                cmd.Parameters.AddWithValue("@SalEmpPosition", gunaLineTextBox3.Text);
                cmd.Parameters.AddWithValue("@SalMonth", gunaDateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(gunaLineTextBox4.Text));


                // Execute the command
                cmd.ExecuteNonQuery();

                // Reset form fields
                reset();

                // Show success message
                MessageBox.Show("Data inserted successfully.");
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                con.Close();
            }

        }



        private void reset()
        {
            // Reset the text boxes
            gunaLineTextBox1.Text = string.Empty;  // Clears the text in gunaLineTextBox1
            comboBox1.SelectedIndex = -1;  // Resets the selected item in comboBox1
            gunaLineTextBox2.Text = string.Empty;  // Clears the text in gunaLineTextBox2
            gunaLineTextBox3.Text = string.Empty;  // Clears the text in gunaLineTextBox3
            gunaDateTimePicker1.Value = DateTime.Now;  // Resets the date picker to the current date

            // Reset the combo boxes
            gunaLineTextBox4.Text = string.Empty;  // Resets the selected item in comboBox2

            // Reset the date picker

        }
        private void getcell()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Salary_tbl", con))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    gunaDataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void LoadStudentUsn()
        {
            try
            {
                // Open the database connection
                con.Open();

                // Define the SQL query
                string query = "SELECT Empid FROM Employee_tbl";

                // Create the SQL command
                SqlCommand cmd = new SqlCommand(query, con);

                // Execute the command and get the data reader
                SqlDataReader reader = cmd.ExecuteReader();

                // Create a DataTable to hold the data
                DataTable dt = new DataTable();
                dt.Columns.Add("Empid", typeof(string));
                dt.Load(reader);

                // Set the DataSource, DisplayMember, and ValueMember properties of the combo box
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Empid";
                comboBox1.ValueMember = "Empid";
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                con.Close();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                con.Open();

                // Create the SQL command for updating
                SqlCommand cmd = new SqlCommand("UPDATE Salary_tbl SET SalEmpld = @SalEmpld, SalEmpName = @SalEmpName, SalEmpPosition = @SalEmpPosition, SalMonth = @SalMonth ,Amount=@Amount WHERE Salid = @Salid", con);

                // Add parameters
                cmd.Parameters.AddWithValue("@Salid", gunaLineTextBox1.Text); // The ID of the record to update
                cmd.Parameters.AddWithValue("@SalEmpld", Convert.ToInt32(comboBox1.Text)); // Assuming comboBox1 contains numeric Employee ID
                cmd.Parameters.AddWithValue("@SalEmpName", gunaLineTextBox2.Text);
                cmd.Parameters.AddWithValue("@SalEmpPosition", gunaLineTextBox3.Text);
                cmd.Parameters.AddWithValue("@SalMonth", gunaDateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(gunaLineTextBox4.Text));

                // Execute the command
                cmd.ExecuteNonQuery();

                // Reset form fields
                reset();

                // Show success message
                MessageBox.Show("Data updated successfully.");
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                con.Close();
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEmpId = comboBox1.SelectedValue.ToString();
            FetchEmployeeDetails(selectedEmpId);
        }

        private void FetchEmployeeDetails(string empId)
        {
            string query = "SELECT Empname, EmpPos FROM Employee_tbl WHERE Empid = @Empid";

            using (SqlConnection conn = new SqlConnection(con.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Empid", empId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        gunaLineTextBox2.Text = reader["Empname"].ToString();
                        gunaLineTextBox3.Text = reader["EmpPos"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Employee not found!");
                        gunaLineTextBox2.Text = string.Empty;
                        gunaLineTextBox3.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                con.Open();

                // Create the SQL command for deleting
                SqlCommand cmd = new SqlCommand("DELETE FROM Salary_tbl WHERE Salid = @Salid", con);

                // Add parameter for the ID of the record to delete
                cmd.Parameters.AddWithValue("@Salid", gunaLineTextBox1.Text); // The ID of the record to delete

                // Execute the command
                cmd.ExecuteNonQuery();

                // Reset form fields
                reset();

                // Show success message
                MessageBox.Show("Data deleted successfully.");
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                con.Close();
            }

        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the user clicked on a row and not the header
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = gunaDataGridView1.Rows[e.RowIndex];

                // Populate the form fields with the selected row's data
                gunaLineTextBox1.Text = row.Cells["Salid"].Value.ToString(); // Assuming "Salid" is the column name
                comboBox1.Text = row.Cells["SalEmpld"].Value.ToString(); // Assuming "SalEmpld" is the column name
                gunaLineTextBox2.Text = row.Cells["SalEmpName"].Value.ToString(); // Assuming "SalEmpName" is the column name
                gunaLineTextBox3.Text = row.Cells["SalEmpPosition"].Value.ToString(); // Assuming "SalEmpPosition" is the column name
                gunaDateTimePicker1.Value = Convert.ToDateTime(row.Cells["SalMonth"].Value); // Assuming "SalMonth" is the column name
                gunaLineTextBox4.Text = Convert.ToInt32(row.Cells["Amount"].Value).ToString();// Assuming "SalEmpName" is the column name

            }
        }


        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            home h1 = new home(email);
            h1.Show();
            this.Hide();

        }
    }
}

