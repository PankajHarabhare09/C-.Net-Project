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

namespace HeavenTasteBakery
{

    public partial class Dashboard : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Your code here, e.g.:
            MessageBox.Show("PictureBox clicked!");
            //this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Vendor vendor = new Vendor();
            vendor.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Material material = new Material();
            material.Show();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            sale.Show();
            this.Hide();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from Category";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from Sale";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from Customer";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from Material";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from Vendor";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from Staff";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from ProductTbl";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            void showcategory()
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                string query = "SELECT * from OrderTbl";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                var dataset = new DataSet();
                adapter.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];

                connection.Close();
            }
            showcategory();
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bill menu clicked!"); // Debugging step

            showCategory();
        }

        private void showCategory()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string query = "SELECT * FROM Bill";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                dataGridView1.DataSource = dataset.Tables[0];
            }
        }


        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
            this.Hide();
           
        }
       

        private void label3_Click_1(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Show();
            this.Close();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Close();
        }

        private void label10_Click_1(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
            this.Close();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Close();
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Vendor vendor = new Vendor();
            vendor.Show();
            this.Close();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            Material material = new Material();
            material.Show();
            this.Close();
        }

        private void label22_Click_1(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            sale.Show();
            this.Close();
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show();
            this.Close();
        }

        private void label19_Click_1(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();
            this.Close();
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }
    }
}
