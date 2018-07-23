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
    public partial class Delivery_Challan : Form
    {
        Connection_DB conn = new Connection_DB();
        public Delivery_Challan()
        {
            InitializeComponent();
        }

        private void Delivery_Challan_Load(object sender, EventArgs e)
        {
            this.label1.Text = "DELIVERY CHALLAN";
            this.label2.Text = "SOID:";
            this.label3.Text = "Customer ID:";
            this.label4.Text = "Customer Name:";
            this.label5.Text = "Data Show:";
            this.label6.Text = "DC ID:";
            this.label7.Text = "Department:";
            this.label8.Text = "Total Amount:";
            this.label11.Text = "Pro.Qty.Delivere:";

            this.button1.Text = "Creat Delivery Challan";
            this.button2.Text = "Main Menu";
            this.button3.Text = "Products detail";
            this.button4.Text = "DC Details";

            //To Populate Combobox1 of POID
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select SOID from SO where status='Open'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["SOID"].ToString());
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
                OleDbCommand cmd = new OleDbCommand("select count(DCid) from DC where SOID='" + comboBox1.Text + "'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]); c++;
                }

                {
                    textBox3.Text = "DC-00" + c.ToString() + "-" + System.DateTime.Today.Year;
                }
                conn.oleDbConnection1.Close();
            }

            //To Populate Combobox1 from POID.....................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from SO where SOID='" + comboBox1.Text + "'", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["Cid"].ToString();
                    textBox2.Text = dr["cname"].ToString();
                    textBox4.Text = dr["cgroup"].ToString();
                    textBox5.Text = dr["totalamount"].ToString();

                }
                conn.oleDbConnection1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // To Close DC status............................
            {

                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Update DC set status='Open' where SOID ='" + this.comboBox1.Text + "'", conn.oleDbConnection1);
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();
            }
            {
                //Data Insertion in DC Prducts table....................................
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("insert into DCProducts(DCid,Cname,pqty)values(@DCid,@Cname,@pqty)", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@DCid", this.textBox3.Text);
                cmd.Parameters.AddWithValue("@cname", this.textBox2.Text);
                cmd.Parameters.AddWithValue("@pqty", this.textBox6.Text);
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();

                MessageBox.Show("Data of DC Products Inserted in Table");

            }

            //Data Insertion int DC Table.............................
            {
                {
                    conn.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("insert into DC(DCID,SOID,CName,CID,TotalAmount)values(@DCID,@SOID,@CName,@CID,@TotalAmount)", conn.oleDbConnection1);
                    cmd.Parameters.AddWithValue("@DCID", this.textBox3.Text);
                    cmd.Parameters.AddWithValue("@SOID", this.comboBox1.Text);
                    cmd.Parameters.AddWithValue("@CName", this.textBox2.Text);
                    cmd.Parameters.AddWithValue("@CID", this.textBox1.Text);
                    cmd.Parameters.AddWithValue("@TotalAmount", this.textBox5.Text);
                    //cmd.Parameters.AddWithValue("@DDate",dateTimePicker1);
                    // cmd.Parameters.AddWithValue("@GRDate",dateTimePicker2);
                    cmd.ExecuteNonQuery();
                    conn.oleDbConnection1.Close();

                    MessageBox.Show("DC Created");
                }

            }
            // To Close PO status............................
            {

                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Update SO set status='Close' where SOID ='" + this.comboBox1.Text + "'", conn.oleDbConnection1);
                cmd.ExecuteNonQuery();
                conn.oleDbConnection1.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                OleDbCommand cmd = new OleDbCommand("Select * from DCproducts where DCID=@DCID", conn.oleDbConnection1);
                cmd.Parameters.AddWithValue("@DCID", comboBox1.Text);
                OleDbDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                conn.oleDbConnection1.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //To Show DC Data....................................
            {
                conn.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from DC ", conn.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                conn.oleDbConnection1.Close();
            }
        }
    }
}
