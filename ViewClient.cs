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

    public partial class ViewClient : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public ViewClient()
        {
            InitializeComponent();
            // BindCombo();
            //datagridShow();
        }
        private void BindCombo()
        {
            DataRow dr;
            con.Open();
            SqlCommand cmd = new SqlCommand("select clientid from client", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt.Rows[i][0].ToString().Trim());
            }
            dr = dt.NewRow();
            con.Close();
        }
        private void ViewClient_Load(object sender, EventArgs e)
        {
            panel2.Height = (80 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("All Client");
            comboBox1.Items.Add("Search by ID");
            BindCombo();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            comboBox2.Visible = false;

            if (comboBox1.Text == "")
            {
                MessageBox.Show("No Data Found...");
            }
            else if(comboBox1.SelectedIndex == 0)
            {
                try
                {
                    con.Close();
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select * from client ", con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        //dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][0].ToString();
                        dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][1].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = ds.Tables[0].Rows[i][2].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = ds.Tables[0].Rows[i][3].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = ds.Tables[0].Rows[i][4].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = ds.Tables[0].Rows[i][5].ToString();
                    }
                    con.Close();
                }
                catch { }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                //try
                //{
                    label1.Visible = true;
                    comboBox2.Visible = true;

                //    con.Close();
                //    con.Open();
                //    SqlDataAdapter sda = new SqlDataAdapter("Select * from client where clientid = '"+comboBox2.Text+"'", con);
                //    DataSet ds = new DataSet();
                //    sda.Fill(ds);
                //    dataGridView1.Rows.Clear();
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //         dataGridView1.Rows.Add();
                //        //    dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][0].ToString();
                //        dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][1].ToString();
                //        dataGridView1.Rows[i].Cells[1].Value = ds.Tables[0].Rows[i][2].ToString();
                //        dataGridView1.Rows[i].Cells[2].Value = ds.Tables[0].Rows[i][3].ToString();
                //        dataGridView1.Rows[i].Cells[3].Value = ds.Tables[0].Rows[i][4].ToString();
                //        dataGridView1.Rows[i].Cells[4].Value = ds.Tables[0].Rows[i][5].ToString();
                //    }
                //    con.Close();
                //}
                //catch { }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label1.Visible = true;
                comboBox2.Visible = true;

                con.Close();
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from client where clientid = '" + comboBox2.Text + "'", con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.Rows.Clear();
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
                con.Close();
            }
            catch { }
        }
        private void SearchClientInformation()
        {
            try
            {
                label1.Visible = true;
                comboBox2.Visible = true;

                if (comboBox2.Text == "")
                {
                    MessageBox.Show("Please enter a Client ID");
                    return;
                }

                con.Close();
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM client WHERE clientid = @ClientID", con);
                sda.SelectCommand.Parameters.AddWithValue("@ClientID", comboBox2.Text);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                con.Close();

                dataGridView1.Rows.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = ds.Tables[0].Rows[i][2].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = ds.Tables[0].Rows[i][3].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = ds.Tables[0].Rows[i][4].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = ds.Tables[0].Rows[i][5].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchClientInformation();
            }
        }
    }
}
