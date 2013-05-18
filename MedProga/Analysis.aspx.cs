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
        List<PersonMeasuring> personMeasuring = new List<PersonMeasuring>();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
            var parameters = measuringTypeRepo.GetAll();
            parameters = parameters.OrderBy(p => p.Title);
            var parametersArray = parameters.ToArray();
            for (int i = 0; i < parametersArray.Length; i++)
            {
                this.CheckBoxList_Parameters.Items.Add(parametersArray[i].Title);
            }
        }

        public void ShowData(string parName, string view)
        {
            DateTime dateFrom;
            DateTime dateTo;
            dateFrom = DateTime.MinValue;
            dateTo = DateTime.MaxValue;
            if (this.TextBox_from.Text != string.Empty)
            {
                try
                {
                    dateFrom = Convert.ToDateTime(this.TextBox_from.Text);
                    dateTo = Convert.ToDateTime(this.TextBox_to.Text);
                }
                catch (Exception)
                {

                }
            }
            var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRepo.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var measuringTypeRepo = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
            var measId = measuringTypeRepo.GetEntitiesByQuery(m => m.Title == parName).First().Id;
            if (view == "table")
            {
                var partOfPersonMeasuring =
                    personMeasuringRepo.GetEntitiesByQuery(
                        m =>
                        m.PersonId == perId && m.MeasuringDate >= dateFrom && m.MeasuringDate <= dateTo
                        && m.MeasuringTypeId == measId);
                partOfPersonMeasuring = partOfPersonMeasuring.OrderBy(m => m.MeasuringDate);
                personMeasuring.AddRange(partOfPersonMeasuring);
            }
            if (view == "chart")
            {
                var analysis =
                    personMeasuringRepo.GetEntitiesByQuery(
                        m =>
                        m.PersonId == perId && m.MeasuringDate >= dateFrom && m.MeasuringDate <= dateTo
                        && m.MeasuringTypeId == measId);
                analysis = analysis.OrderBy(a => a.MeasuringDate);
                var chartArray = analysis.ToArray();
                Series series = new Series(parName);
                series.ChartType = SeriesChartType.Line;
                series.MarkerStyle = MarkerStyle.Circle;
                series.IsValueShownAsLabel = true;
                series.BorderWidth = 1;
                for (int i = 0; i < chartArray.Length; i++)
                {
                    if (chartArray[i].MeasuringTypeId == measId)
                    {
                        series.Points.AddXY(chartArray[i].MeasuringDate, chartArray[i].Value);
                    }
                }
                if (series.Points.Count > 0)
                {
                    Chart_analysis.Series.Add(series);
                    ////Group values and calculate average value for every day
                    Chart_analysis.DataManipulator.Group("AVE", 1, IntervalType.Days, parName, parName);
                    //Add Legend
                    Legend legend = new Legend();
                    Chart_analysis.Legends.Add(legend);
                }
            }
        }


        protected void Button_analysis_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.CheckBoxList_Parameters.Items.Count; i++)
            {
                if (this.CheckBoxList_Parameters.Items[i].Selected)
                {
                    this.ShowData(this.CheckBoxList_Parameters.Items[i].Text, "table");
                    this.ShowData(this.CheckBoxList_Parameters.Items[i].Text, "chart");
                }
            }
            //var pM1 = from pM in personMeasuring select pM.MeasuringDate;
            //var pM2 = from pM in personMeasuring select pM.MeasuringTypeId;
            //var pM3 = from pM in personMeasuring select pM.Value;
            GridView_analysis.DataSource = personMeasuring.Select(pm => new {pm.MeasuringDate, pm.MeasuringTypeId, pm.Value} );
            GridView_analysis.DataBind();
            this.CheckBoxList_Parameters.Items.Clear();
            this.Page_Load(sender, e);
        }
    }
}