using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompanyManagementSystemFinal
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        public static string designa;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString("dd/MM/yyyy,(dddd)");
            label7.Text = DateTime.Now.ToString("hh:mm:ss");
        }
        //EmployeeRegister ob1 =new EmployeeRegister();
        AttendanceForm ob2=new AttendanceForm();
        DesignationForm ob3= new DesignationForm();
        SalaryForm ob4 = new SalaryForm();
        //Client ob5 = new Client();
        Dailyexpanses ob6 = new Dailyexpanses();
        Employeebackrecords ob7= new Employeebackrecords();
        ViewEmployess ob8 = new ViewEmployess();
        SalaryTypeReport ob9 = new SalaryTypeReport();
        ViewClient ob10 = new ViewClient();
        BillFinaldetails ob11 = new BillFinaldetails();
        //Home ob11 = new Home();

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            EmployeeRegister objForm1 = new EmployeeRegister();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();      

            //panel4.Controls.Clear();
            //ob1.Show();
            //ob1.TopLevel = false;
            //panel4.Controls.Add(ob1);
            //ob1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob1.Dock = DockStyle.Fill;
            //ob1.Show();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob7.Hide();
            //ob8.Hide();
            //ob9.Hide();
            //ob11.Hide();
            //ob10.Hide();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            timer1.Interval = 50;
            timer2.Interval = 50;
            label3.Text = Form1.UserName;
            if (Form1.UserName == "admin")
            {
                designationToolStripMenuItem.Visible = true;
                //button5.Visible = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            DesignationForm objForm1 = new DesignationForm();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();
            //panel4.Controls.Clear();
            //ob3.Show();
            //ob3.TopLevel = false;
            //panel4.Controls.Add(ob3);
            //ob3.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob3.Dock = DockStyle.Fill;
            //ob1.Hide();
            //ob2.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob7.Hide();
            //ob8.Hide();
            //ob9.Hide();
            //ob10.Hide();
            //ob11.Hide();

        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
            label3.Text = "Logout";
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
            label3.Text = Form1.UserName;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("Do You Want to Exit", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //if (result == DialogResult.Yes)
            //{
            //    Form1 f1 = new Form1();
            //    f1.Show();
            //}
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            AttendanceForm objForm1 = new AttendanceForm();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();  
            //panel4.Controls.Clear();
            //ob2.Show();
            //ob2.TopLevel = false;
            //panel4.Controls.Add(ob2);
            //ob2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob2.Dock = DockStyle.Fill;
            //ob1.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob7.Hide();
            //ob8.Hide();
            //ob9.Hide();
            //ob10.Hide();
            //ob11.Hide();

        }

        private void designationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void salaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            SalaryForm objForm1 = new SalaryForm();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();
            //panel4.Controls.Clear();
            //ob4.Show();
            //ob4.TopLevel = false;
            //panel4.Controls.Add(ob4);
            //ob4.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob4.Dock = DockStyle.Fill;
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob7.Hide();
            //ob8.Hide();
            //ob9.Hide();
            //ob10.Hide();
            //ob11.Hide();

        }

        private void dailyExpensiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            Client objForm2 = new Client();
            this.IsMdiContainer = true;
            objForm2.TopLevel = false;
            panel4.Controls.Add(objForm2);
            objForm2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm2.Dock = DockStyle.Fill;
            objForm2.Show();  

            //panel4.Controls.Clear();
            //ob5.Show();
            //ob5.TopLevel = false;
            //panel4.Controls.Add(ob5);
            //ob5.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob5.Dock = DockStyle.Fill;
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob6.Hide();
            //ob7.Hide();
            //ob8.Hide();
            //ob9.Hide();
            //ob10.Hide();
            //ob11.Hide();

        }

        private void employeeManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            ViewEmployess objForm1 = new ViewEmployess();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();
            //ob8.Show();
            //ob8.TopLevel = false;
            //panel4.Controls.Add(ob8);
            //ob8.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob8.Dock = DockStyle.Fill;
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob7.Hide();
            //ob9.Hide();
            //ob10.Hide();
            //ob11.Hide();

        }

        private void employeeBackRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ob7.TopLevel = false;
            //panel4.Controls.Add(ob7);
            //ob7.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob7.Dock = DockStyle.Fill;
            //ob7.Show();
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob8.Hide();
            //ob9.Hide();
            //ob10.Hide();
        }

        private void salaryReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            SalaryTypeReport objForm1 = new SalaryTypeReport();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();
            //ob9.TopLevel = false;
            //panel4.Controls.Add(ob9);
            //ob9.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob9.Dock = DockStyle.Fill;
            //ob9.Show();
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob8.Hide();
            //ob7.Hide();
            //ob10.Hide();
            //ob11.Hide();

        }

        private void designationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 currentForm = new Form1();
            currentForm.Show();
        }

        private void viewClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            ViewClient objForm1 = new ViewClient();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();
            //ob10.TopLevel = false;
            //panel4.Controls.Add(ob10);
            //ob10.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob10.Dock = DockStyle.Fill;
            //ob10.Show();
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob8.Hide();
            //ob7.Hide();
            //ob9.Hide();
            //ob11.Hide();

        }

        private void manageEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void expansesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            Dailyexpanses objForm1 = new Dailyexpanses();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();
            //ob6.Show();
            //ob6.TopLevel = false;
            //panel4.Controls.Add(ob6);
            //ob6.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob6.Dock = DockStyle.Fill;
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob7.Hide();
            //ob8.Hide();
            //ob9.Hide();
            //ob10.Hide();
            //ob11.Hide();
        }

        private void expansesBillDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            BillFinaldetails objForm1 = new BillFinaldetails();
            this.IsMdiContainer = true;
            objForm1.TopLevel = false;
            panel4.Controls.Add(objForm1);
            objForm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm1.Dock = DockStyle.Fill;
            objForm1.Show();
            //ob11.TopLevel = false;
            //panel4.Controls.Add(ob11);
            //ob11.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //ob11.Dock = DockStyle.Fill;
            //ob11.Show();
            //ob1.Hide();
            //ob2.Hide();
            //ob3.Hide();
            //ob4.Hide();
            //ob5.Hide();
            //ob6.Hide();
            //ob8.Hide();
            //ob7.Hide();
            //ob9.Hide();
            //ob10.Hide();
        }
    }
}
