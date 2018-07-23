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
    public partial class Cutomer_DisApprove : Form
    {
        Connection_DB conn = new Connection_DB();
        public Cutomer_DisApprove()
        {
            InitializeComponent();
        }

        private void Cutomer_DisApprove_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Customer Approval Form";
            this.label2.Text = "Customer ID:";
            this.label3.Text = "Customer Name:";
            this.label4.Text = "Customer Address:";
            this.label5.Text = "Customer City:";
            this.label6.Text = "Phone No#";
            this.label7.Text = "Credit Limit:";
            this.label8.Text = "Group";

            this.button1.Text = "Now Approve";

            //Populate City ComboBox2.......................
            string[] City = { "Islamabad", "Lahore", "Karachi", "Peshawar", "Multan" };
            this.comboBox2.Items.AddRange(City);

            //Populate Vendor Group Combobox3................................
            string[] CGroup = { "HR", "Sales", "Consumer", "Marketing" };
            this.comboBox3.Items.AddRange(CGroup);

            //Populate comboBox1 for edit approval operation.................
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("select cid from customer where cstatus='disapprove'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["cid"].ToString());
            }
            conn.oleDbConnection1.Close();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* {
                //For editing Vendor...........................................
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("insert into customer (cid,cname,caddress,city,ph1,creditlimit,cgroup) values(@cid,@cname,@caddress,@city,@ph1,@creditlimit,@cgroup)", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@cid", this.comboBox1.Text);
                cmd.Parameters.AddWithValue("@cname", this.textBox1.Text);
                cmd.Parameters.AddWithValue("@caddress", this.textBox2.Text);
                cmd.Parameters.AddWithValue("@city", this.comboBox2.Text);
                cmd.Parameters.AddWithValue("@ph1", this.textBox3.Text);
                cmd.Parameters.AddWithValue("@creditlimit", this.textBox4.Text);
                cmd.Parameters.AddWithValue("@cgroup", this.comboBox3.Text);
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();
            }*/
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Update customer set cstatus='Approved' where cid ='" + comboBox1.Text + "'", conn.oleDbConnection1);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Has Been Approved");
                conn.oleDbConnection1.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from customer where cid='" + comboBox1.Text + "'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.textBox1.Text = dr["cname"].ToString();
                this.textBox2.Text = dr["caddress"].ToString();
                this.comboBox2.Text = dr["city"].ToString();
                this.textBox3.Text = dr["ph1"].ToString();
                this.textBox4.Text = dr["creditlimit"].ToString();
                this.comboBox3.Text = dr["cgroup"].ToString();
            }
            conn.oleDbConnection1.Close();
        }
    }
}
