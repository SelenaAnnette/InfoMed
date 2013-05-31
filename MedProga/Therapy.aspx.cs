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
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Person;

    using Ninject;

    public partial class Therapy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_show_drugs_Click(object sender, EventArgs e)
        {
            this.Label_drugs.Visible = false;
            DateTime dt = DateTime.Now;
            if (this.TextBox_date_time.Text != string.Empty)
            {
                try
                {
                    dt = Convert.ToDateTime(this.TextBox_date_time.Text);
                }
                catch (Exception)
                {
                    this.TextBox_date_time.Text = string.Empty;
                }
            }
            var assignedMedRep = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
            var personsRep = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRep.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var assignedMed = assignedMedRep.GetEntitiesByQuery(
                p => 
                p.PersonConsultation.PatientId == perId &&
                p.StartDate <= dt && dt <= p.FinishDate);
            var medicamentRep = Binder.NinjectKernel.Get<IMedicamentRepository>();
            var drugs = medicamentRep.GetAll();
            var aplicWayRep = Binder.NinjectKernel.Get<IMedicamentApplicationWayRepository>();
            var aplicWays = aplicWayRep.GetAll();
            this.GridView_drugs.DataSource = from asM in assignedMed
                                             join drug in drugs on asM.MedicamentId equals drug.Id
                                             join aplicWay in aplicWays on asM.MedicamentApplicationWayId equals aplicWay.Id
                                             select
                                                 new
                                                     {
                                                         Название = drug.Name,
                                                         Описание = drug.Description,
                                                         СпособПриема = aplicWay.Name,
                                                         Доза = asM.Dosage,
                                                         Частота = asM.Frequency,
                                                         С = asM.StartDate,
                                                         По = asM.FinishDate
                                                     };
            if (assignedMed.Count() > 0) Label_drugs.Visible = true;
            this.GridView_drugs.DataBind();
        }
    }
}
