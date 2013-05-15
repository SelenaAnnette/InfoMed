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
using System.Web.UI.DataVisualization.Charting;

namespace MedProga
{
    using System.Collections;
    using DataLayer.Persistence.Measuring;
    using DataLayer.Persistence.Person;
    using Domain.Measuring;
    using Ninject;

    public partial class Analysis : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckBoxList_Parameters.Items.Add("Систолическое АД");
            this.CheckBoxList_Parameters.Items.Add("Диастолическое АД");
            this.CheckBoxList_Parameters.Items.Add("Частота сердечных сокращений");
            this.CheckBoxList_Parameters.Items.Add("Вес");
            this.CheckBoxList_Parameters.Items.Add("Окружность талии");
            this.CheckBoxList_Parameters.Items.Add("Окружность бедер");
        }

        public void ShowData(string parName, string view)
        {
            DateTime dateFrom;
            DateTime dateTo;
            if (this.TextBox_from.Text != string.Empty)
            {
                try
                {
                    dateFrom = Convert.ToDateTime(this.TextBox_from.Text);
                    dateTo = Convert.ToDateTime(this.TextBox_to.Text);
                }
                catch (Exception)
                {
                    dateFrom = DateTime.MinValue;
                    dateTo = DateTime.MaxValue;
                    this.TextBox_from.Text = string.Empty;
                    this.TextBox_to.Text = string.Empty;
                }
            }
            else
            {
                dateFrom = DateTime.MinValue;
                dateTo = DateTime.MaxValue;
            }
            var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRepo.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
            var measId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == parName).First().Id;
            if (view == "table")
            {
                List <PersonMeasuring> personMeasuring = new List<PersonMeasuring>();
                for (int i = 0; i < this.CheckBoxList_Parameters.Items.Count; i++)
                {
                    if (this.CheckBoxList_Parameters.Items[i].Selected)
                    {
                        measId =
                            measuringTypeRepo.GetEntitiesByQuery(
                                m => m.Title == this.CheckBoxList_Parameters.Items[i].Text).First().Id;
                        var partOfPersonMeasuring =
                            personMeasuringRepo.GetEntitiesByQuery(
                                m =>
                                m.PersonId == perId && m.MeasuringDate >= dateFrom && m.MeasuringDate <= dateTo
                                && m.MeasuringTypeId == measId);
                        partOfPersonMeasuring = partOfPersonMeasuring.OrderBy(p => p.MeasuringDate);
                        personMeasuring.AddRange(partOfPersonMeasuring);
                    }
                }
                //var meas = measuringTypeRepo.GetAll();
                //var table = personMeasuring.Join(meas, m => m.MeasuringTypeId, t => t.Id, (measuring, type) => Equals(measuring,type));
                this.GridView_analysis.DataSource = personMeasuring;
                this.GridView_analysis.DataBind();
            }
            if (view == "chart")
            {
                var analysis = personMeasuringRepo.GetEntitiesByQuery(m => m.PersonId == perId && m.MeasuringDate >= dateFrom && m.MeasuringDate <= dateTo && m.MeasuringTypeId == measId);
                analysis = analysis.OrderBy(a => a.MeasuringDate);
                var chartArray = analysis.ToArray();
                Series series = new Series(parName);
                series.ChartType = SeriesChartType.Line;
                series.MarkerStyle = MarkerStyle.Circle;
                series.IsValueShownAsLabel = true;
                series.BorderWidth = 1;
                Legend parLegend = new Legend(parName);
                this.Chart_analysis.Legends.Add(parLegend);
                for (int i = 0; i < chartArray.Length; i++)
                {
                    if (chartArray[i].MeasuringTypeId == measId)
                    {
                        series.Points.AddXY(chartArray[i].MeasuringDate, chartArray[i].Value);
                    }
                }
                this.Chart_analysis.Series.Add(series);
                ////Group values and calculate average value for every day
                this.Chart_analysis.DataManipulator.Group("AVE", 1, IntervalType.Days, parName, parName);
            }
        }

        protected void Button_show_table_Click(object sender, EventArgs e)
        {
            this.ShowData("Вес", "table");
            this.CheckBoxList_Parameters.Items.Clear();
            this.Page_Load(sender, e);
        }

       protected void Button_create_chart_Click(object sender, EventArgs e)
        {
            for (int i=0; i < this.CheckBoxList_Parameters.Items.Count; i++)
            {
                if (this.CheckBoxList_Parameters.Items[i].Selected)
                {
                    this.ShowData(this.CheckBoxList_Parameters.Items[i].Text, "chart");
                }
            }
            this.CheckBoxList_Parameters.Items.Clear();
            this.Page_Load(sender, e);
        }

    }
}