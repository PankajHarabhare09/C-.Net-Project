using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavenTasteBakery
{
    public partial class Vendor : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int vid = 0;
        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from Vendor";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }
        public Vendor()
        {
            InitializeComponent();
        }

        private void Vendor_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }
        private void GenerateAutoID()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(Vid)from Vendor", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            VendorId.Text = vid + i.ToString();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
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

        private void label4_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
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
                string contactNo = Vcno.Text.Trim(); // Get the contact number input

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
                string vendorName = Vname.Text.Trim();
                if (vendorName.Any(char.IsDigit)) // Checks if there is any digit in the name
                {
                    MessageBox.Show("Vendor Name must not contain numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                // Show data (if necessary before proceeding)
                showdata();

                // Proceed with the insert if the validations pass
                string insert = "INSERT into Vendor VALUES(@Vid , @Vname , @Vcno , @StockSupplies , @Amt)";
                SqlCommand command = new SqlCommand(insert, connection);

                // Add parameters for the SQL query
                command.Parameters.AddWithValue("@Vid", VendorId.Text);
                command.Parameters.AddWithValue("@Vname", vendorName); // Use the trimmed vendor name
                command.Parameters.AddWithValue("@Vcno", contactNo); // Use the trimmed contact number
                command.Parameters.AddWithValue("@StockSupplies", Stock.Text);
                command.Parameters.AddWithValue("@Amt", Amt.Text);

                // Execute the insert command
                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Vendor Information Added..!!!");
                }
                else
                {
                    MessageBox.Show("Please Try Again..!!!");
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
                string update = "UPDATE Vendor SET Vid=@Vid , Vname=@Vname , Vcno=@Vcno , StockSupplies=@StockSupplies , Amt=@Amt WHERE Vid=@Vid";

                SqlCommand command = new SqlCommand(update, connection);
                //int Vid = 1;
                //Vid = Vid + 1;
                //VendorId.Text= Vid.ToString();
                command.Parameters.AddWithValue("@Vid", VendorId.Text);
                command.Parameters.AddWithValue("@Vname", Vname.Text);
                command.Parameters.AddWithValue("@Vcno", Vcno.Text);
                command.Parameters.AddWithValue("@StockSupplies", Stock.Text);
                command.Parameters.AddWithValue("@Amt", Amt.Text);

                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Vendor Information Updated..!!!");
                }
                else
                {
                    MessageBox.Show("Please Try Again..!!!");
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

            string search = "select * from Vendor WHERE Vcno=@Vcno";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@Vcno", Vcno.Text);


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

        private void ClearAllTextBoxes()
        {
           // throw new NotImplementedException();

            VendorId.Clear();
            Vname.Clear();
            Vcno.Clear();
            Stock.Clear();
            Amt.Clear();    
        }
    }
}