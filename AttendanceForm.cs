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
    public partial class AttendanceForm : Form
    {
       
       // SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

       SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        //   string intime1 = DateTime.Now.ToString();
        TimeSpan intime1;
        TimeSpan outTime;
        public AttendanceForm()
        {
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {

            // BindCombo();
            datagridShow();
            //string dt = DateTime.Now.ToString("M/dd/yyyy");

            //// Check if attendance is marked for today

            //{
            //    con.Open();
            //    SqlDataAdapter sda = new SqlDataAdapter("SELECT Empid, EmpName, Day_Type FROM Attendance WHERE date1 = @Date1", con);
            //    sda.SelectCommand.Parameters.AddWithValue("@Date1", dt);

            //    DataSet ds = new DataSet();
            //    sda.Fill(ds);
            //    DataTable dtAttendance = new DataTable();
            //    if (ds.Tables[0].Rows.Count == 0)
            //    {
            //        // No attendance marked for today, add a row with "Absent" status

            //        dtAttendance.Columns.Add("Empid", typeof(string));
            //        dtAttendance.Columns.Add("EmpName", typeof(string));
            //        dtAttendance.Columns.Add("Day_Type", typeof(string));

            //        // Add a row with "Absent" status
            //        DataRow newRow = dtAttendance.NewRow();
            //        newRow["Empid"] = "";
            //        newRow["EmpName"] = "";
            //        newRow["Day_Type"] = "Absent";
            //        dtAttendance.Rows.Add(newRow);

            //        // Bind the DataTable to the GridView
            //        dataGridView1.DataSource = dtAttendance;
            //        // dataGridView1.DataBind();
            //    }
            //    else
            //    {
            //        // Attendance marked for today, bind the data to the GridView
            //        dataGridView1.DataSource = ds.Tables[0];
            //        dataGridView1.DataSource = dtAttendance;
            //    }
            //}
            BindCombo();
            panel2.Height = (65 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
        }

        private void BindCombo()
        {
            con.Close();
            DataRow dr;
            con.Open();
            SqlCommand cmd = new SqlCommand("select EmployeeID from Employeetable", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbid.Items.Add(dt.Rows[i][0].ToString().Trim());
            }
            dr = dt.NewRow();
            con.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbid.Text == "")
            {
                MessageBox.Show("Please Select Employee Id");
            }
            else
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Employeetable where EmployeeID ='" + cmbid.Text + "'", con);
                DataTable ds = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    txtname.Text = row["name"].ToString();
                    txtdesignation.Text = row["designation"].ToString();
                }
            }

            //   SqlDataAdapter sda1 = new SqlDataAdapter("select InTime from attendance where empid='"+cmbid.Text+"'", con);
            //   DataSet dt1 = new DataSet();
            //   sda1.Fill(dt1);
            ////   DateTime lastpresenty =

            //   SqlDataAdapter sda = new SqlDataAdapter("select * from attendance where empid='" + cmbid.Text + "' and empname = '" + txtname.Text + "' and intime = '" + intime1 + "' ", con);
            //   DataSet dt = new DataSet();
            //   sda.Fill(dt);
            //   if (dt.Tables[0].Rows.Count != 0)
            //   {
            //       btnsave.Visible = false;
            //       button1.Visible = true;
            //   }
            //   else
            //   {
            //       btnsave.Visible = true;
            //       button1.Visible = false;
            //   }
        }
        string Day_Type = string.Empty;

        private void UpdateOutTime()
        {

        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            string dt = DateTime.Now.ToString("M/dd/yyyy");
            string intime = DateTime.Now.ToString("hh:mm:ss tt");
            string Day_Type = "";

            // Check if present or absent radio button is checked
            if (rdpresent.Checked)
            {
                Day_Type = "Present";
            }
            else if (rdabsent.Checked)
            {
                Day_Type = "Absent";
            }
            if (!string.IsNullOrEmpty(Day_Type)) // Proceed only if Day_Type is set
            {
                // Check if employee is already marked for present/absent today
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Day_Type FROM Attendance WHERE Empid = @Empid AND EmpName = @EmpName AND date1 = @Date1", con);
                sda.SelectCommand.Parameters.AddWithValue("@Empid", cmbid.SelectedItem);
                sda.SelectCommand.Parameters.AddWithValue("@EmpName", txtname.Text);
                sda.SelectCommand.Parameters.AddWithValue("@Date1", dt);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    string existingDayType = ds.Tables[0].Rows[0]["Day_Type"].ToString();
                    if (existingDayType == Day_Type)
                    {
                        MessageBox.Show("This employee is already marked as " + Day_Type + " today.");
                    }
                    else
                    {
                        MessageBox.Show("This employee is already marked as " + existingDayType + " today.");
                    }
                }
                else
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Attendance VALUES (@Empid, @Empname, @designation, @InTime, @OutTime, @Day_Type, @Date1)", con);
                    cmd.Parameters.AddWithValue("@Empid", cmbid.Text);
                    cmd.Parameters.AddWithValue("@Empname", txtname.Text);
                    cmd.Parameters.AddWithValue("@designation", txtdesignation.Text);
                    cmd.Parameters.AddWithValue("@InTime", Day_Type == "Present" ? intime : ""); // Set InTime only if present
                    cmd.Parameters.AddWithValue("@OutTime", ""); // Assuming OutTime is empty initially
                    cmd.Parameters.AddWithValue("@Day_Type", Day_Type);
                    cmd.Parameters.AddWithValue("@Date1", dt);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(Day_Type.ToUpper() + " marked successfully.");
                    con.Close();
                }
            }
            //////string dt = DateTime.Now.ToString("M/dd/yyyy");
            //////string intime = DateTime.Now.ToString("hh:mm:ss tt");

            ////////DateTime selectedDateTime = dateTimePicker1.Value;
            ////////DateTime dateOnly = selectedDateTime.Date;
            ////////TimeSpan timeOnly = selectedDateTime.TimeOfDay;
            //////if (rdpresent.Checked)
            //////{
            //////    Day_Type = "Present";

            //////    SqlDataAdapter sda = new SqlDataAdapter("select Empid from Attendance where Empid='" + cmbid.SelectedItem + "' and EmpName='" + txtname.Text.ToString() + "'  and date1='" + dt + "'", con);
            //////    DataSet ds = new DataSet();
            //////    sda.Fill(ds);


            //////    if (ds.Tables[0].Rows.Count != 0)
            //////    {
            //////        MessageBox.Show("These Employee IS Present Today");
            //////        ds.Clear();
            //////    }
            //////    else
            //////    {
            //////        con.Close();
            //////        con.Open();
            //////        SqlCommand cmd = new SqlCommand("insert into Attendance values(@EmpID,@EmpName,@designation,@Intime,@OutTime,@Day_Type,@Date1)", con);
            //////        cmd.Parameters.AddWithValue("@Empid", cmbid.Text);
            //////        cmd.Parameters.AddWithValue("@Empname", txtname.Text);
            //////        cmd.Parameters.AddWithValue("@designation", txtdesignation.Text);
            //////        cmd.Parameters.AddWithValue("@InTime", intime.ToString());
            //////        cmd.Parameters.AddWithValue("@OutTime", "");
            //////        cmd.Parameters.AddWithValue("@Day_Type", Day_Type);
            //////        cmd.Parameters.AddWithValue("@Date1", dt);

            //////        cmd.ExecuteNonQuery();
            //////        MessageBox.Show("PRESENT");
            //////        con.Close();
            //////    }
            //////}
            //////else if (rdabsent.Checked)
            //////{
            //////    Day_Type = "Absent";

            //////    SqlDataAdapter sda = new SqlDataAdapter("select Empid from Attendance where Empid='" + cmbid.SelectedItem + "' and EmpName='" + txtname.Text.ToString() + "'  and date1='" + dt + "'", con);
            //////    DataSet ds = new DataSet();
            //////    sda.Fill(ds);

            //////    if (ds.Tables[0].Rows.Count != 0)
            //////    {
            //////        MessageBox.Show("These Employee is already Present Today");
            //////        ds.Clear();
            //////    }
            //////    else
            //////    {
            //////        con.Close();
            //////        con.Open();
            //////        SqlCommand cmd = new SqlCommand("insert into Attendance values(@Empid,@Empname,@designation,@InTime,@OutTime,@Day_Type,@Date1)", con);
            //////        cmd.Parameters.AddWithValue("@Empid", cmbid.Text);
            //////        cmd.Parameters.AddWithValue("@Empname", txtname.Text);
            //////        cmd.Parameters.AddWithValue("@designation", txtdesignation.Text);
            //////        cmd.Parameters.AddWithValue("@InTime", "");
            //////        cmd.Parameters.AddWithValue("@OutTime", "");
            //////        cmd.Parameters.AddWithValue("@Day_Type", Day_Type);
            //////        cmd.Parameters.AddWithValue("@Date1", dt);

            //////        cmd.ExecuteNonQuery();
            //////        MessageBox.Show("ABSENT");
            //////        con.Close();
            //////    }
            //////}
            datagridShow();
        }
        public void datagridShow()
        {
            try
            {
                con.Close();
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from Attendance ", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        //dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][0].ToString();
                        dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][1].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = ds.Tables[0].Rows[i][2].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = ds.Tables[0].Rows[i][3].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = ds.Tables[0].Rows[i][7].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = ds.Tables[0].Rows[i][4].ToString();
                        dataGridView1.Rows[i].Cells[5].Value = ds.Tables[0].Rows[i][5].ToString();
                        dataGridView1.Rows[i].Cells[6].Value = ds.Tables[0].Rows[i][6].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //string dt = DateTime.Now.ToString("M/dd/yyyy");
            //string outtime = DateTime.Now.ToString("hh:mm:ss tt");
            //bool isPresent = false;

            //// Check if the employee is marked present for today
            //SqlDataAdapter sda = new SqlDataAdapter("SELECT Day_Type FROM Attendance WHERE Empid = @Empid AND date1 = @Date1", con);
            //sda.SelectCommand.Parameters.AddWithValue("@Empid", cmbid.Text);
            //sda.SelectCommand.Parameters.AddWithValue("@Date1", dt);

            //DataSet ds = new DataSet();
            //sda.Fill(ds);

            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    string dayType = ds.Tables[0].Rows[0]["Day_Type"].ToString();
            //    if (dayType == "Present" || dayType.StartsWith("Present")) // Check for present or present(half day)
            //    {
            //        isPresent = true;
            //        MessageBox.Show("This employee is already marked as " + dayType + " today.");
            //    }
            //}

            //if (isPresent)
            //{
            //    DateTime halfDayThreshold = DateTime.Today.AddHours(13);
            //    bool isHalfDay = Convert.ToDateTime(outtime) < halfDayThreshold;

            //    con.Close();
            //    con.Open();
            //    if (isHalfDay)
            //    {
            //        SqlCommand cmd = new SqlCommand("UPDATE Attendance SET outtime = @OutTime, Day_Type = 'Present(Half Day)' WHERE empid = @Empid AND date1 = @Date1", con);
            //        cmd.Parameters.AddWithValue("@OutTime", outtime);
            //        cmd.Parameters.AddWithValue("@Empid", cmbid.Text);
            //        cmd.Parameters.AddWithValue("@Date1", dt);
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("HALF DAY");
            //    }
            //    else
            //    {
            //        SqlCommand cmd = new SqlCommand("UPDATE Attendance SET outtime = @OutTime WHERE empid = @Empid AND date1 = @Date1", con);
            //        cmd.Parameters.AddWithValue("@OutTime", outtime);
            //        cmd.Parameters.AddWithValue("@Empid", cmbid.Text);
            //        cmd.Parameters.AddWithValue("@Date1", dt);
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("FULL DAY");
            //    }
            //    MessageBox.Show("Update Success");
            //    con.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Employee is absent, no need to update");
            //}
            string dt = DateTime.Now.ToString("M/dd/yyyy");
            string outtime = DateTime.Now.ToString("hh:mm:ss tt");
            bool isPresent = false;

            // Check if the employee is marked present for today
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Day_Type, OutTime FROM Attendance WHERE Empid = @Empid AND date1 = @Date1", con);
            sda.SelectCommand.Parameters.AddWithValue("@Empid", cmbid.Text);
            sda.SelectCommand.Parameters.AddWithValue("@Date1", dt);

            DataSet ds = new DataSet();
            sda.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                string dayType = ds.Tables[0].Rows[0]["Day_Type"].ToString();
                if (dayType.StartsWith("Present")) // Check for present or present(half day)
                {
                    isPresent = true;
                    MessageBox.Show("This employee is already marked as " + dayType + " today.");
                }

                // Check if outtime is already set, if yes, don't update it again
                string existingOutTime = ds.Tables[0].Rows[0]["OutTime"].ToString();
                if (!string.IsNullOrEmpty(existingOutTime))
                {
                    // Exit the function to avoid updating outtime again
                    MessageBox.Show("Out time already marked for today.");
                    return;
                }
            }

            if (isPresent)
            {
                DateTime halfDayThreshold = DateTime.Today.AddHours(13);
                bool isHalfDay = Convert.ToDateTime(outtime) < halfDayThreshold;

                con.Close();
                con.Open();
                if (isHalfDay)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Attendance SET outtime = @OutTime, Day_Type = 'Present(Half Day)' WHERE empid = @Empid AND date1 = @Date1", con);
                    cmd.Parameters.AddWithValue("@OutTime", outtime);
                    cmd.Parameters.AddWithValue("@Empid", cmbid.Text);
                    cmd.Parameters.AddWithValue("@Date1", dt);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("HALF DAY");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Attendance SET outtime = @OutTime WHERE empid = @Empid AND date1 = @Date1", con);
                    cmd.Parameters.AddWithValue("@OutTime", outtime);
                    cmd.Parameters.AddWithValue("@Empid", cmbid.Text);
                    cmd.Parameters.AddWithValue("@Date1", dt);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("FULL DAY");
                }
                MessageBox.Show("Update Success");
                con.Close();
            }
            else
            {
                MessageBox.Show("Employee is absent, no need to update");
            }
            datagridShow();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtdesignation_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
