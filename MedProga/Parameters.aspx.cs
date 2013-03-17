using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedProga
{
    public partial class Parameters : System.Web.UI.Page
    {
        public string sql;
        public DataTable dataTable;
        public DataRow row;
        public SqlDataAdapter dataAdapter;

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            TextBox_date.Text = dt.Day.ToString() +"/"+dt.Month.ToString() +"/"+dt.Year.ToString();
        }
        
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

        public void Button_parameters_Click(object sender, EventArgs e)
        {
            if ((TextBox_date.Text != "") && (TextBox_sad.Text != "") && (TextBox_dad.Text != "") && (TextBox_chss.Text != "") && (TextBox_weight.Text != "") && (TextBox_bedra.Text != "") && (TextBox_taliya.Text != ""))
            {
                sql = "Select * FROM Parameters WHERE Дата LIKE'" + TextBox_date.Text + "' AND Время_суток LIKE'" + DropDownList1.SelectedValue + "'";
                zapros(sql);
                if (dataTable.Rows.Count == 0)
                {
                    row = dataTable.NewRow();
                    row["Дата"] = TextBox_date.Text;
                    row["Время_суток"] = DropDownList1.Text;
                    row["САД"] = TextBox_sad.Text;
                    row["ДАД"] = TextBox_dad.Text;
                    row["ЧСС"] = TextBox_chss.Text;
                    row["Вес"] = TextBox_weight.Text;
                    row["Окр_бедер"] = TextBox_bedra.Text;
                    row["Окр_талии"] = TextBox_taliya.Text;
                    row["id_пац"] = 1;
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
            else
            {
            }
        }

                
    }
}