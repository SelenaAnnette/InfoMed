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

using DataLayer.Persistence.Person;
using Ninject;
using ServerLogic.Notification;

namespace MedProga
{
    using DataLayer.Persistence.Medicament;

    public partial class Reminders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label_reminders.Text = "Напоминания отсутствуют";
            var actualNotificationsRepo = Binder.NinjectKernel.Get<INotificationManager>();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRepo.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var nots = actualNotificationsRepo.GetNotificationsForPerson(perId).ToArray();
            for (int i = 0; i < nots.Length; i++)
            {
                    this.CheckBoxList_nots.Items.Add(nots[i].Text);
            }
            if (nots.Length > 0) Label_reminders.Text = "Напоминания";
        }
        
        protected void Button_save_Click(object sender, EventArgs e)
        {
            var actualNotificationsRep = Binder.NinjectKernel.Get<INotificationManager>();
            var personsRep = Binder.NinjectKernel.Get<IPersonRepository>();
            var perId = personsRep.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var nots = actualNotificationsRep.GetNotificationsForPerson(perId).ToArray();
            var personMedsRep = Binder.NinjectKernel.Get<IPersonMedicamentRepository>();
            var personMedsFac = new PersonMedicamentFactory();
            for (int i = 0; i < this.CheckBoxList_nots.Items.Count; i++)
            {
                if (this.CheckBoxList_nots.Items[i].Selected)
                {
                    var medId = nots[i].MedicamentId;
                    var notId = nots[i].Id;
                    var personMed = personMedsFac.Create(Guid.NewGuid(), medId, perId, DateTime.Now);
                    personMedsRep.CreateOrUpdateEntity(personMed);
                    actualNotificationsRep.CloseNotificationById(notId);
                    Label labelSave = (Label)Master.FindControl("Label_save");
                    labelSave.Text = "Сохранение прошло успешно";
                    //Color of text
                    labelSave.ForeColor = Color.FromArgb(0, 144, 36);
                }

           }
            this.CheckBoxList_nots.Items.Clear();
            this.Page_Load(sender, e);
       }

        protected void Button_deselect_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.CheckBoxList_nots.Items.Count; i++)
            {
                if (this.CheckBoxList_nots.Items[i].Selected)
                {
                    this.CheckBoxList_nots.Items[i].Selected = false;
                }
            }
            this.CheckBoxList_nots.Items.Clear();
            this.Page_Load(sender, e);
        }
    }
}

