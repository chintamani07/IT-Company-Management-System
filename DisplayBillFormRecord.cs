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
    public partial class DisplayBillFormRecord : Form
    {
      //  SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

      SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
        public static string exname { get; set; }
        public static string pricetotal { get; set; }
        DataTable dt2 = new DataTable();
        public static DateTime currentDate;
        public DisplayBillFormRecord()
        {
            InitializeComponent();
            //datagridShow();  
            //dataGridView1.DataSource = dataSource;
            //DataTable dt = dataSource;
            gridload();
        }
        public void gridload()
        {
            List<string> name = new List<string>();
            List<string> price = new List<string>();
            DataTable dt11 = new DataTable();
            DataTable dt = Dailyexpanses.dt1;
            DataTable dt2 = dt;

            List<string> name1 = new List<string>();
            List<string> price1 = new List<string>();
            //      Get the current date
            currentDate = DateTime.Parse(dt.Rows[0][1].ToString());

            // Get the first day of the current month
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Get the last day of the current month
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Get the last day of the last month
            DateTime lastDayOfLastMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1);

            // Get the first day of the last month
            DateTime firstDayOfLastMonth = lastDayOfLastMonth.AddDays(1 - lastDayOfLastMonth.Day);

            SqlDataAdapter sda = new SqlDataAdapter("WITH CTE AS ( SELECT *,ROW_NUMBER() OVER (PARTITION BY name ORDER BY id desc) AS RowNum FROM Billtable WHERE paiddate >= '" + firstDayOfLastMonth + "' AND paiddate <= '" + lastDayOfLastMonth + "' AND remaining != 0) SELECT name, remaining FROM CTE WHERE RowNum = 1", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                name.Add(ds.Tables[0].Rows[i][0].ToString());
                price.Add(ds.Tables[0].Rows[i][1].ToString());
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sda = new SqlDataAdapter("select Itemname ,Price from Itemtable where did = '" + dt.Rows[i][0].ToString() + "'", con);
                ds = new DataSet();
                sda.Fill(ds);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (name.Contains(ds.Tables[0].Rows[j][0].ToString()))
                    {
                        int index = name.IndexOf(ds.Tables[0].Rows[j][0].ToString());
                        price[index] = (int.Parse(price[index]) + int.Parse(ds.Tables[0].Rows[j][1].ToString())).ToString();
                    }
                    else if (!name.Contains(ds.Tables[0].Rows[j][0].ToString()))
                    {
                        name.Add(ds.Tables[0].Rows[j][0].ToString());
                        price.Add(ds.Tables[0].Rows[j][1].ToString());
                    }
                }
                SqlDataAdapter sda1 = new SqlDataAdapter("select Itemname, Price from Item2table where did = '" + dt.Rows[i][0].ToString() + "'", con);
                DataSet ds1 = new DataSet();
                sda1.Fill(ds1);
                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    if (name.Contains(ds1.Tables[0].Rows[j][0].ToString()))
                    {
                        int index = name.IndexOf(ds1.Tables[0].Rows[j][0].ToString());
                        price[index] = (int.Parse(price[index]) + int.Parse(ds1.Tables[0].Rows[j][1].ToString())).ToString();
                    }
                    else if (!name.Contains(ds1.Tables[0].Rows[j][0].ToString()))
                    {
                        name.Add(ds1.Tables[0].Rows[j][0].ToString());
                        price.Add(ds1.Tables[0].Rows[j][1].ToString());
                    }
                }
            }
            sda = new SqlDataAdapter("SELECT name, remaining FROM Billtable WHERE paiddate >= '" + firstDayOfMonth + "' AND paiddate <= '" + lastDayOfMonth + "' AND remaining != 0 AND given != 0 ORDER BY id DESC", con);
            ds = new DataSet();
            sda.Fill(ds);

            // Check if there are any rows in the dataset
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Retrieve the first row (which is the latest entry)
                DataRow latestRow = ds.Tables[0].Rows[0];
                string itemName = latestRow[0].ToString();
                int remainingAmount = int.Parse(latestRow[1].ToString());

                // Update the price list with the latest remaining amount
                if (name.Contains(itemName))
                {
                    int index = name.IndexOf(itemName);
                    price[index] = remainingAmount.ToString();
                }
            }
            // Create a DataTable and populate it with the updated remaining amounts
            DataTable dt111 = new DataTable();
            dt11.Columns.Add("Name");
            dt11.Columns.Add("Price");

            for (int k = 0; k < name.Count; k++)
            {
                dt11.Rows.Add(name[k], price[k]);
            }

            // Set the DataGridView DataSource
            dataGridView1.DataSource = dt11;

           
            //new code
            //sda = new SqlDataAdapter("WITH CTE AS ( SELECT *,ROW_NUMBER() OVER (PARTITION BY name ORDER BY id desc) AS RowNum FROM Billtable WHERE paiddate >= '" + firstDayOfMonth + "' AND paiddate <= '" + lastDayOfMonth + "' AND remaining != 0) SELECT name, remaining FROM CTE WHERE RowNum = 1", con);
            //ds = new DataSet();
            //sda.Fill(ds);
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{

            //    if (name.Contains(ds.Tables[0].Rows[i][0].ToString()))
            //    {
            //        int index = name.IndexOf(ds.Tables[0].Rows[i][0].ToString());
            //        price[index] = (int.Parse(price[index]) + int.Parse(ds.Tables[0].Rows[i][1].ToString())).ToString();
            //    }
            //    else if (!name.Contains(ds.Tables[0].Rows[i][0].ToString()))
            //    {
            //        name.Add(ds.Tables[0].Rows[i][0].ToString());
            //        price.Add(ds.Tables[0].Rows[i][1].ToString());
            //    }
            //}
            //int count = 0;
            ////sda = new SqlDataAdapter("WITH CTE AS ( SELECT *,ROW_NUMBER() OVER (PARTITION BY name ORDER BY id desc) AS RowNum FROM Billtable WHERE paiddate >= '" + firstDayOfMonth + "' AND paiddate <= '" + lastDayOfMonth + "' AND remaining = 0) SELECT name, remaining FROM CTE WHERE RowNum = 1", con);
            //sda = new SqlDataAdapter("select name, remaining from Billtable WHERE paiddate >= '" + firstDayOfMonth + "' AND paiddate <= '" + lastDayOfMonth + "' ", con);
            //ds = new DataSet();
            //sda.Fill(ds);
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (name.Contains(ds.Tables[0].Rows[i][0].ToString()))
            //    {
            //        if (name.Contains(ds.Tables[0].Rows[i][0].ToString()) && count > 1)
            //        {
            //            int index = name.IndexOf(ds.Tables[0].Rows[i][0].ToString());
            //            price[index] = ds.Tables[0].Rows[i][1].ToString();
            //        }
            //        else
            //        {
            //            count++;
            //        }
            //        //int index = name.IndexOf(ds.Tables[0].Rows[i][0].ToString());
            //        //price[index] = ds.Tables[0].Rows[i][1].ToString();
            //    }
            //}




            //            // Get the current date
            //            currentDate = DateTime.Parse(dt.Rows[0][1].ToString());

            //            // Get the first day of the current month
            //            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            //            // Get the last day of the current month
            //            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            //            SqlDataAdapter sda11 = new SqlDataAdapter("Select * from Billtable where date >= '" + firstDayOfMonth + "' and date <='" + lastDayOfMonth + "'", con);
            //            DataSet ds11 = new DataSet();
            //            sda11.Fill(ds11);
            //            for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
            //            {
            //                if (name.Contains(ds11.Tables[0].Rows[i][1]))
            //                {
            //                    int index = name.IndexOf(ds11.Tables[0].Rows[i][1].ToString());
            //                    price[index] = (double.Parse(price[index]) + double.Parse(ds11.Tables[0].Rows[i][3].ToString())).ToString(); 
            //                }
            //            }
            //            // Calculate total remaining amount from last month
            //            double lastMonthRemainingAmount = 0.0;
            //            DateTime lastMonthFirstDay = firstDayOfMonth.AddMonths(-1);
            //            DateTime lastMonthLastDay = firstDayOfMonth.AddDays(-1);

            //            //SqlDataAdapter sda0 = new SqlDataAdapter("Select * from Billtable where date >= '" + lastMonthFirstDay + "' and date <='" + lastMonthLastDay + "' and remaining != 0", con);
            //            //DataSet ds111 = new DataSet();
            //            //sda0.Fill(ds111);
            //            SqlDataAdapter sda0 = new SqlDataAdapter(@"
            //    WITH CTE AS (
            //        SELECT *,
            //               ROW_NUMBER() OVER (PARTITION BY name ORDER BY date DESC) AS RowNum
            //        FROM Billtable
            //        WHERE date >= '" + lastMonthFirstDay + @"' AND date <= '" + lastMonthLastDay + @"' AND remaining != 0
            //    )
            //    SELECT name, remaining 
            //    FROM CTE
            //    WHERE RowNum = 1", con);
            //            DataSet ds111 = new DataSet();
            //            sda0.Fill(ds111);

            //            // Calculate total remaining amount from last month
            //            for (int k = 0; k < ds111.Tables[0].Rows.Count; k++)
            //            {
            //                string name2 = ds111.Tables[0].Rows[k]["name"].ToString();
            //                string remaining = ds111.Tables[0].Rows[k]["remaining"].ToString();
            //                lastMonthRemainingAmount += double.Parse(remaining);
            //            }
            //            // Now update the current month's remaining amounts
            //            for (int k = 0; k < name.Count; k++)
            //            {
            //                double currentPrice = double.Parse(price[k]);
            //                currentPrice += lastMonthRemainingAmount;
            //                price[k] = currentPrice.ToString();
            //            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void DisplayBillFormRecord_Load(object sender, EventArgs e)
        {
            panel2.Height = (80 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            //gridload();
        }
        public void LoadDataIntoDataGridView(DataTable data)
        {
            try
            {
                dataGridView1.DataSource = data; // Assuming dataGridView2 is the DataGridView on OtherForm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data into DataGridView: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            //Dailyexpanses de = new Dailyexpanses();
            //de.ShowDialog();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Access the selected row's cells
                    exname = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                    pricetotal = dataGridView1.SelectedRows[0].Cells["Price"].Value.ToString();
                    Billform b1 = new Billform();
                    b1.Show();
                }
                else
                {
                    // Display a message or handle the case where no row is selected
                    MessageBox.Show("Please select a row.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //this.Refresh();
            //DisplayBillFormRecord dbf = new DisplayBillFormRecord();
            //dbf.ShowDialog();
            gridload();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
