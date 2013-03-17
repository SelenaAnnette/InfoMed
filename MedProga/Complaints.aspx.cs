using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedProga
{
    public partial class Complaints : System.Web.UI.Page
    {
        public string sql;
        public DataTable dataTable;
        public DataRow row;
        public SqlDataAdapter dataAdapter;
        public int k;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            TextBox_date.Text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
            TextBox_time.Text = dt.Hour.ToString() + ":" + dt.Minute.ToString();
        }

        public void zapros(string sql)
        {
            try
            {
                StreamReader strrd = new StreamReader("S:/ВГТУ/5 курс/9 семестр/Диплом/Диплом1/MedProga/conn2.txt");
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
            catch { }
        }

        protected void Button_complaints_Click(object sender, EventArgs e)
        {
            if ((TextBox_date.Text != "") && (TextBox_time.Text != ""))
            {
                //sql = "Select * FROM Complaints WHERE Дата LIKE'" + TextBox_date.Text + "' AND Время LIKE'" + TextBox_time.Text+"'";
                sql = "Select * From Symptoms_monitoring WHERE DateTime LIKE";
                zapros(sql);
                if (dataTable.Rows.Count==0)
                {
                    //row["id_пац"] = 1;
                    //row["Дата"] = TextBox_date.Text;
                    //row["Время"] = TextBox_time.Text;
                    //string complaints = "";
                    for (int i = 0; i < 68; i++)
                    {
                        if (CheckBoxList_symptoms.Items[i].Selected == true)
                        {
                            //if (complaints.Contains(CheckBoxList_symptoms.Items[i].ToString()) == false)
                            //{
                            //    complaints += CheckBoxList_symptoms.Items[i].ToString() + ",";
                            //}
                            //row = dataTable.NewRow();
                            //row["Code"] = i + 1;
                            //row["Name"] = CheckBoxList_symptoms.Items[i].ToString();
                            //try
                            //{
                            //    dataTable.Rows.Add(row);
                            //    dataAdapter.Update(dataTable);
                            //}
                            //catch
                            //{
                            //}
                        }

                        //if (TextBox_complaints.Text.Length != 0) complaints += TextBox_complaints.Text;
                        //else complaints = complaints.Remove(complaints.Length - 1, 1);
                        //row["Симптомы"] = complaints;
                    }
                }
            }
        }
            
    }
}
  