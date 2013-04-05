using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataLayer.Persistence.Person;
using Ninject;
using ServerLogic.Notification;

namespace MedProga
{
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;

    public partial class Reminders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckBoxList1.Items.Clear();
            var actualNotificationsRepo = Binder.NinjectKernel.Get<INotificationManager>();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var personId = personsRepo.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var nots = actualNotificationsRepo.GetNotificationsForPerson(personId).ToArray();
            for (int i = 0; i < nots.Length; i++)
            {
                this.CheckBoxList1.Items.Add(nots[i].Text);
            }
        }

        protected void Button_save_Click(object sender, EventArgs e)
        {
            for (int i=0; i < this.CheckBoxList1.Items.Count; i++)
            {
                if (this.CheckBoxList1.Items[i].Selected)
                {
                    var nots = Binder.NinjectKernel.Get<INotificationRepository>();
                    var medId = nots.GetEntitiesByQuery(n => n.Text == this.CheckBoxList1.Items[i].Text).First().MedicamentId;
                    var personId = nots.GetEntitiesByQuery(n => n.Text == this.CheckBoxList1.Items[i].Text).First().PersonId;
                    var personMedsRepo = Binder.NinjectKernel.Get<IPersonMedicamentRepository>();
                    var personMedsFac = new PersonMedicamentFactory();
                    var perMed = personMedsFac.Create(Guid.NewGuid(), medId, personId, DateTime.Now);
                    personMedsRepo.CreateOrUpdateEntity(perMed);
                }
            }
        }

        protected void Button_close_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
            {
                if (this.CheckBoxList1.Items[i].Selected)
                {
                    var nots = Binder.NinjectKernel.Get<INotificationRepository>();
                    var notId = nots.GetEntitiesByQuery(n => n.Text == this.CheckBoxList1.Items[i].Text).First().Id;
                    var closeNots = Binder.NinjectKernel.Get<INotificationManager>();
                    closeNots.CloseNotificationById(notId);
                }
            }
        }

        
    }
}

