using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            Label_symptoms.Text = "Нет симптомов для выбора";
            try
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
                if (sympSortedList.Count() > 0) Label_symptoms.Text = "Симптомы";
            }
            catch (Exception)
            {
                
            }
        }

        protected void Button_complaints_Click(object sender, EventArgs e)
        {
            try
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
                            if (dt > DateTime.Now)
                            {
                                dt = DateTime.Now;
                            }
                            this.TextBox_date_time.Text = Convert.ToString(dt);
                        }
                        catch (Exception)
                        {
                            dt = DateTime.Now;
                            this.TextBox_date_time.Text = Convert.ToString(dt);
                        }
                        var perSymp = personSymptomsFac.Create(Guid.NewGuid(), perId, sympId, dt);
                        personSymptomsRep.CreateOrUpdateEntity(perSymp);
                        Label labelSave = (Label)Master.FindControl("Label_save");
                        labelSave.Text = "Сохранение прошло успешно";
                        //Color of text
                        labelSave.ForeColor = Color.FromArgb(0, 144, 36);
                    }
                }
                this.CheckBoxList_symptoms.Items.Clear();
                this.Page_Load(sender, e);
            }
            catch (Exception)
            {
                
            }
        }

        protected void Button_deselect_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.CheckBoxList_symptoms.Items.Count; i++)
            {
                if (this.CheckBoxList_symptoms.Items[i].Selected)
                {
                    this.CheckBoxList_symptoms.Items[i].Selected = false;
                }
            }
            this.CheckBoxList_symptoms.Items.Clear();
            this.Page_Load(sender, e);
        }
    }
}
  