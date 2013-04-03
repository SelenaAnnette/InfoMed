using System;
using System.Linq;
using System.Web.UI.WebControls;

using DataLayer.Persistence.Person;
using Ninject;
using DataLayer.Persistence.Medicament;
using System.ComponentModel;

namespace doc_int
{
<<<<<<< HEAD
=======
    using System.Drawing;

    using Domain.Medicament;

>>>>>>> 78d8078923953fe31862b01a6a4a3b07f5549873
    public partial class About : System.Web.UI.Page
    {

        public class GuidConverter : TypeConverter
        {

        }

        public int interval;
        public Guid patient_guid;
        public Guid drug_guid;
        public Guid Guid_pat = new Guid("11111111-1111-1111-1111-111111111111");
        public Guid Guid_drug = new Guid("11111111-1111-1111-1111-111111111111");
        public DateTime start;
        public int timesAtDay;
        public int eachDay;


        public IPersonRepository personRepo = Binder.NinjectKernel.Get<IPersonRepository>();
        public IMedicamentRepository medicamentRepo = Binder.NinjectKernel.Get<IMedicamentRepository>();
        public IAssignedMedicamentRepository AssignedMedicamentRepo = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
        public AssignedMedicamentFactory AssignedMedicamentFactory = new AssignedMedicamentFactory();

        protected void Page_Load(object sender, EventArgs e)
        {
            var today = DateTime.Today;


            var foundPerson = personRepo.GetAll().ToList();
            GridView1.DataSource = foundPerson;
            GridView1.DataBind();


            var getMedicament = medicamentRepo.GetAll().ToList();
            GridView2.DataSource = getMedicament;
            GridView2.DataBind();
            try
            {
                if (Convert.ToInt32(choose_num.Text) < 0)
                {
                    int zero = 0;
                    choose_num.Text = Convert.ToString(zero);
                }
            }
            catch (Exception o)
            {
                Label2.Text = o.Message;
            }
        }

        public void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num = Convert.ToInt16(e.CommandArgument);
                TextBox1.Text = GridView1.Rows[num].Cells[3].Text;
                TextBox3.Text = GridView1.Rows[num].Cells[4].Text;
            }



        }

        public void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num2 = Convert.ToInt16(e.CommandArgument);
                TextBox2.Text = GridView2.Rows[num2].Cells[2].Text;
                TextBox4.Text = GridView2.Rows[num2].Cells[4].Text;

            }

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((TextBox1.Text != "") && (TextBox2.Text != "") && (dosage.Text != "") && (choose_num.Text != "") && (dayCount.Text != "") && (Label1.Text != "") && (Convert.ToInt16(dosage.Text) > 0) && (Convert.ToInt16(choose_num.Text) > 0) && (Convert.ToInt16(dayCount.Text) > 0))
                {


                    Guid_pat = new Guid(TextBox3.Text);
                    Guid_drug = new Guid(TextBox4.Text);

                    if (RadioButton1.Checked == true)
                    {
                        timesAtDay = Convert.ToInt16(choose_num.Text);
                        eachDay = 1;
                    }

                    if (RadioButton2.Checked == true)
                    {
                        timesAtDay = 1;
                        eachDay = Convert.ToInt16(choose_num.Text); ;
                    }

                    DateTime convertedDate = DateTime.Parse(Label1.Text);
                    DateTime end = convertedDate.AddDays(Convert.ToInt32(dayCount.Text));
                    //var tomorrowNewTime = newTime.AddDays(1);



<<<<<<< HEAD
                    //var AssignedMedicament = AssignedMedicamentFactory.Create(Guid.NewGuid(), Guid_pat, Guid_drug, Convert.ToDouble(dosage.Text), "единиц", convertedDate, Convert.ToInt16(dayCount.Text), timesAtDay, eachDay);
                    var AssignedMedicament = AssignedMedicamentFactory.Create(Guid.NewGuid(), Guid_pat, Guid_drug, Convert.ToDouble(dosage.Text), "единиц", convertedDate, Convert.ToInt16(dayCount.Text), timesAtDay, eachDay);
=======
                   
                    var AssignedMedicament = AssignedMedicamentFactory.Create(Guid.NewGuid(), Guid_pat, Guid_drug, Convert.ToDouble(dosage.Text), "единиц", convertedDate, Convert.ToInt16(dayCount.Text), timesAtDay, eachDay);                     
>>>>>>> 78d8078923953fe31862b01a6a4a3b07f5549873
                    AssignedMedicamentRepo.CreateOrUpdateEntity(AssignedMedicament);

                    Label2.Text = "Отправка данных прошла успешно.";
                }
                else
                {
                    Label2.Text = "Внимание. Была допущена ошибка при формировании выписки препарата. Проверьте все данные и повторите попытку.";
                }
            }
            catch (Exception u) { Label2.Text = u.Message; }


        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Label1.Text = Calendar1.SelectedDate.ToShortDateString();
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                timesAtDay = Convert.ToInt16(choose_num.Text);
                eachDay = 1;
            }

            if (RadioButton2.Checked == true)
            {
                timesAtDay = 1;
                eachDay = Convert.ToInt16(choose_num.Text); ;
            }
            try
            {
                DateTime convertedDate = DateTime.Parse(Label1.Text);
                TextBox6.Text = Convert.ToString(convertedDate);
            }
            catch { }
<<<<<<< HEAD
            TextBox5.Text = dosage.Text;
            TextBox7.Text = dayCount.Text;
            TextBox8.Text = Convert.ToString(timesAtDay);
            TextBox9.Text = Convert.ToString(eachDay);
=======
                TextBox5.Text = dosage.Text;                
                TextBox7.Text = dayCount.Text;
                TextBox8.Text = Convert.ToString(timesAtDay);
                TextBox9.Text = Convert.ToString(eachDay);
>>>>>>> 78d8078923953fe31862b01a6a4a3b07f5549873
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < System.DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
        }
<<<<<<< HEAD

=======
>>>>>>> 78d8078923953fe31862b01a6a4a3b07f5549873
    }
}


