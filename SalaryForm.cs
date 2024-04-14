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
    public partial class SalaryForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public SalaryForm()
        {
            InitializeComponent();
            //  BindCombo();
            //  datagridshow();
        }

        private void SalaryForm_Load(object sender, EventArgs e)
        {
            panel4.Height = (45 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            BindCombo();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
                cmbempid.Items.Add(dt.Rows[i][0].ToString().Trim());
            }
            dr = dt.NewRow();
            con.Close();
        }

        private void SearchEmployeeByID()
        {
            if (cmbempid.Text == "")
            {
                MessageBox.Show("Please enter an Employee ID");
                return;
            }

            // If Employee ID is provided, proceed with searching and calculations
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employeetable WHERE EmployeeID = @EmployeeID", con);
            DataTable dtEmployee = new DataTable();
            SqlDataAdapter daEmployee = new SqlDataAdapter(cmd);
            daEmployee.Fill(dtEmployee);
            con.Close();

            if (dtEmployee.Rows.Count > 0)
            {
                DataRow row = dtEmployee.Rows[0];
                cmbempname.Text = row["name"].ToString();
                txtbsalary.Text = row["Basicsalary"].ToString();

                DateTime currentDate = DateTime.Now;
                DateTime lastMonthDate = currentDate.AddMonths(-1);

                int daysInLastMonth = DateTime.DaysInMonth(lastMonthDate.Year, lastMonthDate.Month);
                txtnoofdays.Text = daysInLastMonth.ToString();

                float hourlyRate = float.Parse(txtbsalary.Text) / daysInLastMonth;
                txthourday.Text = hourlyRate.ToString();

                DateTime firstDayOfLastMonth = new DateTime(lastMonthDate.Year, lastMonthDate.Month, 1);
                DateTime lastDayOfLastMonth = new DateTime(lastMonthDate.Year, lastMonthDate.Month, daysInLastMonth);

                con.Close();
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from attendance where CAST(convert(varchar, convert(date, Date1, 103), 101) as date)>='" + firstDayOfLastMonth.ToString("MM-dd-yyyy") + "' and CAST(convert(varchar, convert(date, Date1, 103), 101) as date)<='" + lastDayOfLastMonth.ToString("MM-dd-yyyy") + "' and empid='" + cmbempid.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                int hcount = 0, fcount = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str = dt.Rows[i][6].ToString();
                    if (str == "Present(Half Day)")
                    {
                        hcount++;
                    }
                    else if (str == "Absent")
                    {
                        fcount++;
                    }
                }
                txthalfday.Text = hcount.ToString();
                txtabsent.Text = fcount.ToString();
            }
            else
            {
                // Clear other fields if no matching ID is found
                cmbempname.Text = "";
                txtbsalary.Text = "";
                txtnoofdays.Text = "";
                txthourday.Text = "";
                txthalfday.Text = "";
                txtabsent.Text = "";

                MessageBox.Show("Employee with ID " + cmbempid.Text + " does not exist.", "ID Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cmbempid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbempid.Text == "")
            {
                MessageBox.Show("Please Select Employee Id");
            }
            else
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Employeetable where EmployeeID ='" + cmbempid.Text + "'", con);
                DataTable ds = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    cmbempname.Text = row["name"].ToString();
                    txtbsalary.Text = row["Basicsalary"].ToString();
                }
                DateTime currentDate = DateTime.Now;
                // Subtract one month from the current date
                DateTime lastMonthDate = currentDate.AddMonths(-1);

                // Get the number of days in the last month
                int daysInLastMonth = DateTime.DaysInMonth(lastMonthDate.Year, lastMonthDate.Month);
                //   MessageBox.Show("Number of days in the current month: " + daysInCurrentMonth);
                txtnoofdays.Text = Convert.ToString(daysInLastMonth);

                txthourday.Text = (float.Parse(txtbsalary.Text) / float.Parse(txtnoofdays.Text)).ToString();

                // Set the day component to 1 to get the first day of the last month
                DateTime firstDayOfLastMonth = new DateTime(lastMonthDate.Year, lastMonthDate.Month, 1);
                DateTime lastDayOfLastMonth = new DateTime(lastMonthDate.Year, lastMonthDate.Month, 1).AddDays(int.Parse(txtnoofdays.Text));
                con.Close();
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from attendance where CAST(convert(varchar, convert(date, Date1, 103), 101) as date)>='" + firstDayOfLastMonth.ToString("MM-dd-yyyy") + "' and CAST(convert(varchar, convert(date, Date1, 103), 101) as date)<='" + lastDayOfLastMonth.ToString("MM-dd-yyyy") + "' and empid='" + cmbempid.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                int hcount = 0, fcount = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str = dt.Rows[i][6].ToString();
                    if (str == "Present(Half Day)")
                    {
                        hcount++;
                    }
                    else if (str == "Absent")
                    {
                        fcount++;
                    }
                }
                txthalfday.Text = hcount.ToString();
                txtabsent.Text = fcount.ToString();
            }
            printbuttonanable();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
            && !char.IsDigit(e.KeyChar)
           )
            {
                e.Handled = true;
                MessageBox.Show("Enter only Numbers");
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
            && !char.IsDigit(e.KeyChar)
           )
            {
                e.Handled = true;
                MessageBox.Show("Enter only Numbers");
            }
        }

        private void txtbsalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
            && !char.IsDigit(e.KeyChar)
           )
            {
                e.Handled = true;
                MessageBox.Show("Enter only Numbers");
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txttdeduction_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btncalculate_Click(object sender, EventArgs e)
        {
            //float basicsala = float.Parse(txtbsalary.Text);
            //int noofdays =int.Parse(txtnoofdays.Text);
            //float perdaysal;
            float absentsal = float.Parse(txtabsent.Text) * float.Parse(txthourday.Text);
            float halfday = float.Parse(txthourday.Text) / 2;
            float halsal = halfday * float.Parse(txthalfday.Text);
            float deduction = halsal + absentsal;
            txttdeduction.Text = deduction.ToString();

            float presentsal = (float.Parse(txtnoofdays.Text) - float.Parse(txtabsent.Text) - float.Parse(txthalfday.Text)) * float.Parse(txthourday.Text);
            float netsa = halsal + presentsal;
            txtnetsalary.Text = netsa.ToString();
            txttotal.Text = netsa.ToString();
            datagridshow();
            printbuttonanable();
        }

        private void txttdeduction_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnetsalary_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            cmbempid.Text = "";
            cmbempname.Text = "";
            txtabsent.Text = "";
            txtbsalary.Text = "";
            txthalfday.Text = "";
            txthourday.Text = "";
            txtnetsalary.Text = "";
            txtnoofdays.Text = "";
            txttdeduction.Text = "";
            txttotal.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SalaryPrint rf = new SalaryPrint();
            rf.Show();
            SqlDataAdapter sda = new SqlDataAdapter("select * from salarytable where Empid = '" + cmbempid.Text + "' order by id desc", con);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            sda.Fill(ds);
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
            
            dt.Rows.Add(ds.Tables[0].Rows[0][1].ToString(), ds.Tables[0].Rows[0][2].ToString(), ds.Tables[0].Rows[0][3].ToString(), ds.Tables[0].Rows[0][4].ToString(), ds.Tables[0].Rows[0][5].ToString(), ds.Tables[0].Rows[0][6].ToString(), ds.Tables[0].Rows[0][7].ToString(), Convert.ToDecimal(ds.Tables[0].Rows[0][8]).ToString("N2"), Convert.ToDecimal(ds.Tables[0].Rows[0][9]).ToString("N2"), Convert.ToDecimal(ds.Tables[0].Rows[0][10]).ToString("N2"), ds.Tables[0].Rows[0][11].ToString());

            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Salarysample.xml");
            //this.dataGridView1.DataSource = dt;
            CrystalReport1 cr = new CrystalReport1();
            cr.SetDataSource(ds);
            rf.crystalReportViewer1.ReportSource = cr;
            rf.crystalReportViewer1.Refresh();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void datagridshow()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //  dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                dataGridView1.Rows[i].Cells[0].Value = cmbempid.Text;
                dataGridView1.Rows[i].Cells[1].Value = cmbempname.Text;
                dataGridView1.Rows[i].Cells[2].Value = txthourday.Text;
                dataGridView1.Rows[i].Cells[3].Value = txtnoofdays.Text;
                dataGridView1.Rows[i].Cells[4].Value = txtbsalary.Text;
                dataGridView1.Rows[i].Cells[5].Value = txthalfday.Text;
                dataGridView1.Rows[i].Cells[6].Value = txtabsent.Text;
                dataGridView1.Rows[i].Cells[7].Value = txttdeduction.Text;
                dataGridView1.Rows[i].Cells[8].Value = txtnetsalary.Text;
                dataGridView1.Rows[i].Cells[9].Value = txttotal.Text;
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void printbuttonanable()
        {
            //DateTime currentDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
            //DateTime lastMonthDate = currentDate.AddMonths(-1);
            SqlDataAdapter sda = new SqlDataAdapter("Select * from SalaryTable where Empid ='" + cmbempid.Text + "'", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                button2.Visible = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            DateTime lastMonthDate = currentDate;

            // Check if salary data already exists for the specified employee and last month
            SqlDataAdapter checkDataAdapter = new SqlDataAdapter("SELECT COUNT(*) FROM SalaryTable WHERE Empid = @Empid AND MONTH(datet) = @LastMonth AND YEAR(datet) = @LastYear", con);
            checkDataAdapter.SelectCommand.Parameters.AddWithValue("@Empid", cmbempid.Text);
            checkDataAdapter.SelectCommand.Parameters.AddWithValue("@LastMonth", lastMonthDate.Month);
            checkDataAdapter.SelectCommand.Parameters.AddWithValue("@LastYear", lastMonthDate.Year);

            DataSet checkDataSet = new DataSet();
            checkDataAdapter.Fill(checkDataSet);
            int existingRecordsCount = Convert.ToInt32(checkDataSet.Tables[0].Rows[0][0]);

            if (existingRecordsCount > 0)
            {
                MessageBox.Show("Salary data for the last month already exists for this employee.");
            }

            else
            {
                // Check if the employee ID already exists in the SalaryTable
                //SqlDataAdapter idCheckDataAdapter = new SqlDataAdapter("SELECT COUNT(*) FROM SalaryTable WHERE Empid = @Empid", con);
                //idCheckDataAdapter.SelectCommand.Parameters.AddWithValue("@Empid", cmbempid.Text);

                //DataSet idCheckDataSet = new DataSet();
                //idCheckDataAdapter.Fill(idCheckDataSet);

                //int idExistingRecordsCount = Convert.ToInt32(idCheckDataSet.Tables[0].Rows[0][0]);

                //if (idExistingRecordsCount > 0)
                //{
                //    MessageBox.Show("Employee ID already exists in the database.");
                //}
                //else
                //{
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        string column1Value = row.Cells[0].Value.ToString();
                        string column2Value = row.Cells[1].Value.ToString();
                        string column3Value = row.Cells[2].Value.ToString();
                        string column4Value = row.Cells[3].Value.ToString();
                        string column5Value = row.Cells[4].Value.ToString();
                        string column6Value = row.Cells[5].Value.ToString();
                        string column7Value = row.Cells[6].Value.ToString();
                        string column8Value = row.Cells[7].Value.ToString();
                        string column9Value = row.Cells[8].Value.ToString();
                        string column10Value = row.Cells[9].Value.ToString();
                        string column11Value = DateTime.Now.ToString("MM/dd/yyyy");

                        con.Close();
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into SalaryTable(Empid, Ename, Perday, Noofdays, basicsal, Halfday, absent,Totaldeduction,netsal,totalsal,datet)values(@Empid, @Ename, @Perday, @Noofdays, @basicsal, @Halfday, @absent,@Totaldeduction,@netsal,@totalsal,@datet)", con);
                        cmd.Parameters.AddWithValue("@Empid", column1Value);
                        cmd.Parameters.AddWithValue("@Ename", column2Value);
                        cmd.Parameters.AddWithValue("@Perday", column3Value);
                        cmd.Parameters.AddWithValue("@Noofdays", column4Value);
                        cmd.Parameters.AddWithValue("@basicsal", column5Value);
                        cmd.Parameters.AddWithValue("@Halfday", column6Value);
                        cmd.Parameters.AddWithValue("@absent", column7Value);
                        cmd.Parameters.AddWithValue("@Totaldeduction", column8Value);
                        cmd.Parameters.AddWithValue("@netsal", column9Value);
                        cmd.Parameters.AddWithValue("@totalsal", column10Value);
                        cmd.Parameters.AddWithValue("@datet", column11Value);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        printbuttonanable();
                        //table.Rows.Add(textBox1.Text, comboBox2.Text, textBox6.Text, textBox4.Text, textBox5.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox2.Text, textBox13.Text, textBox3.Text, textBox10.Text, textBox11.Text, textBox12.Text);
                    }
                    MessageBox.Show("Data Inserted Successfully");
            }
        }
        private void cmbempid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchEmployeeByID();
            }
        }
    }
}
