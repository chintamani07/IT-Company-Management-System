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
    public partial class SalaryTypeReport : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public SalaryTypeReport()
        {
            InitializeComponent();
        }

        private void SalaryTypeReport_Load(object sender, EventArgs e)
        {
            panel2.Height = (70 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            cmbempid.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbempid.Items.Add("All");
            cmbempid.Items.Add("3 Month");
            cmbempid.Items.Add("6 Month");
            cmbempid.Items.Add("Custom Date");
            BindCombo();
        }

        private void BindCombo()
        {
            DataRow dr;
            con.Open();
            SqlCommand cmd = new SqlCommand("select EmployeeID from Employeetable", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][0].ToString().Trim());
            }
            dr = dt.NewRow();
            con.Close();
        }
        private void cmbempid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string three, six;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            label1.Visible = false;
            label3.Visible = false;

            if (cmbempid.SelectedIndex == 3)
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label1.Visible = true;
                label3.Visible = true;
            }
            else if (cmbempid.SelectedIndex == 0)
            {
                try
                {
                    con.Close();
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM SalaryTable  ", con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
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
                    con.Close();
                }
                catch { }
            }
            else if (cmbempid.SelectedIndex == 1)
            {
                //three = DateTime.Now.AddDays(-90).ToString();
                try
                {
                    con.Close();
                    string threeMonthsAgo = DateTime.Now.AddDays(-90).ToString("MM/dd/yyyy");
                    string beforedate = DateTime.Now.ToString("MM/dd/yyyy");
                    string query = "SELECT * FROM SalaryTable WHERE CAST(convert(varchar, convert(date, datet, 103), 101) as date)>='" + threeMonthsAgo + "' and CAST(convert(varchar, convert(date, datet, 103), 101) as date)<='" + beforedate + "' and Empid='" + comboBox1.Text + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
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
                    con.Close();
                }
                catch { }
            }
            else if (cmbempid.SelectedIndex == 2)
            {
                try
                {
                    string sixMonthsAgo = DateTime.Now.AddDays(-180).ToString("MM/dd/yyyy");
                    string beforedate = DateTime.Now.ToString("MM/dd/yyyy");
                    string query = "SELECT * FROM SalaryTable WHERE CAST(convert(varchar, convert(date, datet, 103), 101) as date)>='" + sixMonthsAgo + "' and CAST(convert(varchar, convert(date, datet, 103), 101) as date)<='" + beforedate + "' and Empid='" + comboBox1.Text + "'";
                    
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
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
                    con.Close();
                }
                catch { }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbempid.Text == "")
            {
                MessageBox.Show("No Data Found...");
            }
            else
            {
                SalartTypeCrsytalReport rf = new SalartTypeCrsytalReport();
                rf.Show();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM salarytable where empid = '" + comboBox1.Text + "' and ename='" + cmbempname.Text + "'", con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                // Create a new DataTable to hold the data
                DataTable dt = new DataTable();
                dt.Columns.Add("Empid", typeof(string));
                dt.Columns.Add("Ename", typeof(string));
                dt.Columns.Add("Perday", typeof(string));
                dt.Columns.Add("Noofdays", typeof(string));
                dt.Columns.Add("basicsal", typeof(string));
                dt.Columns.Add("Halfday", typeof(string));
                dt.Columns.Add("absent", typeof(string));
                dt.Columns.Add("Totaldeduction", typeof(string));
                dt.Columns.Add("netsal", typeof(string));
                dt.Columns.Add("totalsal", typeof(string));
                dt.Columns.Add("datet", typeof(string));

                // Iterate through each row of the DataGridView
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (!dgvRow.IsNewRow)
                    {
                        // Create a new row for the DataTable and populate it with data from the current row in the DataGridView
                        DataRow newRow = dt.Rows.Add();
                        newRow["Empid"] = dgvRow.Cells[0].Value.ToString();
                        newRow["Ename"] = dgvRow.Cells[1].Value.ToString();
                        newRow["Perday"] = dgvRow.Cells[2].Value.ToString();
                        newRow["Noofdays"] = dgvRow.Cells[3].Value.ToString();
                        newRow["basicsal"] = dgvRow.Cells[4].Value.ToString();
                        newRow["Halfday"] = dgvRow.Cells[5].Value.ToString();
                        newRow["absent"] = dgvRow.Cells[6].Value.ToString();
                        newRow["Totaldeduction"] = dgvRow.Cells[7].Value.ToString();
                        newRow["netsal"] = dgvRow.Cells[8].Value.ToString();
                        newRow["totalsal"] = dgvRow.Cells[9].Value.ToString();
                        newRow["datet"] = dgvRow.Cells[10].Value.ToString();
                    }
                }
                // Add the DataTable to the DataSet
                ds.Tables.Add(dt);

                // Write the schema to an XML file (optional)
                ds.WriteXmlSchema("Salarysample.xml");

                // Set the DataSource for the Crystal Report
                CrystalReport2 cr = new CrystalReport2();
                cr.SetDataSource(ds);

                // Set the ReportSource for the CrystalReportViewer
                rf.crystalReportViewer2.ReportSource = cr;
                rf.crystalReportViewer2.Refresh();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = dateTimePicker1.Value;
                DateTime toDate = dateTimePicker2.Value;
                con.Close();
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM SalaryTable WHERE CAST(convert(varchar, convert(date, datet, 103), 101) as date)>='" + fromDate + "' and CAST(convert(varchar, convert(date, datet, 103), 101) as date)<='" + toDate + "' and Empid='" + comboBox1.Text + "'", con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.Rows.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
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
                con.Close();
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please Select Employee Id");
            }
            else
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Employeetable where EmployeeID ='" + comboBox1.Text + "'", con);
                DataTable ds = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    cmbempname.Text = row["name"].ToString();
                }
            }
        }
    }
}