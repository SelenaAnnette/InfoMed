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
    public partial class Therapy : System.Web.UI.Page
    {
        public string sql;
        public DataTable dataTable;
        public DataRow row;
        public SqlDataAdapter dataAdapter;

        public void zapros(string sql)
        {
            try
            {
                //StreamReader strrd = new StreamReader("S:/ВГТУ/5 курс/9 семестр/Диплом/Диплом1/MedProga/conn1.txt");
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
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            TextBox_date.Text = dt.Day+"/"+dt.Month+"/"+dt.Year;
            TextBox_time.Text = dt.Hour+":"+dt.Minute+":"+dt.Second;
        }

       

        protected void Button_therapy_Click(object sender, EventArgs e)
        {
            if ((TextBox_date.Text!="")&&(TextBox_time.Text!=""))
            {
                sql = "Select * from Therapy where Дата LIKE'" + TextBox_date.Text + "' and Время LIKE'" + TextBox_time.Text + "'";
                zapros(sql);
                if (dataTable.Rows.Count == 0)
                {
                    row = dataTable.NewRow();
                    row["id_пац"] = 1;
                    row["Дата"] = TextBox_date.Text;
                    row["Время"] = TextBox_time.Text;
                    string drugs="";
                    for (int i = 0; i < 3; i++)
                    {
                        if (CheckBoxList_drugs.Items[i].Selected == true)
                        {
                            if (drugs.Contains(CheckBoxList_drugs.Items[i].ToString()) == false)
                            {
                                drugs += CheckBoxList_drugs.Items[i].ToString() + ",";
                            }
                        }
                    }
                    drugs = drugs.Remove(drugs.Length - 1, 1);
                    row["Принятые_препараты"]=drugs;
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