using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS
{
    public partial class employee : Form
    {
        private string email;

        public employee(string userEmail)
        {
            InitializeComponent();
            email = userEmail;
        }
        private void employee_Load(object sender, EventArgs e)
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
                            label10.Text = username;  // Assuming this is where you want to show the username
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
            getemployee();
        }

        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True";

        private void getemployee()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee_tbl", con))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    gunaDataGridView1.DataSource = dt;
                }
            }
        }

        void reset()
        {
            gunaLineTextBox1.Clear();
            gunaLineTextBox2.Clear();
            gunaLineTextBox3.Clear();
            gunaLineTextBox4.Clear();
            comboBox1.SelectedIndex = -1; // Reset selected index of comboBox1
            comboBox2.SelectedIndex = -1; // Reset selected index of comboBox1
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update Employee_tbl set Empname=@Empname, EmpPhone=@EmpPhone, EmpAddress=@EmpAddress,EmpPos=@EmpPos,EmpSatus=@EmpSatus where Empid=@Empid ", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Empid", gunaLineTextBox1.Text);
            cmd.Parameters.AddWithValue("@Empname", gunaLineTextBox2.Text);
            cmd.Parameters.AddWithValue("@EmpPhone", gunaLineTextBox3.Text);
            cmd.Parameters.AddWithValue("@EmpAddress", gunaLineTextBox4.Text);
            cmd.Parameters.AddWithValue("@EmpPos", comboBox1.Text);
            cmd.Parameters.AddWithValue("@EmpSatus", comboBox2.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully update");
            getemployee();
            reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert Into Employee_tbl VALUES(@Empid,@Empname,@EmpPhone,@EmpAddress,@EmpPos,@EmpSatus)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Empid", gunaLineTextBox1.Text);
            cmd.Parameters.AddWithValue("@Empname", gunaLineTextBox2.Text);
            cmd.Parameters.AddWithValue("@EmpPhone", gunaLineTextBox3.Text);
            cmd.Parameters.AddWithValue("@EmpAddress", gunaLineTextBox4.Text);
            cmd.Parameters.AddWithValue("@EmpPos", comboBox1.Text);
            cmd.Parameters.AddWithValue("@EmpSatus", comboBox2.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully inserted");
            getemployee();
            reset();
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            gunaLineTextBox1.Text = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            gunaLineTextBox2.Text = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            gunaLineTextBox3.Text = gunaDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            gunaLineTextBox4.Text = gunaDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = gunaDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox2.Text = gunaDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("delete from Employee_tbl where Empid=@Empid", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Empid", gunaLineTextBox1.Text);


            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully delete");
            getemployee();
            reset();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            home f1= new home(email);
            f1.Show();
            this.Hide();
        }
    }
}
