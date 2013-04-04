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


    public partial class Reminders : System.Web.UI.Page
    {
        //public string sql;
        //public DataTable dataTable;
        //public DataRow row;
        //public SqlDataAdapter dataAdapter;

        //public void zapros(string sql)
        //{
        //    try
        //    {
        //        StreamReader strrd = new StreamReader("S:/ВГТУ/5 курс/9 семестр/Диплом/Диплом1/MedProga/conn1.txt");
        //        string cs = strrd.ReadLine();
        //        SqlConnection conn = new SqlConnection(cs);
        //        conn.Open();
        //        SqlCommand command = new SqlCommand(sql, conn);
        //        dataAdapter = new SqlDataAdapter(command);
        //        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
        //        dataTable = new DataTable();
        //        dataAdapter.Fill(dataTable);
        //        conn.Close();
        //    }
        //    catch
        //    {

        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            var actualNotificationsRepo = Binder.NinjectKernel.Get<INotificationManager>();
            var personsRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var personId = personsRepo.GetEntitiesByQuery(p => p.LastName == "Glazunov").First().Id;
            var nots = actualNotificationsRepo.GetNotificationsForPerson(personId).ToArray();
            for (int i = 0; i < nots.Length; i++)
            {
                CheckBoxList1.Items.Add(nots[i].Text);
            }
        }

        
    }
}

