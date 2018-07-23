using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace ERP2_PROJECT
{
    public partial class Customer_Form : Form
    {
        Connection_DB conn = new Connection_DB();
        public Customer_Form()
        {
            InitializeComponent();
        }

        private void Customer_Form_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Customer Form";
            this.label2.Text = "Customer ID:";
            this.label3.Text = "Customer Name:";
            this.label4.Text = "Customer Address:";
            this.label5.Text = "Customer City:";
            this.label6.Text = "Phone No#";
            this.label7.Text = "Credit Limit:";
            this.label8.Text = "Group";
            this.label9.Text = "Customer Slip:";
            this.label10.Text = "View All Data:";

            this.button1.Text = "Insert Customer";
            this.button2.Text = "Send for Approval";
            this.button3.Text = "Customer Table";
            this.button4.Text = "Main Menu";
            this.button5.Text = "Clear";
            this.textBox5.ScrollBars = ScrollBars.Both;

            //Populate CB1 by Customer ID Auto Generate..............................

            {
                int c = 0;
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(cid) from customer", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]); c++;

                    {
                        this.textBox1.Text = "Cust-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                    }
                    conn.oleDbConnection1.Close();
                }
            }

             //Populate City ComboBox2.......................
            string[] City = { "Islamabad", "Lahore", "Karachi", "Peshawar", "Multan" };
            this.comboBox1.Items.AddRange(City);

            //Populate Vendor Group Combobox2
            string[] CGroup = { "HR", "Sales", "Consumer","Marketing" };
            this.comboBox2.Items.AddRange(CGroup);
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        //INsert Customer Button.................................
        private void button1_Click(object sender, EventArgs e)
        {
            //For inserting Vendor...........................................
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("insert into customer (cid,cname,caddress,city,ph1,creditlimit,cgroup,cstatus) values(@cid,@cname,@caddress,@city,@ph1,@creditlimit,@cgroup,@cstatus)", conn.oleDbConnection1);
            cmd.Parameters.AddWithValue("@cid",this.textBox1.Text);
             cmd.Parameters.AddWithValue("@cname",this.textBox6.Text);
             cmd.Parameters.AddWithValue("@caddress",this.textBox2.Text);
             cmd.Parameters.AddWithValue("@city",this.comboBox1.Text);
             cmd.Parameters.AddWithValue("@ph1",this.textBox3.Text);
             cmd.Parameters.AddWithValue("@creditlimit",this.textBox4.Text);
             cmd.Parameters.AddWithValue("@cgroup",this.comboBox2.Text);
             cmd.Parameters.AddWithValue("@cstatus=", "pending");
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Inserted Customer Table");
            conn.oleDbConnection1.Close();

            //Showing Inserted Data in List Box..............................
            this.textBox5.Text += "      CUSTOMER SLIP" + Environment.NewLine + Environment.NewLine;
            this.textBox5.Text += "Customer ID       :" + textBox1.Text + Environment.NewLine;
            this.textBox5.Text += "Customer Name     :" + textBox6.Text + Environment.NewLine;
            this.textBox5.Text += "Cusomer Address   :" + textBox2.Text + Environment.NewLine;
            this.textBox5.Text += "City              :" + comboBox1.Text + Environment.NewLine;
            this.textBox5.Text += "Phone No#         :" + textBox3.Text + Environment.NewLine;
            this.textBox5.Text += "Credit Limit      :" + textBox4.Text + Environment.NewLine;
            this.textBox5.Text += "Group             :" + comboBox2.Text + Environment.NewLine;
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = " ";
            this.textBox2.Text = " ";
            this.textBox3.Text = " ";
            this.textBox4.Text = " ";
            this.textBox5.Text = " ";
            this.textBox6.Text = " ";

            this.comboBox1.Text = " ";
            this.comboBox2.Text = " ";
        }

        //View All Data Of Customer.............................
        private void button3_Click(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from customer", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            conn.oleDbConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer_Approval ca = new Customer_Approval();
            ca.Show();
            this.Hide();
        }
    }
}
