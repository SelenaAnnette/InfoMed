using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedProga
{
    using System.Collections;

    using DataLayer.Persistence.Measuring;
    using DataLayer.Persistence.Person;

    using Ninject;

    public partial class Analysis : System.Web.UI.Page
    {
        public string sql;
        public DataTable dataTable;
        public DataRow row;
        public SqlDataAdapter dataAdapter;

       protected void Page_Load(object sender, EventArgs e)
       {
           var personMeasuringRepo = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
           var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
           var perId = personsRepo.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
           var analysis = personMeasuringRepo.GetEntitiesByQuery(m => m.PersonId == perId);
           this.GridView_analysis.DataSource = analysis;
           this.GridView_analysis.DataBind();
       }
    }
}