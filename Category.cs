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
    public partial class Category : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int Caid = 0;
        public Category()
        {
            InitializeComponent();
            showdata();
        }

        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from Category";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }
        private void Category_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }

        private void GenerateAutoID()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(Caid)from Category", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            CategoryId.Text = Caid + i.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            showdata();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {

                string insert = "INSERT into Category VALUES(@Caid , @Ctype , @Cdes ,  @TotalProduct , @ProductCount)";

                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("@Caid", CategoryId.Text);
                command.Parameters.AddWithValue("@Ctype", CategoryType.Text);
                command.Parameters.AddWithValue("@Cdes", Description.Text);
                command.Parameters.AddWithValue("@TotalProduct", Convert.ToInt32(Ilevel.Text));
                command.Parameters.AddWithValue("@ProductCount", Convert.ToInt32(Pcount.Text));

                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    MessageBox.Show("Category Added SuccessFully..!!!");
                }
                else
                {
                    MessageBox.Show("Try Again..!!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                showdata();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                showdata();
                string update = "UPDATE Category SET Caid=@Caid , Ctype=@Ctype , Cdes=@Cdes ,  TotalProduct=@TotalProduct , ProductCount=@ProductCount WHERE Caid=@Caid";
                SqlCommand command = new SqlCommand(update, connection);

                command.Parameters.AddWithValue("@Caid", CategoryId.Text);
                command.Parameters.AddWithValue("@Ctype", CategoryType.Text);
                command.Parameters.AddWithValue("@Cdes", Description.Text);
                command.Parameters.AddWithValue("@TotalProduct", Convert.ToInt32(Ilevel.Text));
                command.Parameters.AddWithValue("@ProductCount", Convert.ToInt32(Pcount.Text));

                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    MessageBox.Show("Category Updated SuccessFully..!!!");
                }
                else
                {
                    MessageBox.Show("Try Again..!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.Close();
                showdata();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string search = "select * from Category WHERE Caid=@Caid";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@Caid", CategoryId.Text);


            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No Record Found....!!!!!");
                dataGridView1.DataSource = null;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
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
            this.Hide();

        }

        private void label22_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            sale.Show();
            this.Hide();
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

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryId.Clear();
            Description.Clear();
            Ilevel.Clear();
            CategoryType.Items.Clear();
            Pcount.Clear();
        }
    }
}
