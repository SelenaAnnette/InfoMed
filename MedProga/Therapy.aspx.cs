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
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Person;

    using Ninject;

    public partial class Therapy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            this.TextBox_date_time.Text = dt.ToString();
            var assignedMedRep = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
            var personsRep = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRep.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var assignedMed = assignedMedRep.GetEntitiesByQuery(p => p.PersonId == perId);
            this.GridView_drugs.DataSource = assignedMed;
            this.GridView_drugs.DataBind();
        }
    }

}
