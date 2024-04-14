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
    public partial class Dailyexpanses : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\CompanyManagementSystemFinal\CompanyManagementSystemFinal\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");
       // SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + "\\CompanyDatabase.mdf;Integrated Security=True;User Instance=True");

        public Dailyexpanses()
        {
            InitializeComponent();
           // datagridShow();
        }

        private void Dailyexpanses_Load(object sender, EventArgs e)
        {
            panel2.Height = (50 * this.Height) / 100;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Palatino Linotype", 12.75F, FontStyle.Bold);
            datagridShow();
            //dataTable.Columns.Add("Date", typeof(DateTime));
            //dataTable.Columns.Add("UtilityExpenses", typeof(decimal));
            //dataTable.Columns.Add("OfficeExpenses", typeof(decimal));
            //dataTable.Columns.Add("TotalExpenses", typeof(decimal));
            //dataGridView1.DataSource = dataTable;
        }

        private void Price_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Price.SelectedIndex != -1)
            {
                textBox1.Text = Price.SelectedItem.ToString();
            }
            //double prices = 0;
            //for (int i = 0; i < Price.Items.Count; i++)
            //{
            //    prices = prices + Convert.ToDouble(Price.Items[i]);
            //}
            //textBox6.Text = prices.ToString();
        }

        public void DailyCalculate()
        {
            double prices = 0, price2 = 0;

            for (int i = 0; i < Price.Items.Count; i++)
            {
                try
                {
                    prices = prices + Convert.ToDouble(Price.Items[i]);
                }
                catch { }
            }
            textBox6.Text = prices.ToString();
           
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                try
                {
                    price2 = price2 + Convert.ToDouble(listBox3.Items[i]);
                }
                catch { }
            }
            textBox5.Text = price2.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!listBox1.Items.Contains(txtname.Text))
            {
                if (txtname.Text == "")
                {
                    MessageBox.Show("Please fill Values");
                }
                else
                {
                    string name = txtname.Text;
                    string price = textBox1.Text;

                    listBox1.Items.Add(name);
                    Price.Items.Add(price);
                    DailyCalculate();
                    txtname.Clear();
                    textBox1.Clear();
                }
            }
            else
            {
                int index = listBox1.Items.IndexOf(txtname.Text);
                string price = textBox1.Text;
                Price.Items[index] = price;
                DailyCalculate();
                txtname.Clear();
                textBox1.Clear();
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                textBox2.Text = listBox3.SelectedItem.ToString();
            }
            //double prices = 0;
            //for (int i = 0; i < listBox3.Items.Count; i++)
            //{
            //    prices = prices + Convert.ToDouble(listBox3.Items[i]);
            //}
            //textBox5.Text = prices.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!listBox2.Items.Contains(textBox3.Text))
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Please fill Values");
                }
                else
                {
                    string name = textBox3.Text;
                    string price = textBox2.Text;

                    listBox2.Items.Add(name);
                    listBox3.Items.Add(price);
                    DailyCalculate();
                    textBox3.Clear();
                    textBox2.Clear();
                }
            }
            else
            {
                int index = listBox2.Items.IndexOf(textBox3.Text);
                string price = textBox2.Text;
                listBox3.Items[index] = price;
                DailyCalculate();
                textBox3.Clear();
                textBox2.Clear();
            }
        }
        DataTable dataTable = new DataTable();
        private void btncalculate_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0 && Price.Items.Count != 0 || listBox2.Items.Count != 0 && listBox3.Items.Count != 0)
            {
                double ut = double.Parse(textBox5.Text);
                double oe = double.Parse(textBox6.Text);
                double ttl = ut + oe;

                DateTime currentDate = DateTime.Now;
                // Get the first day of the current month
                DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                // Get the last day of the current month
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                // Get the last day of the last month
                DateTime lastDayOfLastMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1);
                // Get the first day of the last month
                DateTime firstDayOfLastMonth = lastDayOfLastMonth.AddDays(1 - lastDayOfLastMonth.Day);
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    if (textBox5.Text != "" && textBox6.Text != "")
                    {
                        //dataTable.Columns.Add("Id", typeof(int));
                        //dataTable.Columns.Add("Date", typeof(DateTime));
                        //dataTable.Columns.Add("UtilityExpenses", typeof(decimal));
                        //dataTable.Columns.Add("OfficeExpenses", typeof(decimal));
                        //dataTable.Columns.Add("TotalExpenses", typeof(decimal));

                        //DataRow newRow = dataTable.NewRow();
                        //newRow["Date"] = dateTimePicker1.Value;
                        //newRow["UtilityExpenses"] = decimal.Parse(textBox6.Text);
                        //newRow["OfficeExpenses"] = decimal.Parse(textBox5.Text);
                        //newRow["TotalExpenses"] = (decimal)newRow["UtilityExpenses"] + (decimal)newRow["OfficeExpenses"];
                        //dataTable.Rows.Add(newRow);

                        //con.Close();
                        //con.Open();
                        //foreach (DataRow row in dataTable.Rows)
                        //{

                        // Insert query
                        con.Close();
                        con.Open();
                        string query = "INSERT INTO dailyexpanses (Date, UtilityExpenses, OfficeExpenses, TotalExpenses) " +
                                       "VALUES (@Date, @UtilityExpenses, @OfficeExpenses, @TotalExpenses)";

                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@Date", DateTime.Parse(dateTimePicker1.Text));
                        command.Parameters.AddWithValue("@UtilityExpenses", textBox6.Text);
                        command.Parameters.AddWithValue("@OfficeExpenses", textBox5.Text);
                        command.Parameters.AddWithValue("@TotalExpenses", ttl);
                        command.ExecuteNonQuery();
                        con.Close();

                        SqlDataAdapter sda = new SqlDataAdapter("select max(id) from dailyexpanses", con);
                        DataSet dt = new DataSet();
                        sda.Fill(dt);

                        sda = new SqlDataAdapter("WITH CTE AS ( SELECT *,ROW_NUMBER() OVER (PARTITION BY name ORDER BY id desc) AS RowNum FROM Billtable WHERE paiddate >= '" + firstDayOfMonth + "' AND paiddate <= '" + lastDayOfMonth + "' AND remaining != 0 and given != 0) SELECT name, remaining, id FROM CTE WHERE RowNum = 1", con);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        List<string> itemlist = new List<string>();
                        List<string> itemlist1 = new List<string>();

                        for (int s = 0; s < ds.Tables[0].Rows.Count; s++)
                        {
                            itemlist.Add(ds.Tables[0].Rows[s][0].ToString());
                            itemlist1.Add(ds.Tables[0].Rows[s][1].ToString());
                        }
                        for (int s = 0; s < listBox1.Items.Count; s++)
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert Into ItemTable Values('" + dt.Tables[0].Rows[0][0].ToString() + "','" + listBox1.Items[s] + "','" + Price.Items[s] + "') ", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            con.Open();

                            if (itemlist.Contains(listBox1.Items[s]))
                            {
                                int index = itemlist.IndexOf(listBox1.Items[s].ToString());
                                itemlist1[index] = (int.Parse(itemlist1[index]) + int.Parse(Price.Items[s].ToString())).ToString();

                                string query2 = "INSERT INTO Billtable (name, paiddate, remaining, given,Total,date) " +
                                   "VALUES (@name, @paiddate, @remaining, @given,@Total,@date)";

                                SqlCommand command2 = new SqlCommand(query2, con);
                                command2.Parameters.AddWithValue("@name", itemlist[index]);
                                command2.Parameters.AddWithValue("@paiddate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command2.Parameters.AddWithValue("@remaining", itemlist1[index]);
                                command2.Parameters.AddWithValue("@given", "0");
                                command2.Parameters.AddWithValue("@Total", itemlist1[index]);
                                command2.Parameters.AddWithValue("@date", DateTime.Now);
                                command2.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                string query1 = "INSERT INTO Billtable (name, paiddate, remaining, given,Total,date) " +
                                        "VALUES (@name, @paiddate, @remaining, @given,@Total,@date)";

                                SqlCommand command1 = new SqlCommand(query1, con);
                                command1.Parameters.AddWithValue("@name", listBox1.Items[s]);
                                command1.Parameters.AddWithValue("@paiddate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command1.Parameters.AddWithValue("@remaining", Price.Items[s]);
                                command1.Parameters.AddWithValue("@given", "0");
                                command1.Parameters.AddWithValue("@Total", Price.Items[s]);
                                command1.Parameters.AddWithValue("@date", DateTime.Now);
                                command1.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        for (int s1 = 0; s1 < listBox2.Items.Count; s1++)
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd1 = new SqlCommand("Insert Into Item2Table Values('" + dt.Tables[0].Rows[0][0].ToString() + "','" + listBox2.Items[s1] + "','" + listBox3.Items[s1] + "') ", con);
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            con.Open();

                            if (itemlist.Contains(listBox2.Items[s1]))
                            {
                                int index = itemlist.IndexOf(listBox2.Items[s1].ToString());
                                itemlist1[index] = (int.Parse(itemlist1[index]) + int.Parse(listBox3.Items[s1].ToString())).ToString();

                                string query2 = "INSERT INTO Billtable (name, paiddate, remaining, given,Total,date) " +
                                   "VALUES (@name, @paiddate, @remaining, @given,@Total,@date)";

                                SqlCommand command2 = new SqlCommand(query2, con);
                                command2.Parameters.AddWithValue("@name", itemlist[index]);
                                command2.Parameters.AddWithValue("@paiddate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command2.Parameters.AddWithValue("@remaining", itemlist1[index]);
                                command2.Parameters.AddWithValue("@given", "0");
                                command2.Parameters.AddWithValue("@Total", itemlist1[index]);
                                command2.Parameters.AddWithValue("@date", DateTime.Now);
                                command2.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                string query1 = "INSERT INTO Billtable (name, paiddate, remaining, given,Total,date) " +
                                        "VALUES (@name, @paiddate, @remaining, @given,@Total,@date)";

                                SqlCommand command1 = new SqlCommand(query1, con);
                                command1.Parameters.AddWithValue("@name", listBox2.Items[s1]);
                                command1.Parameters.AddWithValue("@paiddate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command1.Parameters.AddWithValue("@remaining", listBox3.Items[s1]);
                                command1.Parameters.AddWithValue("@given", "0");
                                command1.Parameters.AddWithValue("@Total", listBox3.Items[s1]);
                                command1.Parameters.AddWithValue("@date", DateTime.Now);
                                command1.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        MessageBox.Show("Data saved successfully!");
                        //}
                        datagridShow();
                    }
                }
                else
                {
                    List<string> lst = new List<string>();
                    List<string> lst2 = new List<string>();
                    string gid, gid2;
                    gid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    gid2 = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    SqlDataAdapter sda = new SqlDataAdapter("select ItemName,Price,itemid from ItemTable Where did='" + gid + "'", con);
                    DataSet dt = new DataSet();
                    sda.Fill(dt);

                    SqlDataAdapter sda2 = new SqlDataAdapter("select ItemName,Price,itemid2 from Item2Table Where did='" + gid + "'", con);
                    DataSet dt2 = new DataSet();
                    sda2.Fill(dt2);

                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(dt.Tables[0].Rows[i][0].ToString());
                    }
                    for (int s = 0; s < listBox1.Items.Count; s++)
                    {
                        //if (listBox1.Items.Contains(dt.Tables[0].Rows[s][0].ToString()))
                        if (lst.Contains(listBox1.Items[s].ToString()))
                        {
                            int index = lst.IndexOf(listBox1.Items[s].ToString());

                            if (Price.Items[s].ToString() != dt.Tables[0].Rows[index][1].ToString())
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd1 = new SqlCommand("Update ItemTable Set Price='" + Price.Items[s].ToString() + "' WHERE ItemId='" + dt.Tables[0].Rows[index][2].ToString() + "' ", con);
                                cmd1.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        else if (!lst.Contains(listBox1.Items[s].ToString()))
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert Into ItemTable Values('" + gid + "','" + listBox1.Items[s] + "','" + Price.Items[s] + "') ", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    //Next expanses
                    for (int i = 0; i < dt2.Tables[0].Rows.Count; i++)
                    {
                        lst2.Add(dt2.Tables[0].Rows[i][0].ToString());
                    }
                    for (int s1 = 0; s1 < listBox2.Items.Count; s1++)
                    {
                        //if (listBox1.Items.Contains(dt.Tables[0].Rows[s][0].ToString()))
                        if (lst2.Contains(listBox2.Items[s1].ToString()))
                        {
                            int index = lst2.IndexOf(listBox2.Items[s1].ToString());

                            if (listBox3.Items[s1].ToString() != dt2.Tables[0].Rows[index][1].ToString())
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd1 = new SqlCommand("Update Item2Table Set price='" + listBox3.Items[s1].ToString() + "' WHERE ItemId2='" + dt2.Tables[0].Rows[index][2].ToString() + "' ", con);
                                cmd1.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        else if (!lst2.Contains(listBox2.Items[s1].ToString()))
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert Into Item2Table Values('" + gid2 + "','" + listBox2.Items[s1] + "','" + listBox3.Items[s1] + "') ", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    con.Close();
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Update dailyexpanses Set Utilityexpenses='" + textBox6.Text + "', officeexpenses='" + textBox5.Text + "', Totalexpenses='" + ttl + "' WHERE Id='" + gid + "' ", con);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    //if(listBox1.SelectedItem.ToString)

                    // Execute the update operation

                    //con.Open();
                    //SqlDataAdapter sdaRemaining = new SqlDataAdapter("WITH CTE AS ( SELECT *,ROW_NUMBER() OVER (PARTITION BY name ORDER BY id desc) AS RowNum FROM Billtable WHERE paiddate >= '" + firstDayOfMonth + "' AND paiddate <= '" + lastDayOfMonth + "' AND remaining != 0) SELECT name, remaining, id FROM CTE WHERE RowNum = 1", con);
                    //DataSet dsRemaining = new DataSet();
                    //sdaRemaining.Fill(dsRemaining);
                    //con.Close();

                    //// Bind the fetched data to the GridView
                    //dataGridView1.DataSource = dsRemaining.Tables[0];
                }
                datagridShow();
            }
            else
            {
                MessageBox.Show("Please enter values in both textboxes.");
            }
        }
        public void datagridShow()
        {
            try
            {
                con.Close();
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from DailyExpanses ", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.Rows.Clear();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    int total = 0, z = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime lastMonth = DateTime.Parse(dateTimePicker1.Text);
                        // DateTime lastMonth =  DateTime.Today.AddMonths(-1); 
                        DateTime firstDayOfLastMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                        int daysInLastMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
                        DateTime lastDayOfLastMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1).AddDays(daysInLastMonth);
                        DateTime databdate = DateTime.Parse(ds.Tables[0].Rows[i][1].ToString());
                        if (databdate >= firstDayOfLastMonth && databdate < lastDayOfLastMonth)
                        {
                            dataGridView1.Rows.Add();
                            //dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][0].ToString();
                            dataGridView1.Rows[z].Cells[0].Value = ds.Tables[0].Rows[i][0].ToString();
                            dataGridView1.Rows[z].Cells[1].Value = ds.Tables[0].Rows[i][1].ToString();
                            dataGridView1.Rows[z].Cells[2].Value = ds.Tables[0].Rows[i][2].ToString();
                            dataGridView1.Rows[z].Cells[3].Value = ds.Tables[0].Rows[i][3].ToString();
                            dataGridView1.Rows[z].Cells[4].Value = ds.Tables[0].Rows[i][4].ToString();
                            total = total + int.Parse(ds.Tables[0].Rows[i][4].ToString());
                            z++;
                        }
                    }
                    textBox8.Text = total.ToString(".Rs");
                }
                //else
                //{
                //    MessageBox.Show("No Record Found");
                //}
            }
            catch
            {
            }
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
        //private void MonthlyTotal()
        //{
        //    // Calculate total monthly expenses
        //    string query = "SELECT SUM(TotalExpenses) AS MonthlyTotalExpenses " +
        //                   "FROM dailyexpanses " +
        //                   "WHERE MONTH(Date) = @Month AND YEAR(Date) = @Year";

        //    using (SqlCommand command = new SqlCommand(query, con))
        //    {
        //        command.Parameters.AddWithValue("@Month", dateTimePicker1.Value.Month);
        //        command.Parameters.AddWithValue("@Year", dateTimePicker1.Value.Year);
        //        con.Close();
        //        con.Open();
        //        object result = command.ExecuteScalar();
        //        con.Close();

        //        if (result != DBNull.Value)
        //        {
        //            textBox8.Text = Convert.ToDecimal(result).ToString();

        //            // Refresh DataGridView to display data for the selected month
        //            dataGridView1.DataSource = null;
        //            dataGridView1.DataSource = dataTable;
        //        }
        //        else
        //        {
        //            MessageBox.Show("No expenses for the selected month.");
        //        }
        //    }
        //}

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            datagridShow();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                // Get the selected item from listBox2 and display it in textBox3
                txtname.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                // Get the selected item from listBox2 and display it in textBox3
                textBox3.Text = listBox2.SelectedItem.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SqlDataAdapter dsa = new SqlDataAdapter("SELECT ItemName,Price FROM ItemTable WHERE did='" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                DataSet ds = new DataSet();
                dsa.Fill(ds);
                listBox1.Items.Clear();
                Price.Items.Clear();
                for (int s = 0; s < ds.Tables[0].Rows.Count; s++)
                {
                    listBox1.Items.Add(ds.Tables[0].Rows[s][0].ToString());
                    Price.Items.Add(ds.Tables[0].Rows[s][1].ToString());
                }
                SqlDataAdapter dsa1 = new SqlDataAdapter("SELECT ItemName,Price FROM Item2Table WHERE did='" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                DataSet ds1 = new DataSet();
                dsa1.Fill(ds1);
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                for (int s = 0; s < ds1.Tables[0].Rows.Count; s++)
                {
                    listBox2.Items.Add(ds1.Tables[0].Rows[s][0].ToString());
                    listBox3.Items.Add(ds1.Tables[0].Rows[s][1].ToString());
                }
                DailyCalculate();
            }
            catch { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            textBox6.Text = "";
            listBox1.Items.Clear();
            Price.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            datagridShow();
        }
        public static DataTable dt1;
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    //New code
                    dt1 = DataGridView_To_Datatable(dataGridView1);
                    var name0 = dt1.Rows[0][1];

                    //end new code
                    //DataTable dt = DataGridView_To_Datatable(dataGridView1);
                    //var name0 = dt.Rows[0][1];

                    DisplayBillFormRecord d1 = new DisplayBillFormRecord();
                    d1.Show();
                    //MessageBox.Show("Data is Converted!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public static DataTable DataGridView_To_Datatable(DataGridView dg)
        {
            DataTable ExportDataTable = new DataTable();
            //foreach (DataGridViewColumn col in dg.Columns)
            //{
            ExportDataTable.Columns.Add("Id");
            ExportDataTable.Columns.Add("Date");
            ExportDataTable.Columns.Add("Utility Expanses");
            ExportDataTable.Columns.Add("Office Expanses");
            ExportDataTable.Columns.Add("Total Expanses");
            //}
            foreach (DataGridViewRow row in dg.Rows)
            {
                DataRow dRow = ExportDataTable.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                ExportDataTable.Rows.Add(dRow);
            }
            return ExportDataTable;
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter Itemname", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter Itemname", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter Price", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter Price", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from DailyExpanses Where id= @id", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Deleted Successfully");
            con.Close();
            datagridShow();

        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    DialogResult dialogResult = MessageBox.Show("Are You Sure To Delete Count", "Delete", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        con.Close();
            //        con.Open();
            //        string str = "Delete From DailyExpanses where ID = @ID ";
            //        SqlCommand cmd = new SqlCommand(str, con);
            //        cmd.Parameters.AddWithValue("@ID", dataGridView1.Rows[0].Cells[0].Value);
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("Record Deleted Successfully..!!");
            //        con.Close();

            //        con.Close();
            //        con.Open();
            //        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM DailyExpanses", con);
            //        DataSet ds = new DataSet();
            //        sda.Fill(ds);
            //        dataGridView1.Rows.Clear();
            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            dataGridView1.Rows.Add();
            //            dataGridView1.Rows[i].Cells[0].Value = ds.Tables[0].Rows[i][0].ToString();
            //            dataGridView1.Rows[i].Cells[1].Value = ds.Tables[0].Rows[i][1].ToString();
            //            dataGridView1.Rows[i].Cells[2].Value = ds.Tables[0].Rows[i][2].ToString();
            //            dataGridView1.Rows[i].Cells[3].Value = ds.Tables[0].Rows[i][3].ToString();
            //            dataGridView1.Rows[i].Cells[4].Value = ds.Tables[0].Rows[i][4].ToString();
            //        }
            //        con.Close();
            //    }
            //}
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure To Delete Count", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    con.Close();
                    con.Open();
                    string str = "Delete From DailyExpanses where ID = @ID ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@ID", dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted Successfully..!!");
                    datagridShow();

                    con.Close();

                }
            }
        }


    }
}
