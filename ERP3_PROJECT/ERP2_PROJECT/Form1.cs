using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP2_PROJECT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "LOGIN_FORM";
            this.label1.Text = "LOGIN ";
            this.label2.Text = "Username:";
            this.label3.Text = "Password:";
            this.button2.Text = "LOGIN";
            this.button1.Text = "CLEAR";

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.textBox1.Text = "Enter your name";
            this.textBox2.Text = "Enter your password";

            this.AcceptButton = this.button2;

            this.textBox1.CharacterCasing = CharacterCasing.Upper;
            this.textBox2.CharacterCasing = CharacterCasing.Upper;
        }

        private void button2_Click(object sender, EventArgs e)
        {

          //  if (this.textBox1.Text == "M" && this.textBox2.Text == "M")
           // {
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
           /* }
            else
            {
                MessageBox.Show("Invalid Username Or Password!" + Environment.NewLine + "Please Try Again");
            }*/
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
