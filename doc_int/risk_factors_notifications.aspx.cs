﻿using System;
using System.Linq;
using System.Web.UI.WebControls;

using DataLayer.Persistence.Person;
using Ninject;
using DataLayer.Persistence.Medicament;
using DataLayer.Persistence.Consultation;
using System.ComponentModel;

using System.Web;
using System.Web.SessionState;

namespace doc_int
{    
    using Domain.Medicament;
    using System.Collections;
    using DataLayer.Persistence.RiskFactor;
    using DataLayer.Persistence.Person;
    using Ninject;
    

    public partial class risk_factors_notifications : System.Web.UI.Page
    {
        Guid id_risk_factor;
        public IPersonRepository personRepo = Binder.NinjectKernel.Get<IPersonRepository>();
        public IRiskFactorRepository RiskFactorRepository = Binder.NinjectKernel.Get<IRiskFactorRepository>();
        

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["Update"] = Session["Update"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            
        }

        public void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num2 = Convert.ToInt16(e.CommandArgument);
                TextBox1.Text = GridView1.Rows[num2].Cells[6].Text;

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var parameters = RiskFactorRepository.GetAll();
            parameters = parameters.OrderBy(p => p.Title);
            var parametersArray = parameters.ToArray();
            for (int i = 0; i < parametersArray.Length; i++)
            {
                this.CheckBoxList_Parameters.Items.Add(parametersArray[i].Title);
            }

            var foundPerson = personRepo.GetAll().ToList();
            GridView1.DataSource = foundPerson;
            GridView1.DataBind();

            Button1.Enabled = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            //var list = new List<RiskFactor>()

            for (int i = 0; i < this.CheckBoxList_Parameters.Items.Count; i++)
            {
                if (this.CheckBoxList_Parameters.Items[i].Selected)
                {
                    id_risk_factor = RiskFactorRepository.GetEntitiesByQuery(mi => mi.Title == CheckBoxList_Parameters.Items[i].Text).First().Id;                    
                }
            }
        }
    }
}