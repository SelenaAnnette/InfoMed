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
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            TextBox_date_time.Text = dt.ToString();
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
                CheckBoxList_symptoms.Items.Add(sympSortedList.ElementAt(i));
            }
        }

       protected void Button_complaints_Click(object sender, EventArgs e)
        {
            if (this.TextBox_date_time.Text != string.Empty)
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
                        var sympId = symptomsRepo.GetEntitiesByQuery(s => s.Name == this.CheckBoxList_symptoms.Items[i].Text).First().Id;
                        DateTime dt = Convert.ToDateTime(this.TextBox_date_time.Text);
                        var perSymp = personSymptomsFac.Create(Guid.NewGuid(), perId, sympId, dt);
                        personSymptomsRep.CreateOrUpdateEntity(perSymp);
                    }
                }
            }
            CheckBoxList_symptoms.Items.Clear();
            this.Page_Load(sender, e);
        }
            
    }
}
  