using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            //TextBox_date.Text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
            TextBox_date.Text=dt.ToString();
            var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
        }

       //public void saving(string personSurname, string parName, TextBox tb)
        //{
        //    var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
        //    var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
        //    var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
        //    var personMeasuringFactory = new PersonMeasuringFactory();
        //    var personId = personsRepo.GetEntitiesByQuery(p => p.LastName == personSurname).First().Id;
        //    var parId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == parName).First().Id;
        //    var parameter = personMeasuringFactory.Create(Guid.NewGuid(), parId, personId, DateTime.Now, 0);
        //    parameter.Value = Convert.ToDouble(tb.Text);
        //    personMeasuringRepo.CreateOrUpdateEntity(sad);
        //}
        
        public void Button_parameters_Click(object sender, EventArgs e)
        {
            if ((TextBox_date.Text != "") && (TextBox_sad.Text != "") && (TextBox_dad.Text != "")
                && (TextBox_chss.Text != "") && (TextBox_weight.Text != "") && (TextBox_bedra.Text != "")
                && (TextBox_taliya.Text != ""))
            {
                var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
                var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
                var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
                var personMeasuringFactory = new PersonMeasuringFactory();
                var personId = personsRepo.GetEntitiesByQuery(p => p.LastName == "LastName").First().Id;
                ////Сохранение САД
                var sadId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Систолическое АД").First().Id;
                var sad = personMeasuringFactory.Create(Guid.NewGuid(), sadId, personId, DateTime.Now, 0);
                sad.Value = Convert.ToDouble(TextBox_sad.Text);
                personMeasuringRepo.CreateOrUpdateEntity(sad);
                ////Сохранение ДАД
                var dadId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Диастолическое АД").First().Id;
                var dad = personMeasuringFactory.Create(Guid.NewGuid(), dadId, personId, DateTime.Now, 0);
                dad.Value = Convert.ToDouble(TextBox_dad.Text);
                personMeasuringRepo.CreateOrUpdateEntity(dad);
                ////Сохранение ЧСС
                var chssId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Частота сердечных сокращений").First().Id;
                var chss = personMeasuringFactory.Create(Guid.NewGuid(), chssId, personId, DateTime.Now, 0);
                chss.Value = Convert.ToDouble(TextBox_chss.Text);
                personMeasuringRepo.CreateOrUpdateEntity(chss);
                ////Сохранение Вес
                var weightId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Вес").First().Id;
                var weight = personMeasuringFactory.Create(Guid.NewGuid(), weightId, personId, DateTime.Now, 0);
                weight.Value = Convert.ToDouble(TextBox_weight.Text);
                personMeasuringRepo.CreateOrUpdateEntity(weight);
                ////Сохранение Окр_талии
                var taliyaId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Окружность талии").First().Id;
                var taliya = personMeasuringFactory.Create(Guid.NewGuid(), taliyaId, personId, DateTime.Now, 0);
                taliya.Value = Convert.ToDouble(TextBox_taliya.Text);
                personMeasuringRepo.CreateOrUpdateEntity(taliya);
                ////Сохранение Окр_бедер
                var bedraId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Окружность бедер").First().Id;
                var bedra = personMeasuringFactory.Create(Guid.NewGuid(), bedraId, personId, DateTime.Now, 0);
                bedra.Value = Convert.ToDouble(TextBox_bedra.Text);
                personMeasuringRepo.CreateOrUpdateEntity(bedra);
            }


        }
    }
}