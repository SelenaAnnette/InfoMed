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
        private int quantity = 0;
        private Label label;
        private TextBox tb;

        public void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
                var parameters = measuringTypeRepo.GetAll();
                parameters = parameters.OrderBy(p => p.Title);
                var parametersArray = parameters.ToArray();
                this.label = new Label();
                this.tb = new TextBox();
                if (parametersArray.Count() != 0)
                {
                    this.label.ID = "Label_date_time";
                    this.label.Text = "Введите дату и время измерений";
                    this.tb.ID = "TextBox_date_time";
                    this.tb.MaxLength = 19;
                    this.PlaceHolder_parameters.Controls.Add(this.label);
                    this.PlaceHolder_parameters.Controls.Add(this.tb);
                }
                for (int i = 0; i < parametersArray.Count(); i++)
                {
                    this.label = new Label();
                    this.label.ID = "Label" + i;
                    this.label.Text = parametersArray[i].Title;
                    this.tb = new TextBox();
                    this.tb.ID = "TextBox" + i;
                    this.tb.MaxLength = 3;
                    this.tb.ToolTip = "Введите результаты измерений";
                    this.quantity += 1;
                    this.PlaceHolder_parameters.Controls.Add(new LiteralControl("<br />"));
                    this.PlaceHolder_parameters.Controls.Add(this.label);
                    this.PlaceHolder_parameters.Controls.Add(this.tb);
                }
            }
            catch (Exception)
            {
                this.label.ID = "Label_default";
                this.label.Text = "Соединение с базой данных установить не удалось";
                this.PlaceHolder_parameters.Controls.Add(this.label);
            }
        }

        public void Button_parameters_Click(object sender, EventArgs e)
        {
            int countPars = 0;
            int countSavedPars = 0;
            DateTime dt = new DateTime();
            string personSurname = "Glazunov";
            var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
            var personMeasuringFactory = new PersonMeasuringFactory();
            var personId = personsRepo.GetEntitiesByQuery(p => p.LastName == personSurname).First().Id;
            try
            {
                this.tb = ((TextBox)PlaceHolder_parameters.FindControl("TextBox_date_time"));
                dt = Convert.ToDateTime(this.tb.Text);
                this.tb.Text = Convert.ToString(dt);
                if (dt > DateTime.Now)
                {
                    this.tb.Text = string.Empty;
                    dt = DateTime.Now;
                }
                this.tb.Text = Convert.ToString(dt);
            }
            catch (Exception)
            {
                dt = DateTime.Now;
                this.tb.Text = Convert.ToString(dt);
            }
            for (int i = 0; i < this.quantity; i++)
            {
                //Search textBox by ID
                this.tb = (TextBox)PlaceHolder_parameters.FindControl("TextBox" + i);
                //Search label by ID
                this.label = (Label)PlaceHolder_parameters.FindControl("Label" + i);
                if (this.tb.Text != string.Empty)
                {
                    try
                    {
                        string parName = this.label.Text;
                        var parId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == parName).First().Id;
                        double parValue = Convert.ToDouble(this.tb.Text);
                        var par = personMeasuringFactory.Create(Guid.NewGuid(), parId, personId, dt, parValue);
                        personMeasuringRepo.CreateOrUpdateEntity(par);
                        countSavedPars += 1;
                        this.tb.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    catch (Exception)
                    {
                        this.tb.BackColor = Color.FromArgb(255, 255, 183);
                        this.tb.Text = string.Empty;
                    }
                    countPars += 1;
                }
                Label labelSave = (Label)Master.FindControl("Label_save");
                if (countPars > 0)
                {
                    if (countSavedPars == countPars)
                    {
                        labelSave.Text = "Сохранение прошло успешно";
                        //Color of text
                        labelSave.ForeColor = Color.FromArgb(0, 144, 36);
                    }
                    else
                    {
                        labelSave.Text = "Сохранены не все введенные результаты измерений";
                        //Color of text
                        labelSave.ForeColor = Color.FromArgb(105, 105, 105);
                    }
                }
            }
        }

        public void Button_clear_parameters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.quantity; i++)
            {
                this.tb = (TextBox)PlaceHolder_parameters.FindControl("TextBox" + i);
                this.tb.BackColor = Color.FromArgb(255, 255, 255);
                this.tb.Text = string.Empty;
            }
        }
    }
}