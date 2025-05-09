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
    public partial class Material : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int mid = 0;
        public Material()
        {
            InitializeComponent();
            showdata();
        }
        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from Material";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }

        private void GenerateAutoID()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(mid)from Material", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            MaterialId.Text = mid + i.ToString();
        }

        private void label9_Click(object sender, EventArgs e)
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

        private void label5_Click(object sender, EventArgs e)
        {
            Vendor vendor = new Vendor();
            vendor.Show();
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

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                // Vendor Contact Number Validation
                string vendorContactNo = VendorCno.Text.Trim(); // Get the vendor contact number input

                // Check if the contact number has at least 10 digits
                if (vendorContactNo.Length < 10)
                {
                    MessageBox.Show("Vendor Contact Number must be at least 10 digits long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without proceeding to the SQL execution
                }

                // Vendor Name Validation: Check if the name contains any numbers
                string vendorName = VendorName.Text.Trim();
                if (vendorName.Any(char.IsDigit)) // Checks if there is any digit in the name
                {
                    MessageBox.Show("Vendor Name must not contain numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                string insert = "INSERT into Material VALUES(@mid , @Mname , @Mquantity , @Mprice , @Vid , @Vname , @Vcontactno)";

                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("@mid", MaterialId.Text);
                command.Parameters.AddWithValue("@Mname", MaterialName.Text);
                command.Parameters.AddWithValue("@Mquantity", MaterialQuantity.Text);
                command.Parameters.AddWithValue("@Mprice", Convert.ToDecimal(MaterialPrice.Text));
                command.Parameters.AddWithValue("@Vid", VendorId.Text);
                command.Parameters.AddWithValue("@Vname", vendorName); // Use the trimmed vendor name
                command.Parameters.AddWithValue("@Vcontactno", vendorContactNo); // Use the trimmed vendor contact number

                // Execute the insert command
                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Material Information Added Successfully..!!!");
                }
                else
                {
                    MessageBox.Show("Please try Again.");
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

        private void VendorId_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(VendorId.Text, out int vid) && VendorId.Text.Length > 0)
            {
                FetchProductNameById(vid);
            }
        }

        private void FetchProductNameById(object vid)
        {
            // throw new NotImplementedException();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string select = "SELECT Vname , Vcno, StockSupplies , Amt From Vendor WHERE Vid = @Vid";
            SqlCommand command = new SqlCommand(select, connection);
            command.Parameters.AddWithValue("@Vid", VendorId.Text);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                VendorName.Text = reader["Vname"].ToString();
                VendorCno.Text = reader["Vcno"].ToString();
                MaterialQuantity.Text = reader["StockSupplies"].ToString();
                MaterialPrice.Text = reader["Amt"].ToString();
            }
            else
            {
                VendorId.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                string update = "UPDATE Material SET mid=@mid , Mname=@Mname , Mquantity=@Mquantity , Mprice=@Mprice , Vid=@Vid , Vname=@Vname , Vcontactno=@Vcontactno WHERE mid=@mid";

                SqlCommand command = new SqlCommand(update, connection);

                command.Parameters.AddWithValue("@mid", MaterialId.Text);
                command.Parameters.AddWithValue("@Mname", MaterialName.Text);
                command.Parameters.AddWithValue("@Mquantity", MaterialQuantity.Text);
                command.Parameters.AddWithValue("@Mprice", Convert.ToDecimal(MaterialPrice.Text));
                command.Parameters.AddWithValue("@Vid", VendorId.Text);
                command.Parameters.AddWithValue("@Vname", VendorName.Text);
                command.Parameters.AddWithValue("@Vcontactno", VendorCno.Text);

                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    MessageBox.Show("Material Information Updated Successfully..!!!");

                }
                else
                {
                    MessageBox.Show("Please try Again ");
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

        private void Material_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string search = "select * from Material WHERE mid=@mid";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@mid", MaterialId.Text);


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
            //throw new NotImplementedException();
            MaterialId.Clear();
            MaterialName.Clear();
            MaterialQuantity.Clear();
            MaterialPrice.Clear();
            VendorId.Clear();
            VendorName.Clear();
            VendorCno.Clear();
        }
    }
}
