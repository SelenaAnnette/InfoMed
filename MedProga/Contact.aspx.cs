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
    public partial class Contact : System.Web.UI.Page
    {
        public string sql;
        public DataTable dataTable;
        public DataRow row;
        public SqlDataAdapter dataAdapter;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void zapros(string sql)
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

        protected void Button_contact_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            sql = "Select * from Contact";
            zapros(sql);
            row = dataTable.NewRow();
            row["id_пац"] = 1;
            row["Дата_и_время"] = dt.ToString();
            row["Сообщение"] = TextBox_contact.Text;
            row["id_пац"] = 1;
            try
            {
                dataTable.Rows.Add(row);
                dataAdapter.Update(dataTable);
            }
            catch { }
            
        }
    }
}