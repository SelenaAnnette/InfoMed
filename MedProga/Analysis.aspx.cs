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
    public partial class Analysis : System.Web.UI.Page
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
            sql = "Select Дата,Время_суток,САД,ДАД,ЧСС,";
            sql+="(САД-ДАД) as ПД,";
            sql += "AVG(САД) as Сред.САД, AVG(ДАД) as Сред.ДАД, AVG(ЧСС) as Сред.ЧСС, AVG(ПД) as Сред.ПД,";
            sql+="MAX(САД) as Макс.САД, MAX(ДАД) as Макс.ДАД, MAX(ЧСС) as Макс.ЧСС, MAX(ПД) as Макс.ПД From Parameters";
            DateTime dt = DateTime.Now;
            //string fromdate = "00/00/0000";
            //string todate = dt.Date.ToString();
            //if (TextBox_from_date.Text.Length != 0) fromdate = TextBox_from_date.Text;
            //if (TextBox_to_date.Text.Length != 0) todate = TextBox_to_date.Text;
            //sql += "WHERE Дата BETWEEN " + fromdate + " " + todate;
            //zapros(sql);
            //GridView_analysis.DataSource = dataTable;
        }
    }
}