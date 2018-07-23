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
    public partial class Sales_Order : Form
    {
        Connection_DB conn = new Connection_DB();

        string[] prds = new string[50];
        string[] pname = new string[50];
        int[] pqty = new int[50];
        int[] pprice = new int[50];
        int counter = 0;


        public Sales_Order()
        {
            InitializeComponent();
        }

        private void Sales_Order_Load(object sender, EventArgs e)
        {
            this.button1.Text = "Add Product";
            this.button2.Text = "Create Purchase Order";

            this.groupBox1.Text = "DEPART";
            this.groupBox2.Text = "CUSTOMER DETAILS";
            this.groupBox3.Text = "PRODUCT DETAILS";

            this.label17.Text = "Sales Order Form";
            this.label2.Text = "Choose Department:";
            this.label3.Text = "SOID:";
            this.label4.Text = "SO Creation Date:";
            this.label6.Text = "Dilevary Date:";
            this.label7.Text = "Customer ID:";
            this.label8.Text = "Customer Name:";
            this.label9.Text = "Comapny Name:";
            this.label10.Text = "Phone No#";
            this.label14.Text = "Product ID:";
            this.label13.Text = "Product Name:";
            this.label12.Text = "Product Price:";
            this.label11.Text = "Product Quantity:";
            this.label5.Text = "SALES ORDER SLIP:";
            this.label15.Text = "Total Amount:";
            this.label16.Text = "View All Data Of:";

            this.button3.Text = "SO";
            this.button4.Text = "SOProducts";
            this.button5.Text = "Main Menu";

            this.textBox9.ReadOnly = true;

            this.textBox8.ReadOnly = true;

            {
                //Populate comboBox1 foR Department.................

                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select cgroup from customer", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    comboBox1.Items.Add(dr["cgroup"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
            //Populate ComboBox3 for Product details....................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select sgid from SaleGoods", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox3.Items.Add(dr["sgid"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SOID AUTO gENERATE........................
            {
                int c = 0;
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(soid) from so where cgroup='" + comboBox1.Text + "'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]); c++;
                }
                if (comboBox1.Text == "Consumer")
                {
                    textBox1.Text = "Con-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                }

                else if (comboBox1.Text == "Marketing")
                {
                    textBox1.Text = "MR-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                }
                else if (comboBox1.Text == "HR")
                {
                    textBox1.Text = "HR-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                }
                else if (comboBox1.Text == "Sales")
                {
                    textBox1.Text = "SALES-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                }
                conn.oleDbConnection1.Close();
            }

            //For Customer ID combo2 Population................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select cid from customer where (cgroup=@cgroup) AND  (cstatus='Approved')", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@cgroup", comboBox1.Text);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.comboBox2.Items.Clear();
                    comboBox2.Items.Add(dr["cid"].ToString());
                }
                conn.oleDbConnection1.Close();
            }

        }

        //SalesGoods Id Populate in CB3......................................
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from SaleGoods where sgid='" + comboBox3.Text + "'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox7.Text = dr["sgname"].ToString();
                textBox6.Text = dr["baseprice"].ToString();
            }
            conn.oleDbConnection1.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int baseprice = 0;
            int productqty = 0;

            baseprice = Convert.ToInt32(textBox6.Text);
            productqty = Convert.ToInt32(textBox5.Text);

            //   baseprice* productqty = Convert.ToInt32(this.textBox9.Text); 
            this.textBox9.Text = Convert.ToString(baseprice * productqty);

            //multiple Products storing in Array......................
            prds[counter] = comboBox3.Text;
            pname[counter] = this.textBox7.Text;
            pqty[counter] = Convert.ToInt32(textBox5.Text);
            pprice[counter] = Convert.ToInt32(textBox9.Text);
            counter++;

            //Data Showing In textBox8...........................
            this.textBox8.Text += "****Sales Order Details****" + Environment.NewLine + Environment.NewLine;
            this.textBox8.Text += "Department:  " + comboBox1.Text + Environment.NewLine;
            this.textBox8.Text += "SOID:        " + textBox1.Text + Environment.NewLine;
            this.textBox8.Text += "SO Creation Date:" + dateTimePicker2.Text + Environment.NewLine;
            this.textBox8.Text += "Delivery Date:" + dateTimePicker1.Text + Environment.NewLine + Environment.NewLine;

            this.textBox8.Text += "****Customer Details****" + Environment.NewLine + Environment.NewLine;
            this.textBox8.Text += "Customer ID:   " + comboBox2.Text + Environment.NewLine;
            this.textBox8.Text += "Customer Name: " + textBox2.Text + Environment.NewLine;
            this.textBox8.Text += "Company Name:" + textBox3.Text + Environment.NewLine;
            this.textBox8.Text += "Phone No.#   " + textBox4.Text + Environment.NewLine + Environment.NewLine;

            this.textBox8.Text += "****Sale Goods Details****" + Environment.NewLine + Environment.NewLine;
            this.textBox8.Text += "Product ID:  " + comboBox3.Text + Environment.NewLine;
            this.textBox8.Text += "Product Name:" + textBox7.Text + Environment.NewLine;
            this.textBox8.Text += "Product Price:" + textBox6.Text + Environment.NewLine;
            this.textBox8.Text += "Product Quantity:" + textBox5.Text + Environment.NewLine;
            this.textBox8.Text += "Total Amount :" + textBox9.Text + Environment.NewLine;


            MessageBox.Show("Value edited....");
        }

        //Customer Details...........................
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from customer where cid='" + comboBox2.Text + "'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["cname"].ToString();
                textBox3.Text = dr["ccompany"].ToString();
                textBox4.Text = dr["ph1"].ToString();
            }
            conn.oleDbConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                //Insertion of Purchase Order In PO Table..................
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("insert into SO(SOID,SODATE,DDATE,CCOMPANY,CID,CNAME,CGROUP,CPH1,TOTALAMOUNT,STATUS)values(@SOID,@SODATE,@DDATE,@CCOMPANY,@CID,@CNAME,@CGROUP,@CPH1,@TOTALAMOUNT,@STATUS)", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@SOID", this.textBox1.Text);
                cmd.Parameters.AddWithValue("@SODATE", dateTimePicker1);
                cmd.Parameters.AddWithValue("@DDATE", dateTimePicker2);
                cmd.Parameters.AddWithValue("@CCOMPANY", this.textBox2.Text);
                cmd.Parameters.AddWithValue("@CID", this.comboBox2.Text);
                cmd.Parameters.AddWithValue("@CNAME", this.textBox3.Text);
                cmd.Parameters.AddWithValue("@CGROUP", this.comboBox1.Text);
                cmd.Parameters.AddWithValue("@CPH1", this.textBox4.Text);
                cmd.Parameters.AddWithValue("@TOTALAMOUNT", this.textBox9.Text);
                cmd.Parameters.AddWithValue("@STATUS=", "Open");
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();
                MessageBox.Show("Transaction done!!");
            }
            {
                {
                    try
                    {  //Insertion of Products in PoProducts table

                      //  for (int i = 0; i < prds.Length; i++)

                        for (int counter = 0; counter < prds.Length; counter++)
                        {
                            conn.oleDbConnection1.Open();
                            OleDbCommand cmd = new OleDbCommand("insert into SOProducts(SOID,SGID,SGNAME,SGQTY)values(@SOID,@SGID,@SGNAME,@SGQTY)", conn.oleDbConnection1);
                            cmd.Parameters.AddWithValue("@SOID", this.textBox1.Text);
                            cmd.Parameters.AddWithValue("@SGID", prds[counter]);
                            cmd.Parameters.AddWithValue("@SGNAME", pname[counter]);
                            cmd.Parameters.AddWithValue("@SGQTY", pqty[counter]);
                            cmd.ExecuteNonQuery();
                            conn.oleDbConnection1.Close();
                            MessageBox.Show("Multiple Products Added ");
                        }
                    }
                    catch(OleDbException ex) 
                    { 
                    throw ex;
                    }
                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
             Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        
        }
    }
}