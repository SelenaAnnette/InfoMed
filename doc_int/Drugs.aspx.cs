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
using System.ComponentModel;

namespace doc_int
{
    public partial class About : System.Web.UI.Page
    {

        public class GuidConverter : TypeConverter
        {

        }

        public int interval;
        public Guid patient_guid;
        public Guid drug_guid;
        public string indpat;
        public Guid Guid_pat = new Guid("11111111-1111-1111-1111-111111111111");
        public Guid Guid_drug = new Guid("11111111-1111-1111-1111-111111111111");
        public string ggg;

        public DataTable dt;
        public IDataAdapter da;

        public int times_in_day_num;
        public int pills_count_num;
        public string patient_surname;
        public IPersonRepository personRepo = Binder.NinjectKernel.Get<IPersonRepository>();
        public IMedicamentRepository medicamentRepo = Binder.NinjectKernel.Get<IMedicamentRepository>();
        public IAssignedMedicamentRepository AssignedMedicamentRepo = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
        public AssignedMedicamentFactory AssignedMedicamentFactory = new AssignedMedicamentFactory();

        protected void Page_Load(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            
            
            var foundPerson = personRepo.GetAll().ToList();
            GridView1.DataSource = foundPerson;
            GridView1.DataBind();

            
            var getMedicament = medicamentRepo.GetAll().ToList();
            GridView2.DataSource = getMedicament;
            GridView2.DataBind();



            //personsRepo.GetEntitiesByQuery(p => p.LastName == "Smith").First().Id;



        }

        public void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num = Convert.ToInt16(e.CommandArgument);
                TextBox1.Text = GridView1.Rows[num].Cells[3].Text;
                TextBox3.Text = GridView1.Rows[num].Cells[4].Text;
          
               // patient_surname = GridView1.Rows[num].Cells[3].Text;
                //string indpat = GridView1.Rows[num].Cells[3].Text;
                //string id_patient=GridView1.Rows[num].Cells[4].Text;
                //Guid patient_guid = new Guid(TextBox3.Text);                      
            }

             

        }

        public void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num2 = Convert.ToInt16(e.CommandArgument);
                TextBox2.Text = GridView2.Rows[num2].Cells[2].Text;
                //string id_drugs=GridView2.Rows[num2].Cells[4].Text;
                TextBox4.Text = GridView2.Rows[num2].Cells[4].Text;
                //string idd = TextBox4.Text;
               // Guid drug_guid = Guid.Parse(idd);
            }
            
        }

        public void Button1_Click(object sender, EventArgs e)
        {

            Guid_pat = new Guid(TextBox3.Text);
            Guid_drug = new Guid(TextBox4.Text);

            var AssignedMedicament = AssignedMedicamentFactory.Create(Guid_pat, Guid_drug, Convert.ToDouble(pill_count.Text), "unit", Convert.ToDouble(times_in_day.Text));            
            AssignedMedicamentRepo.CreateOrUpdateEntity(AssignedMedicament);
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