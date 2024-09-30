using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    public partial class home : Form
    {
        private string email;
        public home(string userEmail)
        {
            InitializeComponent();
            email = userEmail;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            rooms myrooms = new rooms(email);
            myrooms.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            student student = new student(email);
            student.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            employee employee = new employee(email);
            employee.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fees myfees = new fees(email);
            myfees.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sala s = new sala(email);
            s.Show();
            this.Hide();
        }

        private void home_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True"))
            {
                
                string query = "SELECT name FROM register WHERE email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        con.Open();

                        string username = (string)cmd.ExecuteScalar();

                        if (!string.IsNullOrEmpty(username))
                        {
                            // Display the username in a label (assuming labelUsername exists on the form)
                            label5.Text = username;  // Assuming this is where you want to show the username
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
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
