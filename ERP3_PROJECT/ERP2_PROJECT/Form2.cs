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
    public partial class Form2 : Form
    {
        Connection_DB conn = new Connection_DB();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.label2.Text="All Data Of Vendor";

            this.button1.Text = "CUSTOMER";
            this.button2.Text = "VENDOR";
            this.button3.Text = "PURCHASE ORDER";
            this.button4.Text = "CREATE GRN";
            this.button5.Text = "CREATE INVOICE";
            this.button9.Text = "SALES ORDER";
            this.button10.Text = "DELIVARY CHALLAN";

            // Vendor buttons...........................
            this.button6.Text = "View All Vendors";
            this.button7.Text = "Insert Vendor";
            this.button8.Text = "Vender Main";
            this.button11.Text = "INVOICE RECIEVABLE";

            this.button6.Visible = false;
            this.button7.Visible = false;
            this.button8.Visible = false;
            this.label2.Visible = false;

            //Vendor Data view.........................
            this.dataGridView1.Visible = false;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.label1.Text = "WELCOME";

        }

        // Vendor Action................................

        private void button2_Click(object sender, EventArgs e)
        {
            this.label1.Visible = false;
            this.button6.Visible = true;
            this.button7.Visible = true;
            this.button8.Visible = true;

           
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            Customer_Form cf = new Customer_Form();
            cf.Show();
            this.Hide();
        }

        //Vendor All Data View.......................................
        private void button6_Click(object sender, EventArgs e)
        {
            this.label2.Visible = true;
            
            this.dataGridView1.Visible = true;
            this.button7.Visible = false;

            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from vendor", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            conn.oleDbConnection1.Close();
        }

        //Vendor Main Menu..........................................
        private void button8_Click(object sender, EventArgs e)
        {
            this.label2.Visible = false;
            this.dataGridView1.Visible = false;
            this.button6.Visible = true;
            this.button7.Visible = true;
        }
        
        //Vendor Insertion..........................................
        private void button7_Click(object sender, EventArgs e)
        {
            this.label2.Visible = false;
            Vendor_Insertion vi = new Vendor_Insertion();
            vi.Show();
            this.Hide();
        }

        //Purchase Order Form.........................................
        private void button3_Click(object sender, EventArgs e)
        {
            Purchase_Order_Form pof = new Purchase_Order_Form();
            pof.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GRN_FORM gf = new GRN_FORM();
            gf.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Invoice_Payable ip = new Invoice_Payable();
            ip.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Sales_Order so = new Sales_Order();
            so.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Delivery_Challan dc = new Delivery_Challan();
            dc.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Invoice_Recievable ir = new Invoice_Recievable();
            ir.Show();
            this.Hide();
        }
    }
}
