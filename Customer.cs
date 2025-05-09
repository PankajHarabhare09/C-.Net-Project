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
    public partial class Customer : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int Cid = 0;
        public Customer()
        {
            InitializeComponent();
            showdata();
        }
        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from Customer";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }

        void ClearAllTextBoxes()
        {
            CustomerId.Clear();
            CustomerName.Clear();
            ContactNo.Clear();
            ProductId.Clear();
            ProductName.Clear();
            ProductQuantity.Clear();
            ProductPrice.Clear();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                // Contact Number Validation
                string contactNo = ContactNo.Text.Trim(); // Get the contact number input

                // Check if the contact number has at least 10 digits
                if (contactNo.Length < 10)
                {
                    MessageBox.Show("Contact Number must be at least 10 digits long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without proceeding to the SQL execution
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(contactNo, @"^\d+$"))
                {
                    MessageBox.Show("Contact Number must contain only numeric digits, no alphabets or special characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                // Customer Name Validation: Check if the name contains any numbers
                string customerName = CustomerName.Text.Trim();
                if (customerName.Any(char.IsDigit)) // Checks if there is any digit in the name
                {
                    MessageBox.Show("Customer Name must not contain numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                // Proceed with the insert if the validations pass
                string insert = "insert into Customer VALUES(@Cid , @Cname , @Contactno , @Pid , @Pname , @Pcategory , @Pquantity , @Totalamt)";
                SqlCommand command = new SqlCommand(insert, connection);

                // Add parameters for the SQL query
                command.Parameters.AddWithValue("@Cid", CustomerId.Text);
                command.Parameters.AddWithValue("@Cname", customerName); // Use the trimmed customer name
                command.Parameters.AddWithValue("@Contactno", contactNo); // Use the trimmed contact number
                command.Parameters.AddWithValue("@Pid", ProductId.Text);
                command.Parameters.AddWithValue("@Pname", ProductName.Text);

                // Handle the product category based on which checkbox is selected
                if (Cake.Checked)
                {
                    command.Parameters.AddWithValue("@Pcategory", Cake.Text);
                }
                else if (Pastery.Checked)
                {
                    command.Parameters.AddWithValue("@Pcategory", Pastery.Text);
                }
                else if (Snack.Checked)
                {
                    command.Parameters.AddWithValue("@Pcategory", Snack.Text);
                }
                else if (Bread.Checked)
                {
                    command.Parameters.AddWithValue("@Pcategory", Bread.Text);
                }
                else
                {
                    MessageBox.Show("Please select a product category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit if no category is selected
                }

                // Add quantity and total amount
                command.Parameters.AddWithValue("@Pquantity", ProductQuantity.Text);
                command.Parameters.AddWithValue("@Totalamt", ProductPrice.Text);

                // Execute the insert command
                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Customer Information Added Successfully..!!!");
                }
                else
                {
                    MessageBox.Show("Please Try Again later..!!!");
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

        private void Customer_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }

        private void GenerateAutoID()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(Cid)from Customer", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            CustomerId.Text = Cid + i.ToString();
        }

        private void ProductId_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ProductId.Text, out int Pid) && ProductId.Text.Length > 0)
            {
                FetchProductNameById(Pid);
            }
        }

        private void FetchProductNameById(int pid)
        {
            Product product = new Product();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            string select = "Select Pname , Pcategory , Pquantity , pprice FROM ProductTbl WHERE Pid=@Pid";
            SqlCommand command = new SqlCommand(select, connection);
            command.Parameters.AddWithValue("@Pid", ProductId.Text);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                ProductName.Text = reader["Pname"].ToString();
                string productCategory = reader["Pcategory"].ToString(); // Retrieve the category

                // Reset all radio buttons to unchecked
                Cake.Checked = false;
                Pastery.Checked = false;
                Snack.Checked = false;
                Bread.Checked = false;

                // Automatically check the corresponding radio button based on productCategory
                switch (productCategory)
                {
                    case "Cake":
                        Cake.Checked = true;
                        break;
                    case "Pastery":
                        Pastery.Checked = true;
                        break;
                    case "Snack":
                        Snack.Checked = true;
                        break;
                    case "Bread":
                        Bread.Checked = true;
                        break;
                    default:
                        MessageBox.Show("Unknown category: " + productCategory);
                        break;
                }

                ProductQuantity.Text = reader["Pquantity"].ToString();
                ProductPrice.Text = reader["pprice"].ToString();
            }
            else
            {
                ProductId.Clear();
            }



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            try
            {
                string update = "UPDATE Customer SET Cid=@Cid , Cname=@Cname , Contactno=@Contactno , Pid=@Pid , Pname=@Pname , Pcategory=@Pcategory , Pquantity=@Pquantity , TOtalamt=@Totalamt WHERE Contactno=@Contactno";
                SqlCommand command = new SqlCommand(update, connection);
                command.Parameters.AddWithValue("@Cid", CustomerId.Text);
                command.Parameters.AddWithValue("@Cname", CustomerName.Text);
                command.Parameters.AddWithValue("@Contactno", ContactNo.Text);
                command.Parameters.AddWithValue("@Pid", ProductId.Text);
                command.Parameters.AddWithValue("@Pname", ProductName.Text);
                if (Cake.Checked == true)
                {
                    command.Parameters.AddWithValue("@Pcategory", Cake.Text);
                }
                else if (Pastery.Checked == true)
                {
                    command.Parameters.AddWithValue("@Pcategory", Pastery.Text);
                }
                else if (Snack.Checked == true)
                {
                    command.Parameters.AddWithValue("@Pcategory", Snack.Text);
                }
                else if (Bread.Checked == true)
                {
                    command.Parameters.AddWithValue("@Pcategory", Bread.Text);
                }
                else
                {
                    MessageBox.Show("Please Try Again..!!!");
                }
                command.Parameters.AddWithValue("@Pquantity", ProductQuantity.Text);
                command.Parameters.AddWithValue("@Totalamt", ProductPrice.Text);

                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    MessageBox.Show("Customer Information Updated SuccessFully..!!!");
                }
                else
                {
                    MessageBox.Show("Please Try Again later..!!!");
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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string search = "select * from Customer WHERE Cid=@Cid";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@Cid", CustomerId.Text);


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
            ClearAllTextBoxes();
        }
    }
}
