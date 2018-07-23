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
    public partial class Invoice_Payable : Form
    {
        Connection_DB conn = new Connection_DB();
        public Invoice_Payable()
        {
            InitializeComponent();
        }

        private void Invoice_Payable_Load(object sender, EventArgs e)
        {
            this.label1.Text = "INVOICE PAYABLE";
            this.label2.Text = "GRN ID:";
            this.label4.Text = "Amount:";
            this.label5.Text = "POID:";
            this.label6.Text = "Vendor ID:";
            this.label7.Text = "Discount %:";
            this.label8.Text = "Total Amount Payable:";
            this.label9.Text = "Show Data:";
            this.label11.Text = "Vendor Name:";
            this.label12.Text = "Invoice ID";
            this.label13.Text = "C_Invoice Date";


            this.button1.Text = "Create Invoice";
            this.button2.Text = "BACK";
            
            //Auto INVOICE ID Geenration..............................
            {
                int c = 0;
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(InvoiceID) from invoice", conn.oleDbConnection1);
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
            //Populate comboBox1 foR GRN ID.................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select GRNID from GRN", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    comboBox1.Items.Add(dr["GRNID"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // //Fetching Data from GRN Table in textboxes................
            {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("select * from GRN where GRNID=@GRNID", conn.oleDbConnection1);
            cmd.Parameters.AddWithValue("@GRNID",this.comboBox1.Text);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.textBox3.Text = dr["POID"].ToString();
                //this.textBox1.Text = dr["GRNDate"].ToString();
                //this.textBox7.Text = dr["DDate"].ToString();
                this.textBox8.Text = dr["vname"].ToString();
                this.textBox4.Text = dr["vid"].ToString();
                this.textBox2.Text = dr["TotalAmount"].ToString();

            }
            conn.oleDbConnection1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            {
                //Insertion of InVoice in  its table...................
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("insert into invoice(vendorid,vendorname,amountpayable,grnid,invoiceID,cdate)values(@vendorid,@vendorname,@amountpayable,@grnid,@invoiceID,@cdate)", conn.oleDbConnection1);
            cmd.Parameters.AddWithValue("@vendorid", this.textBox4.Text);
            cmd.Parameters.AddWithValue("@vendorname", this.textBox8.Text);
            cmd.Parameters.AddWithValue("@amountpayable", this.textBox6.Text);
            cmd.Parameters.AddWithValue("@grnid", this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@invoiceID", this.textBox9.Text);
            cmd.Parameters.AddWithValue("@cdate", this.dateTimePicker1);
            cmd.ExecuteNonQuery();
            conn.oleDbConnection1.Close();

            MessageBox.Show("Data Inserted in Invoice Table");
        }
            {
                //To Show GRN Data....................................
                {
                    conn.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("Select * from invoice ", conn.oleDbConnection1);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    conn.oleDbConnection1.Close();
                }
              /*  // To Close GRN status............................
                {

                    conn.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("Update GRN set status='Close' where invoiceid ='" + textBox9.Text + "'", conn.oleDbConnection1);
                    cmd.ExecuteNonQuery();
                    conn.oleDbConnection1.Close();
                }*/
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int disc;
            int amount;
            int totalAmount ;

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
    }
}
