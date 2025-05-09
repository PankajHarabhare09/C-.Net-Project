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
    public partial class Order : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int Oid = 0;
        public Order()
        {
            InitializeComponent();
            showdata();
        }

        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from OrderTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }


        private void label10_Click(object sender, EventArgs e)
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
            sale.Show(); this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show(); this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show(); this.Hide();

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show(); this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                // Validate Customer Name - Check if it contains any numbers
                string customerName = CustomerNameTB.Text.Trim();
                if (customerName.Any(char.IsDigit)) // Checks if there is any digit in the name
                {
                    MessageBox.Show("Customer Name must not contain numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                // Validate Product Name - Check if it contains any numbers
                string productName = ProductNameTB.Text.Trim();
                if (productName.Any(char.IsDigit)) // Checks if there is any digit in the product name
                {
                    MessageBox.Show("Product Name must not contain numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                // Proceed with the insert if validations pass
                string insert = "INSERT INTO OrderTbl VALUES(@Oid , @Odate , @Cid , @Cname , @Pid , @Pname , @Pquantity , @Pamt)";
                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("@Oid", OrderIdTB.Text);
                command.Parameters.AddWithValue("@Odate", OrderDateTB.Value);
                command.Parameters.AddWithValue("@Cid", CustomerIdTB.Text);
                command.Parameters.AddWithValue("@Cname", customerName); // Use validated customer name
                command.Parameters.AddWithValue("@Pid", ProductIdTB.Text);
                command.Parameters.AddWithValue("@Pname", productName); // Use validated product name
                command.Parameters.AddWithValue("@Pquantity", ProductQuantityTB.Text);
                command.Parameters.AddWithValue("@Pamt", AmountTB.Text);

                // Execute the SQL insert command
                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Order Information Added successfully..!!!");
                }
                else
                {
                    MessageBox.Show("Please try again..!!!");
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

        private void Order_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }

        private void GenerateAutoID()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(Oid)from OrderTbl", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            OrderIdTB.Text = Oid + i.ToString();
        }

        private void CustomerIdTB_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(CustomerIdTB.Text, out int Cid) && CustomerIdTB.Text.Length > 0)
            {
                FetchCustomerName(Cid);


            }

        }

        private void FetchCustomerName(object Cid)
        {
            // throw new NotImplementedException();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string select = "select Cname FROM Customer WHERE Cid=@Cid";
            SqlCommand command = new SqlCommand(select, connection);
            command.Parameters.AddWithValue("@Cid", CustomerIdTB.Text);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                CustomerNameTB.Text = reader["Cname"].ToString();
            }
        }

        private void ProductIdTB_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ProductIdTB.Text, out int Pid) && ProductIdTB.Text.Length > 0)
            {
                FetchProductDetail(Pid);
            }
        }

        private void FetchProductDetail(int pid)
        {
            //  throw new NotImplementedException();

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            string select = "Select Pname , Pquantity , pprice FROM ProductTbl WHERE Pid=@Pid";
            SqlCommand command = new SqlCommand(select, connection);
            command.Parameters.AddWithValue("@Pid", ProductIdTB.Text);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                ProductNameTB.Text = reader["Pname"].ToString();
                ProductQuantityTB.Text = reader["Pquantity"].ToString();
                AmountTB.Text = reader["pprice"].ToString();
            }
            else
            {
                ProductIdTB.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                string update = "Update OrderTbl SET Oid=@Oid , Odate=@Odate , Cid=@Cid , Cname=@Cname , Pid=@Pid , Pname=@Pname ,Pquantity=@Pquantity , Pamt=@Pamt";

                SqlCommand command = new SqlCommand(update, connection);
                command.Parameters.AddWithValue("@Oid", OrderIdTB.Text);
                command.Parameters.AddWithValue("@Odate", OrderDateTB.Value);
                command.Parameters.AddWithValue("@Cid", CustomerIdTB.Text);
                command.Parameters.AddWithValue("@Cname ", CustomerNameTB.Text);
                command.Parameters.AddWithValue("@Pid ", ProductIdTB.Text);
                command.Parameters.AddWithValue("@Pname", ProductNameTB.Text);
                command.Parameters.AddWithValue("@Pquantity", ProductQuantityTB.Text);
                command.Parameters.AddWithValue("@Pamt", AmountTB.Text);

                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Order Information Updated successfully..!!!");
                }
                else
                {
                    MessageBox.Show("please try again..!!!");
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
            //SqlConnection connection = new SqlConnection(connectionstring);
            //connection.Open();

            //string search = "select * From OrderTbl WHERE Oid = @Oid";
            //SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            //adapter.SelectCommand.Parameters.AddWithValue("@Oid", OrderIdTB.Text);
            //// adapter.SelectCommand.Parameters.AddWithValue("@Odate", OrderDateTB.Text);


            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            //if (dt.Rows.Count > 0)
            //{
            //    dataGridView1.DataSource = dt;
            //}
            //else
            //{
            //    MessageBox.Show("No Record Found....!!!!!");
            //    dataGridView1.DataSource = null;
            //}
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string search = "select * From OrderTbl WHERE Oid = @Oid";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@Oid", OrderIdTB.Text);
            // adapter.SelectCommand.Parameters.AddWithValue("@Odate", OrderDateTB.Text);


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
            OrderIdTB.Clear();
            CustomerIdTB.Clear();
            CustomerNameTB.Clear();
            ProductIdTB.Clear();
            ProductNameTB.Clear();
            ProductQuantityTB.Clear();
            AmountTB.Clear();
        }
    }
}
