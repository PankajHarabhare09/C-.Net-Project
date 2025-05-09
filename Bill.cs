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
using System.Xml.Linq;

namespace HeavenTasteBakery
{
    public partial class Bill : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int Bid = 0;
        public Bill()
        {
            InitializeComponent();
            showdata();
            // dataGridView1.CellClick += DataGridView1_CellClick;
        }
        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from Bill";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }
        private void Bill_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }

        private void GenerateAutoID()
        {
            //throw new NotImplementedException();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(Bid)from Bill", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            BillIdTB.Text = Bid + i.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            sale.Show();
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

        private void CustomerIdTB_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(CustomerIdTB.Text, out int Cid) && CustomerIdTB.Text.Length > 0)
            {
                FetchCustomerName(Cid);
            }
        }

        private void FetchCustomerName(int cid)
        {
            //throw new NotImplementedException();

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

        private void OrderIdTB_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ProductIdTB.Text, out int Pid) && ProductIdTB.Text.Length > 0)
            {
                FetchProductDetail(Pid);
            }
        }

        private void FetchProductDetail(int pid)
        {
            // throw new NotImplementedException();

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            string select = "Select Pname , Pcategory , Pquantity , pprice , Totalamt from ProductTbl WHERE Pid=@Pid";
            SqlCommand command = new SqlCommand(select, connection);
            command.Parameters.AddWithValue("@Pid", ProductIdTB.Text);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                ProductNameTB.Text = reader["Pname"].ToString();
                ProductQuantityTB.Text = reader["Pquantity"].ToString();
                TotalAmountTB.Text = reader["Totalamt"].ToString();
                ProductCategoryTB.Text = reader["Pcategory"].ToString();
                ProductPriceTB.Text = reader["pprice"].ToString();
                TotalAmountTB.Text = reader["TOtalamt"].ToString();
            }
            else
            {
                ProductIdTB.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                string insert = "insert into Bill VALUES(@Bid , @Cid ,@Cname , @Dateofbill  , @Pname , @Pquantity , @Totalamt , @Pcategory , @Pprice)";

                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("@Bid", BillIdTB.Text);
                command.Parameters.AddWithValue("@Cid", CustomerIdTB.Text);
                command.Parameters.AddWithValue("@Cname ", Convert.ToString(CustomerNameTB.Text));
                command.Parameters.AddWithValue("@Dateofbill", DateOfBillTB.Text);
                //command.Parameters.AddWithValue("@Oid", ProductIdTB.Text);
                command.Parameters.AddWithValue("@Pname ", ProductNameTB.Text);
                command.Parameters.AddWithValue("@Pquantity", ProductQuantityTB.Text);
                command.Parameters.AddWithValue("@Totalamt", TotalAmountTB.Text);
                command.Parameters.AddWithValue("@Pcategory", ProductCategoryTB.Text);
                // command.Parameters.AddWithValue("@Pcategory", ProductCategoryTB.Text);

                //if (ProductCategoryTB.Text == "Cake")
                //{
                //    int Pprice = 250;
                //    ProductPriceTB.Text = Pprice.ToString();

                //}
                //else if (ProductCategoryTB.Text == "Pastery")
                //{
                //    int Pprice = 80;
                //    ProductPriceTB.Text = Pprice.ToString();
                //}
                //else if (ProductCategoryTB.Text == "Snack")
                //{
                //    int Pprice = 100;
                //    ProductPriceTB.Text = Pprice.ToString();
                //}
                //else
                //{
                //    int Pprice = 40;
                //    ProductPriceTB.Text = Pprice.ToString();
                //}
                command.Parameters.AddWithValue("@Pprice", ProductPriceTB.Text);

                int execute = command.ExecuteNonQuery();

                //printPreviewDialog1.Document = printDocument1;
                //printPreviewDialog1.Show();

                if (execute > 0)
                {
                    MessageBox.Show("Bill Information Added Successfully..!!");
                }
                else
                {
                    MessageBox.Show("Please Try Again..!!");
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
                string update = "Update Bill SET Bid=@Bid , Cid=@Cid ,Cname=@Cname , Dateofbill=@Dateofbill ,Oid= @Oid , Pname=@Pname , Pquantity=@Pquantity , TOtalamt=@Totalamt , Pcategory=@Pcategroy , Pprice=@Pprice  WHERE Bid=@Bid";

                SqlCommand command = new SqlCommand(update, connection);
                command.Parameters.AddWithValue("@Bid", BillIdTB.Text);
                command.Parameters.AddWithValue("@Cid", CustomerIdTB.Text);
                command.Parameters.AddWithValue("@Cname ", Convert.ToString(CustomerNameTB.Text));
                command.Parameters.AddWithValue("@Dateofbill", DateOfBillTB.Text);
                command.Parameters.AddWithValue("@Oid", ProductIdTB.Text);
                command.Parameters.AddWithValue("@Pname ", ProductNameTB.Text);
                command.Parameters.AddWithValue("@Pquantity", ProductQuantityTB.Text);
                command.Parameters.AddWithValue("@Totalamt", TotalAmountTB.Text);
                command.Parameters.AddWithValue("@Pcategory", ProductCategoryTB.Text);
                command.Parameters.AddWithValue("@Pprice", ProductPriceTB.Text);

                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Bill Information Updated Successfully..!!");
                }
                else
                {
                    MessageBox.Show("Please Try Again..!!");
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

            string search = "select * From Bill WHERE Bid = @Bid";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@Bid", BillIdTB.Text);
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
            BillIdTB.Clear();
            CustomerIdTB.Clear();
            CustomerNameTB.Clear();
            ProductIdTB.Clear();
            ProductNameTB.Clear();
            ProductQuantityTB.Clear();
            ProductPriceTB.Clear();
            //TotalAmountTB.Clear();
            //ProductCategoryTB.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                BillIdTB.Text = row.Cells["Bid"].Value.ToString();
                CustomerIdTB.Text = row.Cells["Cid"].Value.ToString();
                CustomerNameTB.Text = row.Cells["Cname"].Value.ToString();
                // DateOfBillTB.Text = row.Cells["Dateofbill"].ToString();
                //ProductIdTB.Text = row.Cells["Oid"].Value.ToString();
                ProductNameTB.Text = row.Cells["Pname"].Value.ToString();
                ProductQuantityTB.Text = row.Cells["Pquantity"].Value.ToString();
                ProductPriceTB.Text = row.Cells["Pprice"].Value.ToString();
                ProductCategoryTB.Text = row.Cells["Pcategory"].Value.ToString();
                TotalAmountTB.Text = row.Cells["Totalamt"].ToString();

            }
        }

       
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           // int Bid =0;
            int Cid = 0;
            String Cname = null;
            //DateTime date=;
            String Pname =null;
            int pqty = 0;
            decimal total =0;
            String pcategory =null;
            decimal price = 0;
            String Cname1=CustomerNameTB.Text;

            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Bill WHERE Cname=@Cname", connection);
            sqlCommand.Parameters.AddWithValue("@Cname", Cname1);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                int Bid = reader.GetInt32(0);
                Cid = reader.GetInt32(1);
                Cname = reader.GetString(2);
                DateTime date = reader.GetDateTime(3);
                Pname = reader.GetString(4);
                pqty = reader.GetInt32(5);
                total = (decimal)reader.GetDouble(6);
                pcategory = reader.GetString(7);
                price = (decimal)reader.GetDouble(8);




                // FIRST POINT FOR LEFT TO RIGHT POSITION EX. 100 OF HT BAKERY
                // SECOND POINT FOR UPPER TO LOWER POSITION EX. 10 OF HT BAKERY     

                //BAKERY NAME

                e.Graphics.DrawString("----- || HEAVEN TASTE BAKERY || -----", new Font("Comic Sans MS", 22, FontStyle.Bold), Brushes.LightCoral, new Point(100, 10));

                // INVOICE INFORMATTION

                e.Graphics.DrawString("Invoice Information", new Font("Comic Sans MS", 22, FontStyle.Bold), Brushes.Black, new Point(250, 80));
                e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Comic Sans MS", 16, FontStyle.Bold), Brushes.Black, new Point(20, 100));
                e.Graphics.DrawString("Date:- ", new Font("Comic Sans MS", 20, FontStyle.Bold), Brushes.Black, new Point(20, 135));
                e.Graphics.DrawString("Bill Id: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 185));

                //CUSTOMER INFORMATION

                e.Graphics.DrawString("Customer Information", new Font("Comic Sans MS", 22, FontStyle.Bold), Brushes.Black, new Point(250, 220));
                e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Comic Sans MS", 16, FontStyle.Bold), Brushes.Black, new Point(20, 240));
                e.Graphics.DrawString("Customer Id: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 270));
                e.Graphics.DrawString("Customer Name: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 310));

                // PRODUCT INFORMATION

                e.Graphics.DrawString("Product Information", new Font("Comic Sans MS", 22, FontStyle.Bold), Brushes.Black, new Point(250, 370));
                e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Comic Sans MS", 16, FontStyle.Bold), Brushes.Black, new Point(20, 400));
                //e.Graphics.DrawString("Product Id: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 430));
                e.Graphics.DrawString("Product Name: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 430));
                e.Graphics.DrawString("Product Quantity: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 505));
                e.Graphics.DrawString("Product Categroy: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 470));
                e.Graphics.DrawString("Product Price: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(20, 540));
                //e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Comic Sans MS", 16, FontStyle.Bold), Brushes.Black, new Point(10, 215));

                //TOTALAMOUNT 

                e.Graphics.DrawString("====================================", new Font("Comic Sans MS", 16, FontStyle.Bold), Brushes.Black, new Point(350, 580));
                e.Graphics.DrawString("Total Amount: ", new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(350, 610));

                e.Graphics.DrawString("****** THANK YOU ******", new Font("Comic Sans MS", 22, FontStyle.Bold), Brushes.Black, new Point(220, 850));

                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //if (row.IsNewRow) continue;
                //{
                //    string Cname = CustomerNameTB.Text;
                //    string select = " select * from Bill WHERE Cname=" + Cname;
                //    int Bid = Convert.ToInt16(row.Cells["Bid"].Value);
                //    int Cid = Convert.ToInt16(row.Cells["Cid"].Value);
                //    Cname = Convert.ToString(row.Cells["Cname"].Value);
                //    //int Pid = Convert.ToInt16(row.Cells["Pid"].Value);
                //    string Pname = Convert.ToString(row.Cells["Pname"].Value);
                //    string Pcategory = Convert.ToString(row.Cells["Pcategory"].Value);
                //    int quantity = Convert.ToInt16(row.Cells["Pquantity"].Value);
                //    decimal Amount = Convert.ToDecimal(row.Cells["Totalamt"].Value);
                //    DateTime date = Convert.ToDateTime(row.Cells["Dateofbill"].Value);
                //    decimal Pprice = Convert.ToDecimal(row.Cells["Pprice"].Value);



                //BILL 
                e.Graphics.DrawString("" + Bid, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(120, 185));
                e.Graphics.DrawString("" + date, new Font("Comic Sans MS", 18, FontStyle.Bold), Brushes.Black, new Point(120, 135));

                //CUSTOMER
                e.Graphics.DrawString("" + Cid, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(195, 270));
                e.Graphics.DrawString("" + Cname, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(220, 313));
                //e.Graphics.DrawString("" + Cid, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(195, 270));

                //PRODUCT
                e.Graphics.DrawString("" + pcategory, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(240, 470));
                e.Graphics.DrawString("" + Pname, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(220, 430));
                e.Graphics.DrawString("" + pqty, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(260, 510));
                e.Graphics.DrawString("" + price, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(205, 545));


                e.Graphics.DrawString("" + total, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(545, 610));
            }

            //    }
            //}
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Show();
        }

        private void ProductNameTB_TextChanged(object sender, EventArgs e)
        {
            //if(int.TryParse(ProductNameTB.Text, out int Pname))
            //{
            //    FetchProductDetail(Pname);
            //}

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the clicked row index is valid (not a header row)
            if (e.RowIndex >= 0)
            {
                // Retrieve the current row
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Display data in TextBoxes
                BillIdTB.Text = row.Cells["Bid"].Value.ToString();
                CustomerIdTB.Text = row.Cells["Cid"].Value.ToString();
                CustomerNameTB.Text = row.Cells["Cname"].Value.ToString();
                DateOfBillTB.Text = row.Cells["Dateofbill"].Value.ToString();
             //   ProductIdTB.Text = row.Cells["PId"].Value.ToString();
                ProductNameTB.Text = row.Cells["Pname"].Value.ToString();
                ProductQuantityTB.Text = row.Cells["pquantity"].Value.ToString();
                TotalAmountTB.Text = row.Cells["Totalamt"].Value.ToString();
                ProductCategoryTB.Text = row.Cells["Pcategory"].Value.ToString();
                ProductPriceTB.Text = row.Cells["Pprice"].Value.ToString();
            }

            //private void FetchProductDetail(int pname)
            //{
            //    //throw new NotImplementedException();
            //    SqlConnection connection = new SqlConnection(connectionstring);
            //    connection.Open();
            //    string select = "Select Pcategory  FROM Customer WHERE Cid=@Cid";
            //    SqlCommand command = new SqlCommand(select, connection);
            //    command.Parameters.AddWithValue("@Cid", ProductNameTB.Text);

            //    SqlDataReader reader = command.ExecuteReader();
            //    if (reader.Read())
            //    {
            //       ProductCategoryTB.Text = reader["Pcategory"].ToString();
            //    }
            //    else
            //    {
            //        ProductNameTB.Clear();
            //    }
            //}
        }
    }
}
