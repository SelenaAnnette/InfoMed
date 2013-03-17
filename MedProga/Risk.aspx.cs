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
    public partial class Risk : System.Web.UI.Page
    {
        public DataTable dataTable;
        public DataRow row;
        public SqlDataAdapter dataAdapter;

        public void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string date = dt.Day + "/" + dt.Month + "/" + dt.Year;
        }

        public void zapros(string sql)
        {
            try
            {
                StreamReader str = new StreamReader("S:/ВГТУ/5 курс/9 семестр/Диплом/Диплом1/MedProga/conn1.txt");
                string cs = str.ReadLine();
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

        protected void Button_risk_Click(object sender, EventArgs e)
        {
            string sql = "Select * from Risk where Дата LIKE'";
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string date = dt.Day + "/" + dt.Month + "/" + dt.Year;
            sql += date + "'";
            zapros(sql);
            if (dataTable.Rows.Count == 0)
            {
                row=dataTable.NewRow();
                row["id_пац"] = 1;
                row["Дата"] = date;
                row["Дистанция_км"] = TextBox_distance.Text;
                row["Калорий_ккал"] = TextBox_kkal.Text;
                row["Алкоголя_мл"] = TextBox_alcohol.Text;
                row["Крепость_проценты"] = TextBox_alc_rate.Text;
                row["Сигарет_шт"] = TextBox_smoking.Text;
                try
                {
                    dataTable.Rows.Add(row);
                    dataAdapter.Update(dataTable);
                }
                catch { }
            }
        }
    }
}