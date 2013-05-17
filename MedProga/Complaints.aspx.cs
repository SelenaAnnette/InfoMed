using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedProga
{
    using DataLayer.Persistence.Person;
    using DataLayer.Persistence.Symptom;

    using Ninject;

    public partial class Complaints : System.Web.UI.Page
    {
        private DateTime dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            var symptomsRepo = Binder.NinjectKernel.Get<ISymptomRepository>();
            var symptoms = symptomsRepo.GetAll().ToArray();
            var sympList = new List<string> { };
            for (int i = 0; i < symptoms.Length; i++)
            {
                sympList.Add(symptoms[i].Name);
            }
            var sympSortedList = sympList.OrderBy(s => s);
            for (int i = 0; i < sympSortedList.Count(); i++)
            {
                this.CheckBoxList_symptoms.Items.Add(sympSortedList.ElementAt(i));
            }
            ////var symptoms = symptomsRepo.GetAll();
            ////symptoms = symptoms.OrderBy(s => s.Name);
            ////var symptomsArray = symptoms.ToArray();
            ////for (int i = 0; i < symptoms.Count(); i++)
            ////{
            ////    this.CheckBoxList_symptoms.Items.Add(symptomsArray[i].Name);
            ////}
        }

        protected void Button_complaints_Click(object sender, EventArgs e)
        {
            var personSymptomsRep = Binder.NinjectKernel.Get<IPersonSymptomRepository>();
            var personSymptomsFac = new PersonSymptomFactory();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRepo.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var symptomsRepo = Binder.NinjectKernel.Get<ISymptomRepository>();
            for (int i = 0; i < this.CheckBoxList_symptoms.Items.Count; i++)
            {
                if (this.CheckBoxList_symptoms.Items[i].Selected)
                {

                    var sympId =
                        symptomsRepo.GetEntitiesByQuery(s => s.Name == this.CheckBoxList_symptoms.Items[i].Text)
                                    .First()
                                    .Id;
                    try
                    {
                        dt = Convert.ToDateTime(this.TextBox_date_time.Text);
                    }
                    catch (Exception)
                    {
                        this.TextBox_date_time.Text = string.Empty;
                        dt = DateTime.Now;
                    }
                    var perSymp = personSymptomsFac.Create(Guid.NewGuid(), perId, sympId, dt);
                    personSymptomsRep.CreateOrUpdateEntity(perSymp);
                }
            }
            this.CheckBoxList_symptoms.Items.Clear();
            this.Page_Load(sender, e);
        }
    }
}
  