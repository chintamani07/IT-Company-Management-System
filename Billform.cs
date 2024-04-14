using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CompanyManagementSystemFinal
{
    public partial class Billform : Form
    {
       SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public Billform()
        {
            InitializeComponent();
            txtname.Text = DisplayBillFormRecord.exname;
            textBox3.Text = DisplayBillFormRecord.pricetotal;

            //double total = Convert.ToDouble(textBox3.Text);
            //double givenPrice = Convert.ToDouble(textBox2.Text);
            //double remainingPrice = total - givenPrice;
            //textBox1.Text = remainingPrice.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Billform_Load(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && keypress == true)
            {
                if (double.Parse(textBox2.Text) <= double.Parse(textBox3.Text))
                {
                    textBox1.Text = (double.Parse(textBox3.Text) - double.Parse(textBox2.Text)).ToString();
                    //con.Close();
                    //con.Open();
                    //SqlCommand cmd1 = new SqlCommand("Update Billtable set total ='" + textBox1.Text + "' where name='" + txtname.Text + "'", con);
                    //cmd1.ExecuteNonQuery();
                    //con.Close();
                }
                else
                {
                    MessageBox.Show("Please enter value less than Total Amount");
                }
            }
            else
            {
                textBox1.Text = "0";
            }
        }
        Boolean keypress = false;
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                keypress = false;
                MessageBox.Show("Enter only Numbers");
            }
            else if (textBox2.Text != "")
            {
                keypress = true;
            }
        }
        string date1 = DateTime.Now.ToString("MM/dd/yyyy");
        private void btnsave_Click(object sender, EventArgs e)
        {
            //con.Close();
            //con.Open();
            //SqlDataAdapter sda = new SqlDataAdapter("Select * from Billtable where name = '" + txtname.Text + "'", con);
            //DataSet ds1 = new DataSet();
            //sda.Fill(ds1);

            //int i = ds1.Tables[0].Rows.Count;
            //if (i > 0)
            //{
            //    MessageBox.Show("These Item is Already Exists");
            //    ds1.Clear();
            //}
            //else
            if (textBox1.Text != "" && txtname.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Billtable(name,date,remaining,given,total,Paiddate)values(@name,@date,@remaining,@given,@total,@Paiddate)", con);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@date", DisplayBillFormRecord.currentDate);
                cmd.Parameters.AddWithValue("@remaining", textBox1.Text);
                cmd.Parameters.AddWithValue("@given", textBox2.Text);
                cmd.Parameters.AddWithValue("@total", textBox3.Text);
                cmd.Parameters.AddWithValue("@Paiddate", date1);

                cmd.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show("Data Inserted Successfully");
                MessageBox.Show("Successfully Added ", "Bill Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //update

                BillReport rf = new BillReport();
                rf.Show();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                int rowcount = 1;

                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("paiddate", typeof(string));
                dt.Columns.Add("remaining", typeof(string));
                dt.Columns.Add("given", typeof(string));
                dt.Columns.Add("total", typeof(string));
                dt.Rows.Add(txtname.Text, date1, textBox1.Text, textBox2.Text, textBox3.Text);

                ds.Tables.Add(dt);
                ds.WriteXmlSchema("bill.xml");
                //  this.dataGridView2.DataSource = dt;

                BillCrystalReport3 cr = new BillCrystalReport3();
                cr.SetDataSource(ds);

                rf.crystalReportViewer1.ReportSource = cr;
                rf.crystalReportViewer1.Refresh();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
