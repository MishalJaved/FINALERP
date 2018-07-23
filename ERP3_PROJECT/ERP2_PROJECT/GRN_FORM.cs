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
    public partial class GRN_FORM : Form
    {
        Connection_DB conn = new Connection_DB();
        public GRN_FORM()
        {
            InitializeComponent();
        }

        private void GRN_FORM_Load(object sender, EventArgs e)
        {
            this.label1.Text = "GRN FORM";
            this.label2.Text = "POID:";
            this.label3.Text = "Vendor ID:";
            this.label4.Text = "Vendor Name:";
            this.label5.Text = "Data Show:";
            this.label6.Text = "GRN ID:";
            this.label7.Text = "Department";
            this.label8.Text = "Total Amount";
            this.label11.Text = "PQTY REC ";

            this.button1.Text = "Creat GRN";
            this.button2.Text = "Main Menu";
            this.button3.Text = "Products detail";
            this.button4.Text = "GRN Details";
            //To Populate Combobox1 of POID
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select POID from PO where status='Open'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["POID"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //For Auto ID Generation Of GRN............................
            {
                int c = 0;
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(grnid) from grn where poid='" + comboBox1.Text + "'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]); c++;
                }
               
                {
                    textBox3.Text = "GRN-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                }
                conn.oleDbConnection1.Close();
            }

            //To Populate Combobox1 from POID.....................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from PO where POID='" + comboBox1.Text + "'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["vid"].ToString();
                    textBox2.Text = dr["vname"].ToString();
                    textBox4.Text = dr["vdept"].ToString();
                    textBox5.Text = dr["totalamount"].ToString();

                }
                conn.oleDbConnection1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                //Data Insertion in GRN Prducts table....................................
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("insert into GRNProducts(grnid,vname,pqty)values(@grnid,@vname,@pqty)", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@grnid", this.textBox3.Text);
                cmd.Parameters.AddWithValue("@vname", this.textBox2.Text);
                cmd.Parameters.AddWithValue("@pqty", this.textBox6.Text);
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();

                MessageBox.Show("Data of GRN Products Inserted in Table");

            }

            //Data Insertion int GRN Table.............................
            {
                {
                    conn.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("insert into GRN(GRNID,POID,VName,VID,TotalAmount)values(@GRNID,@POID,@VName,@VID,@TotalAmount)", conn.oleDbConnection1);
                    cmd.Parameters.AddWithValue("@GRNID", this.textBox3.Text);
                    cmd.Parameters.AddWithValue("@POID", this.comboBox1.Text);
                    cmd.Parameters.AddWithValue("@VName", this.textBox2.Text);
                    cmd.Parameters.AddWithValue("@VID", this.textBox1.Text);
                    cmd.Parameters.AddWithValue("@TotalAmount", this.textBox5.Text);
                    //cmd.Parameters.AddWithValue("@DDate",dateTimePicker1);
                    // cmd.Parameters.AddWithValue("@GRDate",dateTimePicker2);
                    cmd.ExecuteNonQuery();
                    conn.oleDbConnection1.Close();

                    MessageBox.Show("GRN Created");
                }

            }
            // To Close PO status............................
            {

                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Update po set status='Close' where PONID ='" + this.comboBox1.Text+ "'", conn.oleDbConnection1);
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //To Show Products Data....................................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from poproducts where POID=@POID", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@POID", comboBox1.Text);
                OleDbDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                conn.oleDbConnection1.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //To Show GRN Data....................................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from grn ", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                conn.oleDbConnection1.Close();
            }
        }
    }
}