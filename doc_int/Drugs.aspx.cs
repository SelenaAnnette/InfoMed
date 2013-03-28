using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using DataLayer.Persistence.Group;
using DataLayer.Persistence.Person;
using DataLayer.Persistence.Symptom;
using DataLayer.Persistence.Measuring;
using Ninject;
using DataLayer.Persistence.Medicament;

namespace doc_int
{
    public partial class About : System.Web.UI.Page
    {
        public string id_ch;
        public string name_ch;
        public string mname_ch;
        public string surname_ch;
        public string symp_ch;
        public string notif_ch;
        public string risk_ch;
        public string phone_ch;
        public string email_ch;
        public string sql;
        public string sql2;
        public string sql3;
        public int day_end_num;
        public int interval;
        public DataRow row;
        public int id_pat;
        public int times_in_day_num;
        public int pills_count_num;
        public string drugs_name;
        public DateTime day_for_pills;
        public TimeSpan time_for_pills;


        protected void Page_Load(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            day_for_pills = DateTime.Today;





            string sql = @"Select * from Preparat";
            //DataTable dt = GetDataTable(sql);

           // GridView1.DataSource = dt;
            //GridView1.DataBind();

            string sql2 = @"Select id,Imya as Имя,Otchestvo as Отчество, Familiya as Фамилия from Patient";
           // DataTable dt2 = GetDataTable2(sql2);

           // GridView2.DataSource = dt2;
           // GridView2.DataBind();


            var personRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var foundPerson = personRepo.GetAll().ToList();
            GridView1.DataSource = foundPerson;
            GridView1.DataBind();

            var medicamentRepo = Binder.NinjectKernel.Get<IMedicamentRepository>();
            var getMedicament = medicamentRepo.GetAll().ToList();
            GridView2.DataSource = getMedicament;
            GridView2.DataBind();

        }

    }
}

        /*
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

         private DataTable GetDataTable2(string sql2)
        {
            DataTable dt2 = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ToString()))
            {
                SqlDataAdapter da2 = new SqlDataAdapter(sql2, conn);
                da2.Fill(dt2);
            }
            return dt2;
        }

         private DataTable GetDataTable3(string sql3)
         {
             DataTable dt3 = new DataTable();

             using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ToString()))
             {
                 SqlDataAdapter da3 = new SqlDataAdapter(sql3, conn);
                 da3.Fill(dt3);
             }
             return dt3;
         }

         protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
         {

             if (e.CommandName == "Select")
             {
                 Int16 num = Convert.ToInt16(e.CommandArgument);

                 TextBox1.Text = GridView1.Rows[num].Cells[3].Text;
                 drugs_name = GridView1.Rows[num].Cells[3].Text;
             }

         }

         protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
         {
             if (e.CommandName == "Select")
             {
                 Int16 num = Convert.ToInt16(e.CommandArgument);

                 TextBox2.Text = GridView2.Rows[num].Cells[4].Text;
                 id_pat = Convert.ToInt16(GridView2.Rows[num].Cells[1].Text);
             }
         }

         protected void Button1_Click(object sender, EventArgs e)
         {
             sql3 = "Select * From Notificaions";
             DataTable dt3 = GetDataTable2(sql3);

              pills_count_num = Convert.ToInt16(pill_count.Text);
              day_end_num = Convert.ToInt16(day_end.Text);
              interval = Convert.ToInt16(day_count.Text);
              times_in_day_num = Convert.ToInt16(times_in_day.Text);

             if ((day_count.Text != "") && (day_end.Text != "") && (pill_count.Text != ""))
             {
                 
                
                     for (int i = 0; i < day_end_num; i = i + interval)
                     {
                         for (int j = 0; j < times_in_day_num; j++)
                         {

                             time_for_pills = new TimeSpan((12/i)+12, 0, 0);
                             row = dt3.NewRow();
                             row["date_time"] = day_for_pills.AddDays(i);
                             //row["date_time"] = day_for_pills.AddDays(i) + time_for_pills;
                             row["is_active"] = 1;
                             row["patient"] = id_pat;
                             row["text"] = "Напоминание. Примите препарат " + pills_count_num + " " + drugs_name;
                         }
                     }
                 
             }


         }





    }
}
    */