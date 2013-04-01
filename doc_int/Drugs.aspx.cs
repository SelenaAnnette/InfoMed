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
        }

        public void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num = Convert.ToInt16(e.CommandArgument);
                TextBox1.Text = GridView1.Rows[num].Cells[3].Text;
                TextBox3.Text = GridView1.Rows[num].Cells[4].Text;              
            }

             

        }

        public void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num2 = Convert.ToInt16(e.CommandArgument);
                TextBox2.Text = GridView2.Rows[num2].Cells[2].Text;
                TextBox4.Text = GridView2.Rows[num2].Cells[4].Text;

            }
            
        }

        public void Button1_Click(object sender, EventArgs e)
        {

            Guid_pat = new Guid(TextBox3.Text);
            Guid_drug = new Guid(TextBox4.Text);


            var AssignedMedicament = AssignedMedicamentFactory.Create(Guid.NewGuid(), Guid_pat, Guid_drug, Convert.ToDouble(dosage.Text), "unit", DateTime.Now, Convert.ToInt16(dayCount.Text), Convert.ToInt16(timesAtDay.Text), Convert.ToInt16(eachDay.Text));
            AssignedMedicamentRepo.CreateOrUpdateEntity(AssignedMedicament);
        }
    }
}

