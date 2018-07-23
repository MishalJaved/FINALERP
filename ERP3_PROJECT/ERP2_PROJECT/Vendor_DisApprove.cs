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
    public partial class Vendor_DisApprove : Form
    {
        Connection_DB conn = new Connection_DB();
        public Vendor_DisApprove()
        {
            InitializeComponent();
        }

        private void Vendor_DisApprove_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Vendor Edition Form";
            this.label2.Text = "Vendor ID:";
            this.label3.Text = "Vendor Name:";
            this.label4.Text = "Vendor Code:";
            this.label5.Text = "Vendr City:";
            this.label6.Text = "Phone No#";
            this.label7.Text = "Vendor Address";
            this.label8.Text = "Company Name:";
            this.label9.Text = "Vendor Group:";

            this.button1.Text = "Now plz Approve";
            
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select vid from vendor where vstatus='disapprove'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["vid"].ToString());
                }
                conn.oleDbConnection1.Close();
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Update vendor set vstatus='Active' ,vname=@vname ,vcity=@vcity where vid ='" + comboBox1.Text + "'", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@vname", this.textBox1.Text);
                cmd.Parameters.AddWithValue("@vcity", this.textBox3.Text);

                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();

                MessageBox.Show("Vendor Has Been Approved");
            }

           

           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from vendor where vid='" + comboBox1.Text + "'", conn.oleDbConnection1);
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
                
            }
            conn.oleDbConnection1.Close();

        }
    }
}
