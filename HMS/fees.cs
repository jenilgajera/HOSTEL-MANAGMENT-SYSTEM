using Guna.UI.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS
{
    public partial class fees : Form
    {
        private string email;


        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");

        public fees(string userEmail)
        {
            InitializeComponent();
            this.email = userEmail;
        }
        private void fees_Load(object sender, EventArgs e)
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
                            label11.Text = username;
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }
                    }
                    catch (Exception ex)
                    {
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
            getroomno();
            getcell();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;


        }
    

public class StudentInfoFetcher
    {
        private SqlConnection con;

        public StudentInfoFetcher()
        {
            con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");
        }

        public void FetchStudentDetails(string StdUsn, out string studentName, out int roomNo)
        {
            studentName = string.Empty;
            roomNo = -1;

            string query = "SELECT StdName, StdRoom FROM Student_tbl WHERE StdUsn = @StdUsn";

            try
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StdUsn", StdUsn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        studentName = reader["StdName"].ToString();
                        roomNo = Convert.ToInt32(reader["StdRoom"]);
                    }
                    else
                    {
                        MessageBox.Show("Student not found!");
                    }
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
    }

    private void FetchAndDisplayStudentDetails(string StdUsn)
        {
            StudentInfoFetcher fetcher = new StudentInfoFetcher();

            string studentName;
            int roomNo;

            fetcher.FetchStudentDetails(StdUsn, out studentName, out roomNo);

            if (!string.IsNullOrEmpty(studentName))
            {
                gunaLineTextBox1.Text = studentName; 
                gunaLineTextBox2.Text = roomNo.ToString(); 
            }
        }

        private void getcell()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Fees_tbl", con))
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
   
           
        }

      
        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gunaDataGridView1.SelectedRows[0];
                gunaLineTextBox1.Text = selectedRow.Cells[0].Value.ToString();
                comboBox1.Text = selectedRow.Cells[1].Value.ToString();
                gunaLineTextBox2.Text = selectedRow.Cells[2].Value.ToString();
                comboBox2.Text = selectedRow.Cells[3].Value.ToString();

                if (DateTime.TryParse(selectedRow.Cells[4].Value.ToString(), out DateTime date))
                {
                    gunaDateTimePicker1.Value = date;
                }
                else
                {
                    MessageBox.Show("Invalid date format.");
                }
                gunaLineTextBox3.Text = selectedRow.Cells[5].Value.ToString();

            }
        }

        private void LoadStudentUsn()
        {
            try
            {
          
                con.Open();

               
                string query = "SELECT StdUsn FROM Student_tbl";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("StdUsn", typeof(string));
                dt.Load(reader);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "StdUsn";
                comboBox1.ValueMember = "StdUsn";
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

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void gunaLineTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

       
        
        private void getroomno()
        {
            try
            {
                con.Open();

                string query = "SELECT RoomNum FROM Room_tbl";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("RoomNum", typeof(string));
                dt.Load(reader);

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "RoomNum";
                comboBox2.ValueMember = "RoomNum";
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


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Fees_tbl (PaymentId, StudentUsn, StudentName, StdRoom, PaymentMonth, Amount) VALUES (@PaymentId, @StudentUsn, @StudentName, @StdRoom, @PaymentMonth, @Amount)", con);

                cmd.Parameters.AddWithValue("@PaymentId", Convert.ToInt32(gunaLineTextBox1.Text)); 
                cmd.Parameters.AddWithValue("@StudentUsn", comboBox1.Text); 
                cmd.Parameters.AddWithValue("@StudentName", gunaLineTextBox3.Text);
                cmd.Parameters.AddWithValue("@StdRoom", Convert.ToInt32(comboBox2.Text)); 
                cmd.Parameters.AddWithValue("@PaymentMonth", gunaDateTimePicker1.Value.ToString("MMMM yyyy"));
                cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(gunaLineTextBox3.Text));

                cmd.ExecuteNonQuery();

              
                reset();

                
                MessageBox.Show("Data inserted successfully.");
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

        private void reset()
        {
            gunaLineTextBox1.Clear();
            gunaLineTextBox2.Clear();
            gunaLineTextBox3.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            gunaDateTimePicker1.ResetText();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Fees_tbl SET StudentUsn=@StudentUsn, StudentName=@StudentName, StdRoom=@StdRoom, PaymentMonth=@PaymentMonth, Amount=@Amount WHERE PaymentId=@PaymentId", con);

              
                cmd.Parameters.AddWithValue("@PaymentId", gunaLineTextBox1.Text);
                cmd.Parameters.AddWithValue("@StudentUsn", comboBox1.Text);
                cmd.Parameters.AddWithValue("@StudentName", gunaLineTextBox2.Text);
                cmd.Parameters.AddWithValue("@StdRoom", comboBox2.Text);

                cmd.Parameters.AddWithValue("@PaymentMonth", gunaDateTimePicker1.Value.ToString("yyyy-MM-dd"));

                cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(gunaLineTextBox3.Text));

                cmd.ExecuteNonQuery();

                reset();
                MessageBox.Show("Data updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
                getcell(); 
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(gunaLineTextBox1.Text))
                {
                    MessageBox.Show("Please select a record to delete.");
                    return;
                }

                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Fees_tbl WHERE PaymentId=@PaymentId", con);
                cmd.Parameters.AddWithValue("@PaymentId", gunaLineTextBox1.Text);
                cmd.ExecuteNonQuery();
                reset();
                MessageBox.Show("Record deleted successfully.");
                getcell(); 
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            home h1 = new home(email);
            h1.Show();
            this.Hide();

        }
    }
}
