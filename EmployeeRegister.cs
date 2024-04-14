using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace CompanyManagementSystemFinal
{
    public partial class EmployeeRegister : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public EmployeeRegister()
        {
            InitializeComponent();
            cnt = 0;
            SrNoAUTO();
            BindCombo();
            datagridShow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BindCombo()
        {
            DataRow dr;
            con.Open();
            SqlCommand cmd = new SqlCommand("select designation from [designation]", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbdesignation.Items.Add(dt.Rows[i][0].ToString().Trim());
            }
            dr = dt.NewRow();
            con.Close();
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
                SqlDataAdapter sda = new SqlDataAdapter("Select max(Employeeid) From EmployeeTable ", con);
                sda.Fill(ds);
                if (ds.Tables[0].Rows[0][0].ToString() != "" && ds.Tables[0].Rows[0][0].ToString() != null)
                {
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private DateTime previousDateTimeValue;

        private void EmployeeRegister_Load(object sender, EventArgs e)
        {

            previousDateTimeValue = dateTimePicker1.Value;
            panel2.Height = (50 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            cmbgender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbgender.Items.Add("Female");
            cmbgender.Items.Add("Male");

            if (ViewEmployess.selected == true)
            {
                txtid.Text = ViewEmployess.id;
                txtname.Text = ViewEmployess.empame;
                txtaddress.Text = ViewEmployess.address;
                cmbgender.Text = ViewEmployess.gender;
                dateTimePicker1.Text = ViewEmployess.dob;
                cmbdesignation.Text = ViewEmployess.design;
                txtbasesalary.Text = ViewEmployess.bsalary;
                txtincrement.Text = ViewEmployess.salaryincre;
            }

        }

        private void comboBox3_Click(object sender, EventArgs e)
        {

        }
        string filename;
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Image files( *.Jpg; *.png; *.Gif)|*.Jpg; *.png; *.Gif";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    Image Image = Image.FromFile(opf.FileName);
                    filename = opf.FileName;
                    pictureBox1.Image = Image.FromFile(opf.FileName);
                    //    pictureBox1.Image = Image.GetThumbnailImage(100, 100, () => false, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            DateTime joindate = DateTime.Now;
            DateTime leavedate = DateTime.Now;

            con.Close();
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Employeetable where employeeid = '" + txtid.Text + "'", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int i = ds.Tables[0].Rows.Count;
            if (i > 0)
            {
                MessageBox.Show("These Id is Already Exists");
                ds.Clear();
            }
            else if (txtid.Text.Trim() == "" || txtname.Text.Trim() == "" || txtaddress.Text.Trim() == "" || cmbgender.Text.Trim() == "" || dateTimePicker1.Text.Trim() == "" || cmbdesignation.Text.Trim() == "" || txtbasesalary.Text.Trim() == "" || txtincrement.Text.Trim() == "" || pictureBox1.Image == null)
            {
                MessageBox.Show("Please fill Missing Values");
            }
            else
            {
                MemoryStream ms;
                ms = new MemoryStream();
                byte[] photo_aray = File.ReadAllBytes(filename);
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);

                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Employeetable(Employeeid,name,address,gender,dob,designation,basicsalary,salaryincrement,joindate,leavedate,cv)values(@Employeeid,@name,@address,@gender,@dob,@designation,@basicsalary,@salaryincrement,@joindate,@leavedate,@cv)", con);
                cmd.Parameters.AddWithValue("@Employeeid", txtid.Text);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@gender", cmbgender.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@designation", cmbdesignation.Text);
                cmd.Parameters.AddWithValue("@basicsalary", txtbasesalary.Text);
                cmd.Parameters.AddWithValue("@salaryincrement", txtincrement.Text);
                //cmd.Parameters.AddWithValue("@username", txtuser.Text);
                //cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                cmd.Parameters.AddWithValue("@joindate", joindate);
                cmd.Parameters.AddWithValue("@leavedate", "");
                cmd.Parameters.AddWithValue("@CV", SqlDbType.Binary).Value = photo_aray;
                cmd.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show("Data Inserted Successfully");
                MessageBox.Show("Successfully Added ", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            datagridShow();
            SrNoAUTO();
        }

        private void btnshow_Click(object sender, EventArgs e)
        {

        }
        public void datagridShow()
        {
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

        }

        private void btnupdate_Click_1(object sender, EventArgs e)
        {
            string joindate = DateTime.Now.ToString();
            string leavedate = DateTime.Now.ToString();
            if (txtid.Text.Trim() == "" || txtname.Text.Trim() == "" || txtaddress.Text.Trim() == "" || cmbgender.Text.Trim() == "" || dateTimePicker1.Text.Trim() == "" || cmbdesignation.Text.Trim() == "" || txtbasesalary.Text.Trim() == "" || txtincrement.Text.Trim() == "" || pictureBox1.Image == null)
            {
                MessageBox.Show("Please fill Missing Values");
            }
            else
            {
                MemoryStream ms = new MemoryStream();

                if (filename != null)
                {
                    byte[] photo_aray = File.ReadAllBytes(filename);
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Employeetable set name = @name,address = @address,gender = @gender,dob = @dob,designation= @designation,basicsalary=basicsalary,salaryincrement=@salaryincrement, joindate=@joindate, leavedate=@leavedate, cv=@cv where Employeeid='" + txtid.Text + "'", con);
                    cmd.Parameters.AddWithValue("@Employeeid", txtid.Text);
                    cmd.Parameters.AddWithValue("@name", txtname.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbgender.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@designation", cmbdesignation.Text);
                    cmd.Parameters.AddWithValue("@basicsalary", txtbasesalary.Text);
                    cmd.Parameters.AddWithValue("@salaryincrement", txtincrement.Text);
                    //cmd.Parameters.AddWithValue("@username", txtuser.Text);
                    //cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@joindate", joindate);
                    cmd.Parameters.AddWithValue("@leavedate", "");
                    cmd.Parameters.AddWithValue("@CV", SqlDbType.Binary).Value = photo_aray;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //  MessageBox.Show("Data Updated Successfully");
                    MessageBox.Show("Successfully Updated ", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (filename == null)
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Employeetable set name = @name,address = @address,gender = @gender,dob = @dob,designation= @designation,basicsalary=basicsalary,salaryincrement=@salaryincrement, joindate=@joindate, leavedate=@leavedate where Employeeid='" + txtid.Text + "'", con);
                    cmd.Parameters.AddWithValue("@Employeeid", txtid.Text);
                    cmd.Parameters.AddWithValue("@name", txtname.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbgender.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@designation", cmbdesignation.Text);
                    cmd.Parameters.AddWithValue("@basicsalary", txtbasesalary.Text);
                    cmd.Parameters.AddWithValue("@salaryincrement", txtincrement.Text);
                    //cmd.Parameters.AddWithValue("@username", txtuser.Text);
                    //cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@joindate", joindate);
                    cmd.Parameters.AddWithValue("@leavedate", "");

                    cmd.ExecuteNonQuery();
                    con.Close();
                    //MessageBox.Show("Data Updated Successfully");
                    MessageBox.Show("Successfully Updated ", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            datagridShow();
            SrNoAUTO();

        }
        int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtid.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtaddress.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmbgender.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                cmbdesignation.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtbasesalary.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtincrement.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                pictureBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            catch { }
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Close();
                con.Open();
                string query1 = "Select cv from Employeetable where Name = '" + txtname.Text + "'  ";
                SqlCommand cmd = new SqlCommand(query1, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                {
                    if (reader.HasRows)
                    {
                        byte[] Photo = ((byte[])reader[0]);
                        if (Photo == null)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(Photo);
                            pictureBox1.Image = Image.FromStream(ms);
                            button1.Visible = true;
                        }
                    }
                    else
                    {
                        con.Close();
                    }
                }
            }
            catch { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from EmployeeTable Where Employeeid = '" + txtid.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Deleted Successfully");
            con.Close();
            datagridShow();
            SrNoAUTO();
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

        private void txtbasesalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
           && !char.IsDigit(e.KeyChar)
          )
            {
                e.Handled = true;
                MessageBox.Show("Enter only Numbers");
            }
        }

        private void txtincrement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
           && !char.IsDigit(e.KeyChar)
          )
            {
                e.Handled = true;
                MessageBox.Show("Enter only Numbers");
            }
        }
        private void txtbasesalary_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
           // txtid.Text = "";
            ViewEmployess.selected = false;
            SrNoAUTO();
            txtname.Text = "";
            txtaddress.Text = "";
            txtbasesalary.Text = "";
            txtincrement.Text = "";
            pictureBox1.Image = null;
            dateTimePicker1.Text = null;
            cmbdesignation.Text = "";
            cmbgender.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string joindate = DateTime.Now.ToString();
            string leavedate = DateTime.Now.ToString();
            if (txtid.Text.Trim() == "" || txtname.Text.Trim() == "" || txtaddress.Text.Trim() == "" || cmbgender.Text.Trim() == "" || dateTimePicker1.Text.Trim() == "" || cmbdesignation.Text.Trim() == "" || txtbasesalary.Text.Trim() == "" || txtincrement.Text.Trim() == "" || pictureBox1.Image == null)
            {
                MessageBox.Show("Please fill Missing Values");
            }
            else
            {
                MemoryStream ms = new MemoryStream();

                if (filename != null)
                {
                    byte[] photo_aray = File.ReadAllBytes(filename);
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Employeetable set name = @name,address = @address,gender = @gender,dob = @dob,designation= @designation,basicsalary=basicsalary,salaryincrement=@salaryincrement, joindate=@joindate, leavedate=@leavedate, cv=@cv where Employeeid='" + txtid.Text + "'", con);
                    cmd.Parameters.AddWithValue("@Employeeid", txtid.Text);
                    cmd.Parameters.AddWithValue("@name", txtname.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbgender.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@designation", cmbdesignation.Text);
                    cmd.Parameters.AddWithValue("@basicsalary", txtbasesalary.Text);
                    cmd.Parameters.AddWithValue("@salaryincrement", txtincrement.Text);
                    //cmd.Parameters.AddWithValue("@username", txtuser.Text);
                    //cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@joindate", joindate);
                    cmd.Parameters.AddWithValue("@leavedate", leavedate);
                    cmd.Parameters.AddWithValue("@CV", SqlDbType.Binary).Value = photo_aray;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //  MessageBox.Show("Data Updated Successfully");
                    MessageBox.Show("Successfully Updated ", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (filename == null)
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Employeetable set name = @name,address = @address,gender = @gender,dob = @dob,designation= @designation,basicsalary=basicsalary,salaryincrement=@salaryincrement, joindate=@joindate, leavedate=@leavedate where Employeeid='" + txtid.Text + "'", con);
                    cmd.Parameters.AddWithValue("@Employeeid", txtid.Text);
                    cmd.Parameters.AddWithValue("@name", txtname.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbgender.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@designation", cmbdesignation.Text);
                    cmd.Parameters.AddWithValue("@basicsalary", txtbasesalary.Text);
                    cmd.Parameters.AddWithValue("@salaryincrement", txtincrement.Text);
                    //cmd.Parameters.AddWithValue("@username", txtuser.Text);
                    //cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@joindate", joindate);
                    cmd.Parameters.AddWithValue("@leavedate", leavedate);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    //MessageBox.Show("Data Updated Successfully");
                    MessageBox.Show("Successfully Updated ", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            datagridShow();
            SrNoAUTO();
        }
        int cnt = 0;
        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            DateTime from = dateTimePicker1.Value;
            DateTime to = DateTime.Now;
            TimeSpan TSpan = to - from;
            double days = TSpan.TotalDays;

            int diffinyr = to.Year - from.Year;
            if (diffinyr >= 18 && cnt > 0)
            {
                MessageBox.Show(diffinyr + " Age");
            }
            else if (cnt > 0)
            {
                MessageBox.Show("Age should be greater than 18");
            }
            else
            {
                cnt++;
            }


        }
    }
}
