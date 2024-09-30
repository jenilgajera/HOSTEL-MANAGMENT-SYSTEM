using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS
{
    public partial class student : Form
    {
        private string email;

        public student(string useremail)
        {
            InitializeComponent();
            this.email = useremail;
        }
        void reset()
        {
            gunaLineTextBox1.Clear();
            gunaLineTextBox2.Clear();
            gunaLineTextBox3.Clear();
            gunaLineTextBox4.Clear();
            gunaLineTextBox5.Clear();
            gunaLineTextBox6.Clear();
            comboBox1.SelectedIndex = -1; // Reset selected index of comboBox1
            comboBox3.SelectedIndex = -1; // Reset selected index of comboBox1
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("insert into Student_tbl  values(@StdUsn, @StdName, @FatherName,@Mothername,@StdAddess ,@Collage,@StdRoom,@StdStatus)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@StdUsn", gunaLineTextBox1.Text);
            cmd.Parameters.AddWithValue("@StdName", gunaLineTextBox2.Text);
            cmd.Parameters.AddWithValue("@FatherName", gunaLineTextBox3.Text);
            cmd.Parameters.AddWithValue("@Mothername", gunaLineTextBox4.Text);
            cmd.Parameters.AddWithValue("@StdAddess", gunaLineTextBox5.Text);
            cmd.Parameters.AddWithValue("@Collage", gunaLineTextBox6.Text);
            cmd.Parameters.AddWithValue("@StdRoom", Convert.ToInt32(comboBox1.Text));
            cmd.Parameters.AddWithValue("@StdStatus", comboBox3.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully insert");
            getstuden();
            reset();
        }

        private void student_Load(object sender, EventArgs e)
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
                            label13.Text = username;  // Assuming this is where you want to show the username
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
            getstuden();
            getroomno();
        }

        private void getroomno()
        {
            try
            {
                // Open the database connection
                con.Open();

                // Define the SQL query
                string query = "SELECT RoomNum FROM Room_tbl";

                // Create the SQL command
                SqlCommand cmd = new SqlCommand(query, con);

                // Execute the command and get the data reader
                SqlDataReader reader = cmd.ExecuteReader();

                // Create a DataTable to hold the data
                DataTable dt = new DataTable();
                dt.Columns.Add("RoomNum", typeof(string));
                dt.Load(reader);

                // Set the DataSource, DisplayMember, and ValueMember properties of the combo box
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "RoomNum";
                comboBox1.ValueMember = "RoomNum";
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

        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True";

        private void getstuden()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Student_tbl", con))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    gunaDataGridView1.DataSource = dt;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update Student_tbl set StdUsn=@StdUsn, StdName=@StdName, FatherName=@FatherName,Mothername=@Mothername,StdAddess=@StdAddess ,Collage=@Collage,StdRoom=@StdRoom,StdStatus=@StdStatus  where StdUsn=@StdUsn ", con);
            con.Open();
            cmd.Parameters.AddWithValue("@StdUsn", gunaLineTextBox1.Text);
            cmd.Parameters.AddWithValue("@StdName", gunaLineTextBox2.Text);
            cmd.Parameters.AddWithValue("@FatherName", gunaLineTextBox3.Text);
            cmd.Parameters.AddWithValue("@Mothername", gunaLineTextBox4.Text);
            cmd.Parameters.AddWithValue("@StdAddess", gunaLineTextBox5.Text);
            cmd.Parameters.AddWithValue("@Collage", gunaLineTextBox6.Text);
            cmd.Parameters.AddWithValue("@StdRoom", comboBox1.Text);
            cmd.Parameters.AddWithValue("@StdStatus", comboBox3.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully update");
            getstuden();
            reset();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            gunaLineTextBox1.Text = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            gunaLineTextBox2.Text = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            gunaLineTextBox3.Text = gunaDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            gunaLineTextBox4.Text = gunaDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            gunaLineTextBox5.Text = gunaDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            gunaLineTextBox6.Text = gunaDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            comboBox1.Text = gunaDataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            comboBox3.Text = gunaDataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count > 0)
            {


                try
                {
                    SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
                    con.Open();

                    string query = "DELETE FROM Student_tbl WHERE StdUsn=@StdUsn";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StdUsn", gunaLineTextBox1.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record deleted successfully.");
                    reset();
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
