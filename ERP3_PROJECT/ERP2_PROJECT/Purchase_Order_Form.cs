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
    public partial class Purchase_Order_Form : Form
    {
        Connection_DB conn = new Connection_DB();

        string[] prds = new string[50];
        string[] pname = new string[50];
        int[] pqty = new int[50];        
        int[] pprice=new int[50];
        int counter = 0;
 
 

        public Purchase_Order_Form()
        {
            InitializeComponent();
        }

        private void Purchase_Order_Form_Load(object sender, EventArgs e)
        {
            this.button1.Text = "Add Product";
            this.button2.Text = "Create Purchase Order";

            this.groupBox1.Text = "DEPART";
            this.groupBox2.Text = "VENDER DETAILS";
            this.groupBox3.Text = "PRODUCT DETAILS";

            this.label1.Text = "Purchase Order Form";
            this.label2.Text = "Choose Department:";
            this.label3.Text = "POID:";
            this.label4.Text = "PO Creation Date:";
            this.label6.Text = "Dilevary Date:";
            this.label7.Text = "Vendor ID:";
            this.label8.Text = "Vendor Name:";
            this.label9.Text = "Comapny Name:";
            this.label10.Text = "Phone No#";
            this.label14.Text = "Product ID:";
            this.label13.Text = "Product Name:";
            this.label12.Text = "Product Price:";
            this.label11.Text = "Product Quantity:";
            this.label5.Text = "Show Total Products:";
            this.label15.Text = "Total Amount:";
            this.label16.Text = "View All Data Of:";

            this.button3.Text = "PO";
            this.button4.Text = "POProdutcs";
            this.button5.Text = "Main Menu";

            this.textBox9.ReadOnly = true;

            this.textBox8.ReadOnly = true;

            {
                //Populate comboBox1 foR Department.................

               conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select vgroup from vendor", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                  
                    comboBox1.Items.Add(dr["vgroup"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
            //Populate ComboBox3 for Product details....................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select pid from Products", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox3.Items.Add(dr["pid"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {//POID AUTO gENERATE........................
            {
                int c = 0;
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(poid) from po where vdept='" + comboBox1.Text + "'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]); c++;
                }
                if (comboBox1.Text == "Consumer") 
                {
                    textBox1.Text = "Con-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                }

                else if(comboBox1.Text == "Marketing") 
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

            //For Vendor ID combo2 Population................
            {
                conn.oleDbConnection1.Open();
               OleDbCommand cmd = new OleDbCommand("select vid from vendor where (vgroup=@vgroup) AND  (vstatus='Active')" ,conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@vgroup", comboBox1.Text);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.comboBox2.Items.Clear();
                    comboBox2.Items.Add(dr["vid"].ToString());
                }
                conn.oleDbConnection1.Close();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from products where pid='" + comboBox3.Text + "'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox7.Text = dr["pname"].ToString();
                textBox6.Text = dr["baseprice"].ToString();
                
            }
            conn.oleDbConnection1.Close();
        }

        //CODING OF ADD PRODUCT.............................
        private void button1_Click(object sender, EventArgs e)
        {
            int baseprice=0;
            int productqty=0;

            baseprice=Convert.ToInt32(textBox6.Text);
            productqty=Convert.ToInt32(textBox5.Text);

         //   baseprice* productqty = Convert.ToInt32(this.textBox9.Text); 
            this.textBox9.Text = Convert.ToString(baseprice * productqty);

            //multiple Products storing in Array
            prds[counter] = comboBox3.Text;
            pname[counter] = this.textBox7.Text;
            pqty[counter] = Convert.ToInt32(textBox5.Text);
            pprice[counter] = Convert.ToInt32(textBox9.Text);
            counter++;


            //Data Showing In textBox8
            this.textBox8.Text += "****Purchase Order Details****"+Environment.NewLine+Environment.NewLine;
            this.textBox8.Text += "Department:  " +comboBox1.Text+ Environment.NewLine;
            this.textBox8.Text += "POID:        " +textBox1.Text+ Environment.NewLine;
            this.textBox8.Text += "PO Creation Date:" + dateTimePicker2.Text + Environment.NewLine;
            this.textBox8.Text += "Delivery Date:" + dateTimePicker1.Text + Environment.NewLine + Environment.NewLine;

            this.textBox8.Text += "****Vendor Details****" + Environment.NewLine+Environment.NewLine;
            this.textBox8.Text += "Vendor ID:   " +comboBox2.Text+ Environment.NewLine;
            this.textBox8.Text += "Vendor Name: " +textBox2.Text+ Environment.NewLine;
            this.textBox8.Text += "Company Name:" +textBox3.Text+ Environment.NewLine;
            this.textBox8.Text += "Phone No.#   " + textBox4.Text + Environment.NewLine + Environment.NewLine;

            this.textBox8.Text += "****Product Details****" + Environment.NewLine + Environment.NewLine;
            this.textBox8.Text += "Product ID:  " +comboBox3.Text+ Environment.NewLine;
            this.textBox8.Text += "Product Name:" +textBox7.Text+ Environment.NewLine;
            this.textBox8.Text += "Product Price:" +textBox6.Text+ Environment.NewLine;
            this.textBox8.Text += "Product Quantity:" + textBox5.Text + Environment.NewLine;
            this.textBox8.Text += "Total Amount :" + textBox9.Text + Environment.NewLine;


            MessageBox.Show("Value edited....");
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from vendor where vid='" + comboBox2.Text + "'", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["vname"].ToString();
                textBox3.Text = dr["cpname"].ToString();
                textBox4.Text = dr["ph1"].ToString();
                
            }
            conn.oleDbConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                //Insertion of Purchase Order In PO Table..................
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("insert into PO(poid,vdept,vname,vid,vcpph,podate,ddate,TotalAmount,Approve)values(@poid,@vdept,@vname,@vid,@vcpph,@podate,@ddate,@TotalAmount,@Approve)", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@poid", this.textBox1.Text);
                cmd.Parameters.AddWithValue("@vdept", this.comboBox1.Text);
                cmd.Parameters.AddWithValue("@vname", this.textBox2.Text);
                cmd.Parameters.AddWithValue("@vid", this.comboBox2.Text);
                cmd.Parameters.AddWithValue("@vcpph", this.textBox4.Text);
                cmd.Parameters.AddWithValue("@podate", this.dateTimePicker1);
                cmd.Parameters.AddWithValue("@ddate", this.dateTimePicker1);
                cmd.Parameters.AddWithValue("@TotalAmount", this.textBox9.Text);
                cmd.Parameters.AddWithValue("@Approve=","Approved");
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();
                MessageBox.Show("Transaction done!!");
            }
            {
                {
                    //Insertion of Products in PoProducts table.................
                    try
                    {
                        //  for(int i=0; i<prds.Length; i++)
                        for (int counter = 0; counter < prds.Length; counter++)
                        {
                            conn.oleDbConnection1.Open();
                            OleDbCommand cmd = new OleDbCommand("insert into POProducts(poid,pid,pname,pqty)values(@poid,@pid,@pname,@pqty)", conn.oleDbConnection1);
                            cmd.Parameters.AddWithValue("@poid", this.textBox1.Text);
                            cmd.Parameters.AddWithValue("@pid", prds[counter]);
                            cmd.Parameters.AddWithValue("@pname", pname[counter]);
                            cmd.Parameters.AddWithValue("@pqty", pqty[counter]);
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

        private void button3_Click(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from PO", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            conn.oleDbConnection1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from POProducts", conn.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            conn.oleDbConnection1.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
