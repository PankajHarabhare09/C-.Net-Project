using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavenTasteBakery
{
    public partial class Product : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int pid = 0;

        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from ProductTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }
        public Product()
        {
            InitializeComponent();
            showdata();
        }

        private void GenerateAutoID()
        {
            //throw new NotImplementedException();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(pid)from ProductTbl", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            Pid.Text = pid + i.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt16(pqty.Text);
            decimal price = Convert.ToDecimal(Pprice.Text);

            decimal total = qty * price; // storing the total price of multiple Quantity in total variable
            Total.Text = total.ToString(); // that total price we'll give to total.text textbox.

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                // Product Name Validation: Check if the name contains any numbers
                string productName = Pname.Text.Trim();
                if (productName.Any(char.IsDigit)) // Checks if there is any digit in the name
                {
                    MessageBox.Show("Product Name must not contain numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                string insert = "INSERT into ProductTbl VALUES(@pid , @Pname , @Pcategory , @Pquantity , @pprice , @totalamt)";

                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("@pid", Pid.Text);
                command.Parameters.AddWithValue("@Pname", productName); // Use the trimmed product name
                command.Parameters.AddWithValue("@Pcategory", Ptype.Text);
                command.Parameters.AddWithValue("@Pquantity", qty); // qty is a variable for Product Quantity textbox text
                command.Parameters.AddWithValue("@pprice", price); // price is a variable for Product Price textbox text
                command.Parameters.AddWithValue("@totalamt", Total.Text);

                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Product Details Added Successfully..!!!");
                }
                else
                {
                    MessageBox.Show("Something Went Wrong..!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

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

        private void label10_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt16(pqty.Text);
            decimal price = Convert.ToDecimal(Pprice.Text);

            decimal total = qty * price; // storing the total price of multiple Quantity in total variable
            Total.Text = total.ToString(); // that total price we'll give to total.text textbox.

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                String update = "UPDATE ProductTbl SET pid = @pid , Pname = @Pname , Pcategory = @Pcategory , Pquantity = @Pquantity , pprice = @pprice , totalamt = @totalamt WHERE pid = @pid";

                SqlCommand command = new SqlCommand(update, connection);
                command.Parameters.AddWithValue("@pid", Pid.Text);
                command.Parameters.AddWithValue("@Pname", Pname.Text);
                command.Parameters.AddWithValue("@Pcategory", Ptype.Text);
                command.Parameters.AddWithValue("@Pquantity", qty);// qty is a variable for Product Quantity textbox text
                command.Parameters.AddWithValue("@pprice", price); // price is a variable for Product Price textbox text
                command.Parameters.AddWithValue("@totalamt", Total.Text);

                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Product Details Updated Successfully..!!!");
                }
                else
                {
                    MessageBox.Show("Somthing Went Wrong..!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string search = "select * from ProductTbl WHERE pid=@pid";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@pid", Pid.Text);


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

        private void button2_Click(object sender, EventArgs e)
        {
            Pid.Clear();
            Pname.Clear();
            pqty.Clear();
            Pprice.Clear();
            Total.Clear();
            Ptype.Items.Clear();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }
    }
}
