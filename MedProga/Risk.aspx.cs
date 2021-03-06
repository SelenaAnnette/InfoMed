﻿using System;
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
        private int quantity = 0;
        private Label label;
        private TextBox tb;
        public void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                var riskFactorRepo = Binder.NinjectKernel.Get<IRiskFactorRepository>();
                var riskFactors = riskFactorRepo.GetAll();
                riskFactors = riskFactors.OrderBy(rf => rf.Title);
                var riskFactorsArray = riskFactors.ToArray();
                this.label = new Label();
                this.tb = new TextBox();
                if (riskFactorsArray.Count() != 0)
                {
                    this.label.ID = "Label_date_time";
                    this.label.Text = "Введите дату и время";
                    this.tb.ID = "TextBox_date_time";
                    this.tb.MaxLength = 19;
                    this.PlaceHolder_risk.Controls.Add(this.label);
                    this.PlaceHolder_risk.Controls.Add(new LiteralControl("<br />"));
                    this.PlaceHolder_risk.Controls.Add(this.tb);
                }
                for (int i = 0; i < riskFactorsArray.Count(); i++)
                {
                    this.label = new Label();
                    this.label.ID = "Label" + i;
                    this.label.Text = riskFactorsArray[i].Title;
                    this.tb = new TextBox();
                    this.tb.ID = "TextBox" + i;
                    this.tb.MaxLength = 3;
                    if (riskFactorsArray[i].Title == "Крепость принятого алкоголя") this.tb.MaxLength = 2;
                    if (riskFactorsArray[i].Title == "Количество поглощенных с пищей калорий") this.tb.MaxLength = 4;
                    this.tb.ToolTip = "Введите значение для данного фактора риска";
                    this.quantity += 1;
                    this.PlaceHolder_risk.Controls.Add(new LiteralControl("<br />"));
                    this.PlaceHolder_risk.Controls.Add(this.label);
                    this.PlaceHolder_risk.Controls.Add(new LiteralControl("<br />"));
                    this.PlaceHolder_risk.Controls.Add(this.tb);
                }
            }
            catch (Exception)
            {
                this.label.ID = "Label_default";
                this.label.Text = "Соединение с базой данных установить не удалось";
                this.PlaceHolder_risk.Controls.Add(this.label);
            }
        }

        protected void Button_risk_Click(object sender, EventArgs e)
        {
            int countRF = 0;
            int countSavedRF = 0;
            DateTime dt = new DateTime();
            string personSurname = "Glazunov";
            var riskFactorRep = Binder.NinjectKernel.Get<IRiskFactorRepository>();
            var personsRep = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRep.GetEntitiesByQuery(p => p.LastName == personSurname).First().Id;
            var personRiskFactorRep = Binder.NinjectKernel.Get<IPersonRiskFactorRepository>();
            var personRiskFactorFac = new PersonRiskFactorFactory();
            try
            {
                this.tb = ((TextBox)PlaceHolder_risk.FindControl("TextBox_date_time"));
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
                this.tb = (TextBox)PlaceHolder_risk.FindControl("TextBox" + i);
                //Search label by ID
                this.label = (Label)PlaceHolder_risk.FindControl("Label" + i);
                if (this.tb.Text != string.Empty)
                {
                    try
                    {
                        var riskFacId = riskFactorRep.GetEntitiesByQuery(rf => rf.Title == label.Text).First().Id;
                        double riskFactorValue = Convert.ToDouble(tb.Text);
                        var riskFactor = personRiskFactorFac.Create(
                            Guid.NewGuid(), perId, riskFacId, riskFactorValue, dt);
                        personRiskFactorRep.CreateOrUpdateEntity(riskFactor);
                        countSavedRF += 1;
                        this.tb.BackColor = Color.FromArgb(255, 255, 255);
                        this.tb.ToolTip = "Введите значение для данного фактора риска";
                    }
                    catch (Exception)
                    {
                        tb.Text = string.Empty;
                        tb.BackColor = Color.FromArgb(255, 255, 183);
                        this.tb.ToolTip = "Введены данные неверного типа";
                    }
                    countRF += 1;
                }
                Label labelSave = (Label)Master.FindControl("Label_save");
                if (countRF > 0)
                {
                    if (countSavedRF == countRF)
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

        protected void Button_clear_risk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.quantity; i++)
            {
                this.tb = (TextBox)PlaceHolder_risk.FindControl("TextBox" + i);
                this.tb.BackColor = Color.FromArgb(255, 255, 255);
                this.tb.Text = string.Empty;
            }
        }
    }
}