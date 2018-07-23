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
    public partial class Customer_Approval : Form
    {
        Connection_DB conn = new Connection_DB();
        public Customer_Approval()
        {
            InitializeComponent();
        }

        private void Customer_Approval_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Customer Approval Form";
            this.label2.Text = "Customer ID:";
            this.label3.Text = "Customer Name:";
            this.label4.Text = "Customer Address:";
            this.label5.Text = "Customer City:";
            this.label6.Text = "Phone No#";
            this.label7.Text = "Credit Limit:";
            this.label8.Text = "Group";

            this.button1.Text = "Approve";

            //Populate City ComboBox1.......................
            string[] City = { "Islamabad", "Lahore", "Karachi", "Peshawar", "Multan" };
            this.comboBox1.Items.AddRange(City);

            //Populate Vendor Group Combobox2
            string[] CGroup = { "HR", "Sales", "Consumer", "Marketing" };
            this.comboBox2.Items.AddRange(CGroup);

            //Populate comboBox3 for Approval operation.................
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("select cid from customer where cstatus='pending'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["cid"].ToString());
            }
            conn.oleDbConnection1.Close();
        }

        //To fetch Info Of required ID................................................
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from customer where cid='" + comboBox3.Text + "'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.textBox6.Text = dr["cname"].ToString();
                this.textBox2.Text = dr["caddress"].ToString();
                this.comboBox1.Text = dr["city"].ToString();
                this.textBox3.Text = dr["ph1"].ToString();
                this.textBox4.Text = dr["creditlimit"].ToString();
                this.comboBox2.Text = dr["cgroup"].ToString();
            }
            conn.oleDbConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Update customer set cstatus='Approved' where cid ='" + comboBox3.Text + "'", conn.oleDbConnection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Customer Has Been Approved");
            conn.oleDbConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Update customer set cstatus='DisApprove' where cid ='" + comboBox3.Text + "'", conn.oleDbConnection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Customer Has not been Approved");
            MessageBox.Show("Deal With Another Customer");
            conn.oleDbConnection1.Close();

           /* Cutomer_DisApprove cd = new Cutomer_DisApprove();
            cd.Show();
            this.Hide();*/

            Customer_Form cf = new Customer_Form();
            cf.Show();
            this.Hide();

        }
    }
}
