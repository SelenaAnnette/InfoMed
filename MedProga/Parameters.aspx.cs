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
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            //TextBox_date.Text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
            TextBox_date.Text=dt.ToString();
            //var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
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
            if (TextBox_date.Text != string.Empty)
            {
                var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
                var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
                var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
                var personMeasuringFactory = new PersonMeasuringFactory();
                var personId = personsRepo.GetEntitiesByQuery(p => p.LastName == "LastName").First().Id;
                ////Сохранение САД
                if (TextBox_sad.Text != string.Empty)
                {
                    try
                    {
                        var sadId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Систолическое АД").First().Id;
                        var sad = personMeasuringFactory.Create(Guid.NewGuid(), sadId, personId, DateTime.Now, 0);
                        sad.Value = Convert.ToDouble(TextBox_sad.Text);
                        personMeasuringRepo.CreateOrUpdateEntity(sad);
                    }
                    catch (ArgumentException)
                    {
                        TextBox_sad.BackColor = Color.FromArgb(255, 255, 183);
                        TextBox_sad.Text = string.Empty;
                    }
                }
                ////Сохранение ДАД
                if (TextBox_dad.Text != string.Empty)
                {
                    try
                    {
                        var dadId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Диастолическое АД").First().Id;
                        var dad = personMeasuringFactory.Create(Guid.NewGuid(), dadId, personId, DateTime.Now, 0);
                        dad.Value = Convert.ToDouble(TextBox_dad.Text);
                        personMeasuringRepo.CreateOrUpdateEntity(dad);
                    }
                    catch (ProviderIncompatibleException)
                    {
                        TextBox_dad.BackColor = Color.FromArgb(255, 255, 183);
                        TextBox_dad.Text = string.Empty;
                    }
                }
                ////Сохранение ЧСС
                if (TextBox_chss.Text != string.Empty)
                {
                    try
                    {
                        var chssId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Частота сердечных сокращений").First().Id;
                        var chss = personMeasuringFactory.Create(Guid.NewGuid(), chssId, personId, DateTime.Now, 0);
                        chss.Value = Convert.ToDouble(TextBox_chss.Text);
                        personMeasuringRepo.CreateOrUpdateEntity(chss);
                    }
                    catch (ProviderIncompatibleException)
                    {
                        TextBox_chss.BackColor = Color.FromArgb(255, 255, 183);
                        TextBox_chss.Text = string.Empty;
                    }
                }
                ////Сохранение Вес
                if (TextBox_weight.Text != string.Empty)
                {
                    try
                    {
                        var weightId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Вес").First().Id;
                        var weight = personMeasuringFactory.Create(Guid.NewGuid(), weightId, personId, DateTime.Now, 0);
                        weight.Value = Convert.ToDouble(TextBox_weight.Text);
                        personMeasuringRepo.CreateOrUpdateEntity(weight);
                    }
                    catch (ProviderIncompatibleException)
                    {
                        TextBox_weight.BackColor = Color.FromArgb(255, 255, 183);
                        TextBox_weight.Text = string.Empty;
                    }
                }
                ////Сохранение Окр_талии
                if (TextBox_taliya.Text != string.Empty)
                {
                    try
                    {
                        var taliyaId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Окружность талии").First().Id;
                        var taliya = personMeasuringFactory.Create(Guid.NewGuid(), taliyaId, personId, DateTime.Now, 0);
                        taliya.Value = Convert.ToDouble(TextBox_taliya.Text);
                        personMeasuringRepo.CreateOrUpdateEntity(taliya);
                    }
                    catch (ProviderIncompatibleException)
                    {
                        TextBox_taliya.BackColor = Color.FromArgb(255, 255, 183);
                        TextBox_taliya.Text = string.Empty;
                    }
                }
                ////Сохранение Окр_бедер
                if (TextBox_bedra.Text != string.Empty)
                {
                    try
                    {
                        var bedraId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == "Окружность бедер").First().Id;
                        var bedra = personMeasuringFactory.Create(Guid.NewGuid(), bedraId, personId, DateTime.Now, 0);
                        bedra.Value = Convert.ToDouble(TextBox_bedra.Text);
                        personMeasuringRepo.CreateOrUpdateEntity(bedra);
                    }
                    catch (ProviderIncompatibleException)
                    {
                        TextBox_bedra.BackColor = Color.FromArgb(255, 255, 183);
                        TextBox_bedra.Text = string.Empty;
                    }
                }
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