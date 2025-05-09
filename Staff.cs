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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HeavenTasteBakery
{
    public partial class Staff : Form
    {
        string connectionstring = "Data Source=LAPTOP-G59715HB\\SQLEXPRESS;Initial Catalog=HeavenTasteBakery;Integrated Security=True";
        int sid = 0;


        void showdata()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string query = "select * from Staff";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            var dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

            connection.Close();
        }
        public Staff()
        {
            InitializeComponent();
            showdata();
        }

        private void GenerateAutoID()
        {
            //throw new NotImplementedException();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Count(Sid)from Staff", connection);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            i++;
            StaffId.Text = sid + i.ToString();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Vendor vendor = new Vendor();
            vendor.Show();
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Material material = new Material();
            material.Show();
            this.Close();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            sale.Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show();
            this.Close();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                // Staff Name Validation: Check if the name contains any numbers
                string staffName = StaffName.Text.Trim();
                if (staffName.Any(char.IsDigit)) // Checks if there is any digit in the name
                {
                    MessageBox.Show("Staff Name must not contain numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

              

                // Contact Number Validation
                string staffContactNo = StaffContactno.Text.Trim(); // Get the staff contact number input

                // Check if the contact number has at least 10 digits
                if (staffContactNo.Length < 10)
                {
                    MessageBox.Show("Staff Contact Number must be at least 10 digits long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without proceeding to the SQL execution
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(staffContactNo, @"^\d+$"))
                {
                    MessageBox.Show("Staff Contact Number must contain only numeric digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if validation fails
                }

                // Prepare and execute the insert statement
                string insert = "INSERT into Staff VALUES(@Sid , @Sname , @Scno , @Srole , @Sworkdays , @Ssalary)";
                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("@Sid", StaffId.Text);
                command.Parameters.AddWithValue("@Sname", staffName); // Use the validated staff name
                command.Parameters.AddWithValue("@Scno", staffContactNo); // Use the trimmed staff contact number
                command.Parameters.AddWithValue("@Srole", StaffRole.Text);
                command.Parameters.AddWithValue("@Sworkdays", StaffWorkDays.Text);

                // Calculate the salary based on role
                int perdaySalary;
                int workDays = Convert.ToInt32(StaffWorkDays.Text); // Workdays should be a number

                if (StaffRole.Text == "Bakers")
                {
                    perdaySalary = 200;
                }
                else if (StaffRole.Text == "Bakery Clerks")
                {
                    perdaySalary = 250;
                }
                else
                {
                    perdaySalary = 300;
                }

                int totalSalary = workDays * perdaySalary;
                StaffSalary.Text = totalSalary.ToString();

                command.Parameters.AddWithValue("@Ssalary", StaffSalary.Text);

                // Execute the insert command
                int execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    MessageBox.Show("Staff Data Added Successfully..!");
                }
                else
                {
                    MessageBox.Show("Please Try Again..!");
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
            //showdata();
            //SqlConnection connection = new SqlConnection(connectionstring);
            //connection.Open();

            //try
            //{
            //    string update = "update Staff SET Sid=@Sid , Sname=@Sname , Scno=@Scno , Srole=@Srole , Sworkdays=@Sworkdays , Ssalary=@Ssalary WHERE Sname=@Sname";

            //    SqlCommand command = new SqlCommand(update, connection);
            //    command.Parameters.AddWithValue("@Sid", StaffId.Text);
            //    command.Parameters.AddWithValue("@Sname", StaffName.Text);
            //    command.Parameters.AddWithValue("@Scno", StaffContactno.Text);
            //    command.Parameters.AddWithValue("@Srole", StaffRole.Text);
            //    command.Parameters.AddWithValue("@Sworkdays", StaffWorkDays.Text);

            //    if (StaffRole.Text == "Bakers")
            //    {
            //        int perdaylsalary = 200;
            //        int wd = Convert.ToInt32(StaffWorkDays.Text);
            //        int total;
            //        total = wd * perdaylsalary;

            //        StaffSalary.Text = total.ToString();
            //    }
            //    else if (StaffRole.Text == "Bakery Clerks")
            //    {
            //        int perdaylsalary = 250;
            //        int wd = Convert.ToInt32(StaffWorkDays.Text);
            //        int total;
            //        total = wd * perdaylsalary;

            //        StaffSalary.Text = total.ToString();
            //    }
            //    else
            //    {
            //        int perdaylsalary = 300;
            //        int wd = Convert.ToInt32(StaffWorkDays.Text);
            //        int total;
            //        total = wd * perdaylsalary;

            //        StaffSalary.Text = total.ToString();
            //    }
            //    command.Parameters.AddWithValue("@Ssalary", StaffSalary.Text);

            //    int execute = command.ExecuteNonQuery();
            //    if (execute > 0)
            //    {
            //        MessageBox.Show("Staff Data Updated Successfully..!");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please Try Again..!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    connection.Close();
            //    showdata();
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            showdata();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            try
            {
                string update = "update Staff SET Sid=@Sid , Sname=@Sname , Scno=@Scno , Srole=@Srole , Sworkdays=@Sworkdays , Ssalary=@Ssalary WHERE Sname=@Sname";

                SqlCommand command = new SqlCommand(update, connection);
                command.Parameters.AddWithValue("@Sid", StaffId.Text);
                command.Parameters.AddWithValue("@Sname", StaffName.Text);
                command.Parameters.AddWithValue("@Scno", StaffContactno.Text);
                command.Parameters.AddWithValue("@Srole", StaffRole.Text);
                command.Parameters.AddWithValue("@Sworkdays", StaffWorkDays.Text);

                if (StaffRole.Text == "Bakers")
                {
                    int perdaylsalary = 200;
                    int wd = Convert.ToInt32(StaffWorkDays.Text);
                    int total;
                    total = wd * perdaylsalary;

                    StaffSalary.Text = total.ToString();
                }
                else if (StaffRole.Text == "Bakery Clerks")
                {
                    int perdaylsalary = 250;
                    int wd = Convert.ToInt32(StaffWorkDays.Text);
                    int total;
                    total = wd * perdaylsalary;

                    StaffSalary.Text = total.ToString();
                }
                else
                {
                    int perdaylsalary = 300;
                    int wd = Convert.ToInt32(StaffWorkDays.Text);
                    int total;
                    total = wd * perdaylsalary;

                    StaffSalary.Text = total.ToString();
                }
                command.Parameters.AddWithValue("@Ssalary", StaffSalary.Text);

                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    MessageBox.Show("Staff Data Updated Successfully..!");
                }
                else
                {
                    MessageBox.Show("Please Try Again..!");
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

            string search = "select * from Staff WHERE Scno=@Scno";
            SqlDataAdapter adapter = new SqlDataAdapter(search, connection);

            adapter.SelectCommand.Parameters.AddWithValue("@Scno", StaffContactno.Text);


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

            StaffId.Clear();
            StaffName.Clear();
            StaffContactno.Clear();
            StaffRole.Items.Clear();
            StaffSalary.Clear();
            StaffWorkDays.Clear();
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            GenerateAutoID();
        }

        private void Staff_Load_1(object sender, EventArgs e)
        {
            GenerateAutoID();
        }
    }
}
