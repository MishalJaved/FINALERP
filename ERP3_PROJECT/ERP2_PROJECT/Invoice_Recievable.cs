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
    public partial class Invoice_Recievable : Form
    {
        Connection_DB conn = new Connection_DB();
        public Invoice_Recievable()
        {
            InitializeComponent();
        }

        private void Invoice_Recievable_Load(object sender, EventArgs e)
        {
            this.label1.Text = "INVOICE RECEIVABLE";
            this.label2.Text = "DC ID:";
            this.label4.Text = "Amount:";
            this.label5.Text = "SOID:";
            this.label6.Text = "Customer ID:";
            this.label7.Text = "Discount %:";
            this.label8.Text = "Total Amount Recievable:";
            this.label9.Text = "Show Data:";
            this.label11.Text = "Customer Name:";
            this.label12.Text = "Invoice ID";
            this.label13.Text = "C_Invoice Date";


            this.button1.Text = "Create Invoice";
            this.button2.Text = "Main Menu";


            //Auto INVOICE ID Geenration..............................
            {
                int c = 0;
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(InvoiceRID) from invoice_REC", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]); c++;

                    {
                        this.textBox9.Text = "Inv-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                    }
                    conn.oleDbConnection1.Close();
                }
            }
            //Populate comboBox1 foR DC ID.................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select DCID from DC", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    comboBox1.Items.Add(dr["DCID"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // //Fetching Data from GRN Table in textboxes................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select * from DC where DCID=@DCID", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@DCID", this.comboBox1.Text);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    this.textBox3.Text = dr["SOID"].ToString();
                    //this.textBox1.Text = dr["DCDate"].ToString();
                    //this.textBox7.Text = dr["CRDate"].ToString();
                    this.textBox8.Text = dr["Cname"].ToString();
                    this.textBox4.Text = dr["Cid"].ToString();
                    this.textBox2.Text = dr["Totalamount"].ToString();

                }
                conn.oleDbConnection1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                //Insertion of InVoice in  its table...................
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("insert into invoice_rec(cid,cname,amountrec,dcid,invoiceRID,crdate)values(@cid,@cname,@amountrec,@dcid,@invoiceRID,@crdate)", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@cid", this.textBox4.Text);
                cmd.Parameters.AddWithValue("@cname", this.textBox8.Text);
                cmd.Parameters.AddWithValue("@amountrec", this.textBox6.Text);
                cmd.Parameters.AddWithValue("@dcid", this.comboBox1.Text);
                cmd.Parameters.AddWithValue("@invoiceRID", this.textBox9.Text);
                cmd.Parameters.AddWithValue("@crdate", this.dateTimePicker1);
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();

                MessageBox.Show("Data Inserted in Invoice Table");
            }
            {
                //To Show Invoice Data....................................
                {
                    conn.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("Select * from invoice_rec ", conn.oleDbConnection1);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    conn.oleDbConnection1.Close();
                }
                  // To Close GRN status............................
                  {

                      conn.oleDbConnection1.Open();
                      OleDbCommand cmd = new OleDbCommand("Update DC set status='Close' where DCID ='" + this.comboBox1.Text + "'", conn.oleDbConnection1);
                      cmd.ExecuteNonQuery();
                      conn.oleDbConnection1.Close();
                  }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int disc;
            int amount;
            int totalAmount;

            disc = Convert.ToInt32(this.textBox5.Text);
            amount = Convert.ToInt32(this.textBox2.Text);
            totalAmount = (amount * disc) / 100;
            this.textBox6.Text = Convert.ToString(totalAmount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
