using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace HeavenTasteBakery
{
    public partial class Sale : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int SaleId = 0;
        string pname;
        string pcat;


        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from Sale";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];


        }
        private void GenerateAutoID()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(Sid)from Sale", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            Sid.Text = SaleId + i.ToString();
        }
        public Sale()
        {
            InitializeComponent();
            showdata();
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            showdata();
            Product product = new Product();
            Sale sale = new Sale();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                string insert = "insert into Sale VALUES(@Sid , @Pid , @Pname , @Pcategory , @Totalqty ,  @Totalsale , @Remainingproduct)";
                SqlCommand command = new SqlCommand(insert, connection);


                command.Parameters.AddWithValue("@Sid", Sid.Text);
                command.Parameters.AddWithValue("@Pid", ProductId.Text);
                command.Parameters.AddWithValue("@Pname", ProductName.Text);
                command.Parameters.AddWithValue("@Pcategory", ProductCategory.Text);
                command.Parameters.AddWithValue("@Totalqty", TotalQuantity.Text);
                command.Parameters.AddWithValue("@Totalsale", Totalsale.Text);

                int totalq = Convert.ToInt32(TotalQuantity.Text);
                int totals = Convert.ToInt32(Totalsale.Text);

                int remaining = totalq - totals;
                RemainingProduct.Text = remaining.ToString();

                command.Parameters.AddWithValue("@Remainingproduct", RemainingProduct.Text);

                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    MessageBox.Show("Sale Information Added Successfully..!!!");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            string select = "Select Pname , Pcategory FROM ProductTbl WHERE Pid=@Pid";
            SqlCommand command = new SqlCommand(select, connection);
            command.Parameters.AddWithValue("@Pid", ProductId.Text);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                ProductName.Text = reader["Pname"].ToString();
                ProductCategory.Text = reader["Pcategory"].ToString();
            }
            else
            {
                ProductId.Clear();
            }



        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            showdata();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                string update = "UPDATE Sale SET Sid=@Sid , Pid=@Pid , Pname=@Pname , Pcategory=@Pcategory , Totalqty=@Totalqty ,  Totalsale=@Totalsale , Remainingproduct=@Remainingproduct WHERE Sid=@Sid";

                SqlCommand command = new SqlCommand(update, connection);
                command.Parameters.AddWithValue("@Sid", Sid.Text);
                command.Parameters.AddWithValue("@Pid", ProductId.Text);
                command.Parameters.AddWithValue("@Pname", ProductName.Text);
                command.Parameters.AddWithValue("@Pcategory", ProductCategory.Text);
                command.Parameters.AddWithValue("@Totalqty", TotalQuantity.Text);
                command.Parameters.AddWithValue("@Totalsale", Totalsale.Text);

                int totalq = Convert.ToInt32(TotalQuantity.Text);
                int totals = Convert.ToInt32(Totalsale.Text);

                int remaining = totalq - totals;
                RemainingProduct.Text = remaining.ToString();

                command.Parameters.AddWithValue("@Remainingproduct", RemainingProduct.Text);

                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    MessageBox.Show("Sale Information Updated Successfully..!!!");
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

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string search = "select * from Sale WHERE Sid=@Sid";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@Sid", Sid.Text);


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
            Sid.Clear();
            ProductId.Clear();
            ProductName.Clear();
            ProductCategory.Items.Clear();
            TotalQuantity.Clear();
            Totalsale.Clear();
            RemainingProduct.Clear();

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

        private void label6_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Dashboard dashboard1 = new Dashboard();
            dashboard1.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Material material = new Material();
            material.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Vendor vendor = new Vendor();
            vendor.Show(); this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show(); this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show(); this.Hide();
        }
    }
}