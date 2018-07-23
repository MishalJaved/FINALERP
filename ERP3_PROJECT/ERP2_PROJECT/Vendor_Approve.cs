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
    public partial class Vendor_Approve : Form
    {
        Connection_DB conn = new Connection_DB();
        public Vendor_Approve()
        {
            InitializeComponent();
        }

        private void Vendor_Approve_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Vendor Approval Form";
            this.label2.Text = "Vendor ID:";
            this.label3.Text = "Vendor Name:";
            this.label4.Text = "Vendor Code:";
            this.label5.Text = "Vendr City:";
            this.label6.Text = "Phone No#";
            this.label7.Text = "Vendor Address";
            this.label8.Text = "Company Name:";
            this.label9.Text = "Vendor Group:";
            this.label10.Text = "Vendor Status:";

           

            this.button1.Text = "Approve";
            this.button2.Text = "DisApprove";

            //Populate comboBox1 for Approval operation.................
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("select vid from vendor where vstatus='pending'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["vid"].ToString());
            }
           conn.oleDbConnection1.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from vendor where vid='" + comboBox1.Text+ "'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["vname"].ToString();
                textBox2.Text = dr["vcode"].ToString();
                textBox3.Text = dr["vcity"].ToString();
                textBox4.Text = dr["ph1"].ToString();
                textBox5.Text = dr["vaddress"].ToString();
                textBox6.Text = dr["cpname"].ToString();
                textBox7.Text = dr["vgroup"].ToString();
                textBox8.Text = dr["vstatus"].ToString();
            }
            conn.oleDbConnection1.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Update vendor set vstatus='Active' where vid ='"+comboBox1.Text+"'", conn.oleDbConnection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Vendor Has Been Approved");
            conn.oleDbConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Update vendor set vstatus='DisApprove' where vid ='" + comboBox1.Text + "'", conn.oleDbConnection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Vendor Has not been Approved");
            conn.oleDbConnection1.Close();

            Vendor_DisApprove vda = new Vendor_DisApprove();
            vda.Show();
            this.Hide();
           
         
        }
    }
}
