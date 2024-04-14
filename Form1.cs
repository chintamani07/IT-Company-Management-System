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

    public partial class Form1 : Form
    {
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        public static string UserName;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtuser.Text.Trim() == "" || txtpassword.Text.Trim() == "")
            {
                    MessageBox.Show("Please fill missing values");
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from LoginTable where username = '" + txtuser.Text + "' and password='" + txtpassword.Text + "'", con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
            //    int count = ds.Tables[0].Rows.Count;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Login Successfully");
                    UserName = txtuser.Text;
                    Home h1 = new Home();
                    h1.ShowDialog();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username and Password is not Correct");
                }
            }
        }
        private void btnreset_Click(object sender, EventArgs e)
        {
            txtuser.Text = "";
            txtpassword.Text = "";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want to Exit", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnlogin.PerformClick();
            }
        }
        private void txtuser_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
