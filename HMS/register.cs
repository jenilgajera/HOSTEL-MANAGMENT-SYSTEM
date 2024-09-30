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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HMS
{
    public partial class register : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True");

        public register()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            demo d1 = new demo();
            d1.Show();
            this.Hide();
                           
        }

        private void register_Load(object sender, EventArgs e)
        {
            textfieldclear();
        }

        private void textfieldclear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.net\\HMS\\HMS\\hostel.mdf;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("Insert Into register VALUES(@name,@email,@password)", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@email", textBox2.Text);
                    cmd.Parameters.AddWithValue("@password", textBox3.Text);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Register");
                    textfieldclear();

                }
            }
        }
    }
}
