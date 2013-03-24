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
    public partial class _Default : System.Web.UI.Page
    {
        public string id_ch="";
        public string name_ch = "";
        public string mname_ch = "";
        public string surname_ch = "";
        public string symp_ch = "";
        public string notif_ch = "";
        public string risk_ch = "";
        public string phone_ch = "";
        public string email_ch = "";
        public string sql = "";
        public string prep_ch="";

        protected void Page_Load(object sender, EventArgs e)
        {

            sql = @"Select Patient.id,Doctor.Familiya as Лечащий_врач,Patient.Imya,Patient.Otchestvo, Patient.Familiya from Patient,Doctor WHERE Doctor.id=Patient.Doctor";

            DataTable dt = GetDataTable(sql);

            grid1.DataSource = dt;
            grid1.DataBind();


            if (name_c.Checked == true)
            {
                name_ch = ",Patient.Imya as Имя";
            }
            else
            {
                name_ch = "";
            }

            if (mname_c.Checked == true)
            {
                mname_ch = ",Patient.Otchestvo as Отчество";
            }
            else
            {
                mname_ch = "";
            }

            if (surname_c.Checked == true)
            {
                surname_ch = ",Patient.Familiya as Фамилия";
            }
            else
            {
                surname_ch = "";
            }

            if (risk_c.Checked == true)
            {
                risk_ch = ",Risk_Factors.Name as Факторы_риска";
            }
            else
            {
                risk_ch = "";
            }

            if (prep_c.Checked == true)
            {
                prep_ch = ",Preparat.Name as Принимаемый_препарат";
            }
            else
            {
                prep_ch = "";
            }

            if (symp_c.Checked == true)
            {
                symp_ch = ",Symptoms.Name as Симптомы";
            }
            else
            {
                symp_ch = "";
            }

            if (notif_c.Checked == true)
            {
                notif_ch = ",Patient.Notification as Напоминания";
            }
            else
            {
                notif_ch = "";
            }

            if (phone_c.Checked == true)
            {
                phone_ch = ",Patient.Phone as Телефон";
            }
            else
            {
                phone_ch = "";
            }

            if (email_c.Checked == true)
            {
                email_ch = ",Patient.Email as Электронный_Адрес";
            }
            else
            {
                email_ch = "";
            }


           /* string sql = @"Select * from Patient";
            DataTable dt = GetDataTable(sql);

            grid1.DataSource = dt;
            grid1.DataBind();*/
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            sql = @"Select Patient.id,Doctor.Familiya";// + id_ch + name_ch + mname_ch + surname_ch + "from Patient";
            sql += name_ch;
            sql += mname_ch;
            sql += surname_ch;
            sql += risk_ch;
            sql += prep_ch;
            sql += symp_ch;
            sql += notif_ch;
            sql += phone_ch;
            sql += email_ch;
            sql += " FROM Patient,Preparat,Risk_factors,Symptoms,Doctor WHERE Patient.Doctor=Doctor.id ";

            if (prep_c.Checked == true)
            {
                sql += "AND Patient.Preparats=Preparat.Code ";
            }

            if (symp_c.Checked == true)
            {
                sql += "AND Patient.Symptoms=Symptoms.Code ";
            }


            if (risk_c.Checked == true)
            {
                sql += "AND Patient.Risk_Factors=Risk_factors.Code";
            }

            DataTable dt = GetDataTable(sql);

            grid1.DataSource = dt;
            grid1.DataBind();
        }
    }
}
