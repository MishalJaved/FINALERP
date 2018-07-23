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
    public partial class Vendor_Insertion : Form
    {
        Connection_DB conn = new Connection_DB();
        public Vendor_Insertion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Vendor_Insertion_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Vendor Insertion Form";
            this.label2.Text = "Vendor ID:";
            this.label3.Text = "Vendor Name:";
            this.label4.Text = "Vendor Code:";
            this.label5.Text = "Vendr City:";
            this.label6.Text = "Phone No#";
            this.label7.Text = "Vendor Address";
            this.label8.Text = "Company Name:";
            this.label9.Text = "Vendor Group:";
            this.label11.Text = "For Making Approval Request  >>";
            this.label12.Text = "Inserted Information Of Vendor:";

            this.textBox8.ScrollBars = ScrollBars.Both;
            this.textBox8.ReadOnly = true;

            this.button1.Text = "Insert Vendor";
            this.button2.Text = "Approval";
            this.button4.Text = "Back";

            this.textBox1.ReadOnly = true;

            //Auto Vnedor ID Geenration..............................
            {
                int c = 0;
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(vid) from vendor", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]); c++;

                    {
                        this.textBox1.Text = "Ven-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                    }
                    conn.oleDbConnection1.Close();
                }
            }


            //Populate City ComboBox1.......................
            string[] VCity = { "Islamabad", "Lahore", "Karachi", "Peshawar", "Multan" };
            this.comboBox1.Items.AddRange(VCity);

            //Populate Vendor Group Combobox2
            string[] VGroup = { "HR", "Sales", "Consumer" };
            this.comboBox2.Items.AddRange(VGroup);
        }

        //Vendor Insertion Operation.........................................
        private void button1_Click_1(object sender, EventArgs e)
        {
            //For inserting Vendor...........................................
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("insert into vendor (vid,vname,vcode,vcity,ph1,vaddress,cpname,vgroup,vstatus) values(@vid,@vname,@vcode,@vcity,@ph1,@vaddress,@cpname,@vgroup,@vstatus)", conn.oleDbConnection1);
            cmd.Parameters.AddWithValue("@vid", textBox1.Text);
            cmd.Parameters.AddWithValue("@vname", textBox2.Text);
            cmd.Parameters.AddWithValue("@vcode",textBox3.Text);
            cmd.Parameters.AddWithValue("@vcity",comboBox1.Text);
            cmd.Parameters.AddWithValue("@ph1",textBox4.Text);
            cmd.Parameters.AddWithValue("@vaddress",textBox5.Text);
            cmd.Parameters.AddWithValue("@cpname", textBox6.Text);
            cmd.Parameters.AddWithValue("@vgroup", comboBox2.Text);
            cmd.Parameters.AddWithValue("@vstatus", "Pending");
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Inserted");
            conn.oleDbConnection1.Close();

            //Showing Inserted Data in List Box..............................
            this.textBox8.Text += "Vendor ID    :" + textBox1.Text + Environment.NewLine;
            this.textBox8.Text += "Vendor Name  :" + textBox2.Text + Environment.NewLine;
            this.textBox8.Text += "Vendor Code  :" + textBox3.Text + Environment.NewLine;
            this.textBox8.Text += "Vendor City  :" + comboBox1.Text + Environment.NewLine;
            this.textBox8.Text += "Phone No#    :" + textBox4.Text + Environment.NewLine;
            this.textBox8.Text += "Vendor Address:" + textBox5.Text + Environment.NewLine;
            this.textBox8.Text += "Company Name :" + textBox6.Text + Environment.NewLine;
            this.textBox8.Text += "Vendor Group :" + comboBox2.Text + Environment.NewLine;
            this.textBox8.Text += "Vendor Status: Pending" + Environment.NewLine;

        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            Vendor_Approve va = new Vendor_Approve();
            va.Show();
            this.Hide();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
