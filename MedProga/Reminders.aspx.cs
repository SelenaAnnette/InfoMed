using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedProga
{
    public partial class Reminders : System.Web.UI.Page
    {
        public string sql;
        public DataTable dataTable;
        public DataRow row;
        public SqlDataAdapter dataAdapter;

        public void zapros(string sql)
        {
            try
            {
                StreamReader strrd = new StreamReader("S:/ВГТУ/5 курс/9 семестр/Диплом/Диплом1/MedProga/conn1.txt");
                string cs = strrd.ReadLine();
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter(command);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                conn.Close();
            }
            catch
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_save_Click(object sender, EventArgs e)
        {
            if (TextBox_reminder.Text != "")
            {
                sql = "Select * from Reminders where Текст LIKE'" + TextBox_reminder.Text + "'";
                zapros(sql);
                if (dataTable.Rows.Count == 0)
                {
                    row = dataTable.NewRow();
                    row["id_пац"] = 1;
                    row["Текст"] = TextBox_reminder.Text;
                    if (CheckBox_activation.Checked == true) row["Активировано"] = "Да"; else row["Активировано"] = "Нет";
                    string time = "";
                    for (int i = 0; i < 24; i++)
                    {
                        if (CheckBoxList_time.Items[i].Selected == true)
                        {
                            if (time.Contains(CheckBoxList_time.Items[i].ToString()) == false)
                            {
                                time += CheckBoxList_time.Items[i].ToString() + ",";
                            }
                        }
                    }
                    time = time.Remove(time.Length - 1, 1);
                    row["Время_отправки"] = time;
                    string days = "";
                    for (int i = 0; i < 7; i++)
                    {
                        if (CheckBoxList_days.Items[i].Selected == true)
                        {
                            if (days.Contains(CheckBoxList_days.Items[i].ToString()) == false)
                            {
                                days += CheckBoxList_days.Items[i].ToString() + ",";
                            }
                        }
                    }
                    days = days.Remove(days.Length - 1, 1);
                    row["Дни_недели"] = days;
                    try
                    {
                        dataTable.Rows.Add(row);
                        dataAdapter.Update(dataTable);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}