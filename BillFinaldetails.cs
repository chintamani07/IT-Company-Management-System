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
    public partial class BillFinaldetails : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public BillFinaldetails()
        {
            InitializeComponent();
            //  datagridShow();

        }

        private void BillFinaldetails_Load(object sender, EventArgs e)
        {
            panel2.Height = (80 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            datagridShow();

        }
        private DataTable FilterDataBySelectedMonth()
        {
            DataTable dataTable = (DataTable)dataGridView1.DataSource;
            DateTime selectedMonth = dateTimePicker1.Value;

            // Filter data by selected month
            DataTable filteredData = dataTable.Clone(); // Create a clone with the same schema
            foreach (DataRow row in dataTable.Rows)
            {
                DateTime rowDate = Convert.ToDateTime(row["DateColumn"]); // Replace "DateColumn" with the actual column name
                if (rowDate.Year == selectedMonth.Year && rowDate.Month == selectedMonth.Month)
                {
                    filteredData.ImportRow(row);
                }
            }

            return filteredData;
        }
        public void datagridShow()
        {
            try
            {
                con.Close();
                con.Open();

                DateTime selectedDate = dateTimePicker1.Value;
                int selectedMonth = selectedDate.Month;
                int selectedYear = selectedDate.Year;

                // Construct the SQL query to select data for the selected month
                string query = "SELECT * FROM BillTable WHERE MONTH(date) = @SelectedMonth AND YEAR(date) = @SelectedYear";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@SelectedMonth", selectedMonth);
                da.SelectCommand.Parameters.AddWithValue("@SelectedYear", selectedYear);

                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.Rows.Clear();
                if (ds.Tables[0].Rows.Count != 0)
                {
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            datagridShow();
        }
    }
}
