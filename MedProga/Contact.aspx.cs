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

namespace MedProga
{
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Person;

    using Ninject;

    public partial class Contact : System.Web.UI.Page
    {
        protected void Button_contact_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            var personsRep = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRep.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var contactRep = Binder.NinjectKernel.Get<IMessageRepository>();
            var contactFac = new MessageFactory();
            if (this.TextBox_contact.Text != string.Empty)
            {
                var contactMes = contactFac.Create(Guid.NewGuid(), perId, Guid.NewGuid(), this.TextBox_contact.Text,dt);
                contactRep.CreateOrUpdateEntity(contactMes);
                Label labelSave = (Label)Master.FindControl("Label_save");
                labelSave.Text = "Сообщение успешно отправлено";
                //Color of text
                labelSave.ForeColor = Color.FromArgb(0, 144, 36);
            }
        }
    }
}