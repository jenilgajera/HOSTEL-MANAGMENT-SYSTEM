using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS
{
    public partial class rooms : Form
    {
        private string email;
        public rooms(string userEmail)
        {
            InitializeComponent();
            email = userEmail;
        }

        private void rooms_Load(object sender, EventArgs e)
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
                            label6.Text = username;  // Assuming this is where you want to show the username
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
            getrooms();
        }
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True";

        private void getrooms()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Room_tbl", con))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    gunaDataGridView1.DataSource = dt;
                }
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        void reset()
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string yesNoValue;

            if (radioButton1.Checked)
            {
                yesNoValue = "Yes";
            }
            else if (radioButton2.Checked)
            {
                yesNoValue = "No";
            }
            else
            {
                MessageBox.Show("Please select Yes or No.");
                return;
            }

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert Into Room_tbl VALUES(@RoomNum,@RoomStatus,@Booked)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@RoomNum", textBox1.Text);
            cmd.Parameters.AddWithValue("@RoomStatus", comboBox1.Text);
            cmd.Parameters.AddWithValue("@Booked", yesNoValue);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully inserted");
            getrooms();
            reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Please select Yes or No for Booked value.");
                return;
            }

            string yesNoValue = radioButton1.Checked ? "Yes" : "No";

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");

            try
            {
                con.Open();

                // Assuming RoomNum is the primary key
                string query = "UPDATE Room_tbl SET RoomStatus=@RoomStatus, Booked=@Booked WHERE RoomNum=@RoomNum";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@RoomNum", textBox1.Text);
                cmd.Parameters.AddWithValue("@RoomStatus", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Booked", yesNoValue);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    MessageBox.Show("No record updated. Check if the Room Number exists.");
                }
                else
                {
                    MessageBox.Show("Successfully updated.");
                    getrooms(); // Call your function to refresh data
                    reset();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Handle primary key constraint violation (unique constraint error)
                {
                    MessageBox.Show("Error updating: Room number might already exist.");
                }
                else
                {
                    MessageBox.Show("Error updating data: " + ex.Message);
                }
            }
            finally
            {
                con.Close();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = gunaDataGridView1.Rows[e.RowIndex];

            textBox1.Text = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            comboBox1.Text = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string radioButtonValue = selectedRow.Cells[2].Value.ToString();

            radioButton1.Checked = radioButtonValue == "Yes";
            radioButton2.Checked = radioButtonValue == "No";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = gunaDataGridView1.SelectedRows[0].Index;
                int roomNum = Convert.ToInt32(gunaDataGridView1.Rows[selectedIndex].Cells["RoomNum"].Value);

                try
                {
                    SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
                    con.Open();

                    string query = "DELETE FROM Room_tbl WHERE RoomNum = @RoomNum";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RoomNum", roomNum);
                    cmd.ExecuteNonQuery();

                    gunaDataGridView1.Rows.RemoveAt(selectedIndex);
                    MessageBox.Show("Record deleted successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

      

        private void button4_Click_1(object sender, EventArgs e)
        {
            home h1=new home(email);
            h1.Show();
            this.Hide();

                
        }
    }
}
