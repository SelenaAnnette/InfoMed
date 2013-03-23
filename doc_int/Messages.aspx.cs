using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace doc_int
{
    public partial class Messages : System.Web.UI.Page
    {
        public string sql;


        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = @"Select * from Messages WHERE To_ Like 1";
            DataTable dt = GetDataTable(sql);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;

        }



    }
}