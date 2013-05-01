using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataLayer.Persistence.Measuring;
using DataLayer.Persistence.Person;

namespace MedProga
{
    using Ninject;

    public partial class Parameters : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            this.TextBox_DateTime.Text = dt.ToString();         
        }

        public void Saving(string personSurname, string parName, TextBox tb)
        {
            var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
            var personMeasuringFactory = new PersonMeasuringFactory();
            var personId = personsRepo.GetEntitiesByQuery(p => p.LastName == personSurname).First().Id;
            if (tb.Text != string.Empty)
            {
                try
                {
                    var parId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == parName).First().Id;
                    double parValue = Convert.ToDouble(tb.Text);
                    DateTime dt = Convert.ToDateTime(this.TextBox_DateTime.Text);
                    var par = personMeasuringFactory.Create(Guid.NewGuid(), parId, personId, dt, parValue);
                    personMeasuringRepo.CreateOrUpdateEntity(par);
                }
                catch (Exception)
                {
                    tb.BackColor = Color.FromArgb(255, 255, 183);
                    tb.Text = string.Empty;
                }
            }
        }
        
        public void Button_parameters_Click(object sender, EventArgs e)
        {
            if (this.TextBox_DateTime.Text != string.Empty)
            {
                ////Saving САД
                this.Saving("Glazunov", "Систолическое АД", this.TextBox_sad);
                ////Saving ДАД
                this.Saving("Glazunov", "Диастолическое АД", this.TextBox_dad);
                ////Saving ЧСС
                this.Saving("Glazunov", "Частота сердечных сокращений", this.TextBox_chss);
                ////Saving Вес
                this.Saving("Glazunov", "Вес", this.TextBox_weight);
                ////Saving Окр_талии
                this.Saving("Glazunov", "Окружность талии", this.TextBox_okr_talii);
                ////Saving Окр_бедер
                this.Saving("Glazunov", "Окружность бедер", this.TextBox_okr_beder);
            }
            else
            {
                //TextBox_bedra.BackColor = Color.FromArgb(255, 255, 183);
                //TextBox_chss.BackColor = Color.FromArgb(255, 255, 183);
                //TextBox_dad.BackColor = Color.FromArgb(255, 255, 183);
                //TextBox_date.BackColor = Color.FromArgb(255, 255, 183);
                //TextBox_sad.BackColor = Color.FromArgb(255, 255, 183);
                //TextBox_taliya.BackColor = Color.FromArgb(255, 255, 183);
                //TextBox_weight.BackColor = Color.FromArgb(255, 255, 183);
            }


        }
    }
}