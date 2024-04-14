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
    public partial class DesignationForm : Form
    {
       // SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

       SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        public DesignationForm()
        {
            InitializeComponent();
            clear();
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            con.Open();
            // Check if the textbox is empty
            if (string.IsNullOrWhiteSpace(txtdesignation.Text))
            {
                MessageBox.Show("Please enter a designation.", "Empty Designation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Check if the designation already exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM designation WHERE designation = @designation", con);
                checkCmd.Parameters.AddWithValue("@designation", txtdesignation.Text);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    // Designation already exists, show a message
                    MessageBox.Show("Designation already exists.", "Duplicate Designation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Designation does not exist, insert it into the database
                    SqlCommand insertCmd = new SqlCommand("INSERT INTO designation (designation) VALUES (@designation)", con);
                    insertCmd.Parameters.AddWithValue("@designation", txtdesignation.Text);
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Designation inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            con.Close();
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            Home h1 = new Home();
            h1.ShowDialog();
        }
        private void clear()
        {
            txtdesignation.Text = "";
        }
        private void DesignationForm_Load(object sender, EventArgs e)
        {
        }
    }
}
