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
    public partial class ViewEmployess : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
      //  SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public ViewEmployess()
        {
            InitializeComponent();
        }
        public static Boolean selected = false;
        private void ViewEmployess_Load(object sender, EventArgs e)
        {
            selected = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("All Employees");
            comboBox1.Items.Add("Leave Employees");
            comboBox1.Items.Add("Current Employees");
            try
            {
                con.Close();
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from Employeetable ", con);
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
                        dataGridView1.Rows[i].Cells[5].Value = ds.Tables[0].Rows[i][6].ToString();
                        dataGridView1.Rows[i].Cells[6].Value = ds.Tables[0].Rows[i][7].ToString();
                        dataGridView1.Rows[i].Cells[7].Value = ds.Tables[0].Rows[i][8].ToString();
                        dataGridView1.Rows[i].Cells[8].Value = ds.Tables[0].Rows[i][9].ToString();
                        dataGridView1.Rows[i].Cells[9].Value = ds.Tables[0].Rows[i][10].ToString();
                        dataGridView1.Rows[i].Cells[10].Value = ds.Tables[0].Rows[i][11].ToString();
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
        public static string id;
        public static string empame;
        public static string address;
        public static string gender;
        public static string dob;
        public static string design;
        public static string bsalary;
        public static string salaryincre;
        public static string joindt;
        public static string leavedt;
      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                id = selectedRow.Cells[0].Value.ToString();
                empame = selectedRow.Cells[1].Value.ToString();
                address = selectedRow.Cells[2].Value.ToString();
                gender = selectedRow.Cells[3].Value.ToString();
                dob = selectedRow.Cells[4].Value.ToString();
                design = selectedRow.Cells[5].Value.ToString();
                bsalary = selectedRow.Cells[6].Value.ToString();
                salaryincre = selectedRow.Cells[7].Value.ToString();
                joindt = selectedRow.Cells[8].Value.ToString();
                leavedt = selectedRow.Cells[9].Value.ToString();
                selected = true;
                EmployeeRegister e1 = new EmployeeRegister();
                e1.Show();
            }
            catch { }
        //    selectedRow.Cells[3].Value.ToString();
        }
        public static string tdata;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                tdata = "SELECT * FROM  EmployeeTable ";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                tdata = "SELECT * FROM EmployeeTable WHERE LeaveDate!=''";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                tdata = "SELECT * FROM EmployeeTable WHERE LeaveDate=''";
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select type of Employee Data");
            }
            else
            {
                try
                {
                    con.Close();
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(tdata, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
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
                            dataGridView1.Rows[i].Cells[5].Value = ds.Tables[0].Rows[i][6].ToString();
                            dataGridView1.Rows[i].Cells[6].Value = ds.Tables[0].Rows[i][7].ToString();
                            dataGridView1.Rows[i].Cells[7].Value = ds.Tables[0].Rows[i][8].ToString();
                            dataGridView1.Rows[i].Cells[8].Value = ds.Tables[0].Rows[i][9].ToString();
                            dataGridView1.Rows[i].Cells[9].Value = ds.Tables[0].Rows[i][10].ToString();
                            dataGridView1.Rows[i].Cells[10].Value = ds.Tables[0].Rows[i][11].ToString();
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
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            selected = false;
            this.Close();
        }
    }
}
