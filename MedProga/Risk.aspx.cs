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
    using DataLayer.Persistence.RiskFactor;

    using Ninject;

    public partial class Risk : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Saving(string personSurname, string riskFactorName, TextBox tb)
        {
            var riskFactorRep = Binder.NinjectKernel.Get<IRiskFactorRepository>();
            var riskFacId = riskFactorRep.GetEntitiesByQuery(rf => rf.Title == riskFactorName).First().Id;
            var personsRep = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRep.GetEntitiesByQuery(p => p.LastName == personSurname).First().Id;
            var personRiskFactorRep = Binder.NinjectKernel.Get<IPersonRiskFactorRepository>();
            var personRiskFactorFac = new PersonRiskFactorFactory();
            if (tb.Text != string.Empty)
            {
                try
                {
                    double riskFactorValue = Convert.ToDouble(tb.Text);
                    DateTime dt = DateTime.Now;
                    var riskFactor = personRiskFactorFac.Create(Guid.NewGuid(), perId, riskFacId, riskFactorValue, dt);
                    personRiskFactorRep.CreateOrUpdateEntity(riskFactor);
                }
                catch (Exception)
                {
                    tb.Text = string.Empty;
                    tb.BackColor = Color.FromArgb(255, 255, 183);
                }
            }
        }

       protected void Button_risk_Click(object sender, EventArgs e)
       {
           ////Saving Пройденная дистанция за день (км)
           this.Saving("Glazunov", "Пройденная дистанция за день", this.TextBox_distance);
           ////Saving Количество поглощенных с пищей калорий (ккал)
           this.Saving("Glazunov", "Количество поглощенных с пищей калорий", this.TextBox_kkal);
           ////Saving Количество алкоголя за сутки (мл)
           this.Saving("Glazunov", "Количество алкоголя за сутки", this.TextBox_alcohol);
           ////Saving Крепость принятого алкоголя (%)
           this.Saving("Glazunov", "Крепость принятого алкоголя", this.TextBox_alc_rate);
           ////Saving Количество выкуренных сигарет (шт в день)
           this.Saving("Glazunov", "Количество выкуренных сигарет за день", this.TextBox_smoking);
       }
    }
}