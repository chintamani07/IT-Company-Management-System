using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CompanyManagementSystemFinal
{
    public partial class Client : Form
    {
      SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
       // SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public Client()
        {
            InitializeComponent();
        }
       
        private void SrNoAUTO()
        {
            // int SrNo=0;
            try
            {
                con.Close();
                con.Open();
                DataSet ds = new DataSet();
                ds.Tables.Clear();
                SqlDataAdapter sda = new SqlDataAdapter("Select max(clientid) From Client ", con);
                sda.Fill(ds);

                string a1 = ds.Tables[0].Rows[0][0].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (a1.Trim() != "")
                    {
                        txtid.Text = (int.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else
                    {
                        txtid.Text = "1";
                    }
                }
                else
                {
                    txtid.Text = "1";
                }
            }
            catch
            {
                txtid.Text = "1";
            }
            finally
            {
                con.Close();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from client Where clientid = '" + txtid.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Deleted Successfully");
            con.Close();
            datagridShow();
            clear();
            SrNoAUTO();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtid.Text.Trim() == "" || txtname.Text.Trim() == "" || txtaddress.Text.Trim() == "" || textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Please fill Missing Values");
            }
            else
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("update client set name=@name,address=@address,email=@email,mobile=@mobile where clientid='" + txtid.Text + "'", con);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@mobile", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show("Data Inserted Successfully");
                MessageBox.Show("Successfully Updated ", "Client Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            datagridShow();
            SrNoAUTO();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from client where clientid = '" + txtid.Text + "'", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            int i = ds.Tables[0].Rows.Count;
            if (i > 0)
            {
                MessageBox.Show("These Id is Already Exists");
                clear();
                ds.Clear();
            }
            else if (txtid.Text.Trim() == "" || txtname.Text.Trim() == "" || txtaddress.Text.Trim() == "" || textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Please fill Missing Values");
            }
            else
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into client(clientid,name,address,email,mobile)values(@clientid,@name,@address,@email,@mobile)", con);
                cmd.Parameters.AddWithValue("@clientid", txtid.Text);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@mobile", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show("Data Inserted Successfully");
                MessageBox.Show("Successfully Added ", "Client Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            datagridShow();
            SrNoAUTO();
        }
        private void clear()
        {
            txtid.Text = "";
            txtname.Text = "";
            txtaddress.Text = "";
            textBox2.Text = "";
            textBox1.Text = ""; 
        }

        private void Client_Load(object sender, EventArgs e)
        {
            SrNoAUTO();
            datagridShow();
            panel2.Height = (60 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
        }

        private void txtaddress_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                base.OnKeyPress(e);
                MessageBox.Show("Enter Characters only");
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
          && !char.IsDigit(e.KeyChar)
         )
            {
                e.Handled = true;
                MessageBox.Show("Enter only Numbers");
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;
            if (textBox1.Text.Trim() != string.Empty)
            {
                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(textBox1.Text.Trim()))
                {
                    MessageBox.Show("E-mail address format is not correct.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                }
            }
        }
        public void datagridShow()
        {
            try
            {
                con.Close();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from client ", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        //    dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][0].ToString();
                        dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][1].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = ds.Tables[0].Rows[i][2].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = ds.Tables[0].Rows[i][3].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = ds.Tables[0].Rows[i][4].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = ds.Tables[0].Rows[i][5].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No Record Found");
                }
            }
            catch
            {
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtid.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtaddress.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch { }
        }
    }
}
