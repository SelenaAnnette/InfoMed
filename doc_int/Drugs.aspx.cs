using System;
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

    using System.Drawing;

    using Domain.Medicament;


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
        public Guid conType;
        public Guid wayType;
        public Guid doc_id = new Guid("E69188D9-ADC1-4757-AA4B-B5E97A518212");
        public Guid Guid_const;

        public IPersonConsultationRepository consultationRepo = Binder.NinjectKernel.Get<IPersonConsultationRepository>();
        public IConsultationTypeRepository consultationTypeRepo = Binder.NinjectKernel.Get<IConsultationTypeRepository>();
        public IPersonRepository personRepo = Binder.NinjectKernel.Get<IPersonRepository>();
        public IMedicamentRepository medicamentRepo = Binder.NinjectKernel.Get<IMedicamentRepository>();
        public IAssignedMedicamentRepository AssignedMedicamentRepo = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
        public AssignedMedicamentFactory AssignedMedicamentFactory = new AssignedMedicamentFactory();
        public PersonConsultationFactory PersonConsultationFactory = new PersonConsultationFactory();



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



            if (RadioButton3.Checked==true)
                conType = new Guid("C473F947-3564-4553-9548-5196DC491186");

            if (RadioButton4.Checked == true)
                conType = new Guid("1075A6AA-1945-433A-A485-97FE2561C066");

            if (RadioButton5.Checked == true)
                conType = new Guid("A05852A7-7670-421A-904D-C700EC77AAA4");

            if (RadioButton6.Checked == true)
                conType = new Guid("C07B9959-7D7F-4003-9586-E35E4FC41A3B");

            if (RadioButton7.Checked == true)
                wayType = new Guid("B7ECFDC8-C2E7-4B05-BE2A-9E2958CA64CB");

            if (RadioButton8.Checked == true)
                wayType = new Guid("D3A45C44-B85B-4E01-B366-07F5FD74CEB5");


            var today = DateTime.Today;


            var foundPerson = personRepo.GetAll().ToList();
                //personRepo.GetAll().ToList();
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
                TextBox3.Text = GridView1.Rows[num].Cells[6].Text;
                Panel2.Visible = true;
                Label3.Visible = true;
                Button3.Visible = true;
                TextBox10.Visible = true;
            }



        }

        public void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num2 = Convert.ToInt16(e.CommandArgument);
                TextBox2.Text = GridView2.Rows[num2].Cells[2].Text;
                TextBox4.Text = GridView2.Rows[num2].Cells[5].Text;

            }

        }

        public void Button1_Click(object sender, EventArgs e)
        {

            if (Session["Update"].ToString() == ViewState["Update"].ToString())
            {

                try
                {
                    if ((TextBox1.Text != "") && (TextBox2.Text != "") && (dosage.Text != "") && (choose_num.Text != "") && (dayCount.Text != "") && (Label1.Text != "") && (Convert.ToInt16(dosage.Text) > 0) && (Convert.ToInt16(choose_num.Text) > 0) && (Convert.ToInt16(dayCount.Text) > 0))
                    {


                        Guid_pat = new Guid(TextBox3.Text);
                        Guid_drug = new Guid(TextBox4.Text);
                        Guid_const = new Guid(TextBox10.Text);

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

                        var AssignedMedicament = AssignedMedicamentFactory.Create(Guid.NewGuid(), Guid_const, Guid_drug, wayType, Convert.ToDouble(dosage.Text), convertedDate, Convert.ToInt16(dayCount.Text), timesAtDay, eachDay);
                         AssignedMedicamentRepo.CreateOrUpdateEntity(AssignedMedicament);





                        Label2.Text = "Отправка данных прошла успешно.";
                        Label2.ForeColor = Color.Green;
                    }
                    else
                    {
                        Label2.Text = "Внимание. Была допущена ошибка при формировании выписки препарата. Проверьте все данные и повторите попытку.";
                    }


                   

                }
                catch (Exception u) { Label2.Text = u.Message; }

                {
                    Session["Update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }

                Button4.Enabled = true;


            }
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

            TextBox5.Text = dosage.Text;
            TextBox7.Text = dayCount.Text;
            TextBox8.Text = Convert.ToString(timesAtDay);
            TextBox9.Text = Convert.ToString(eachDay);

                TextBox5.Text = dosage.Text;                
                TextBox7.Text = dayCount.Text;
                TextBox8.Text = Convert.ToString(timesAtDay);
                TextBox9.Text = Convert.ToString(eachDay);

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < System.DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Guid conid = Guid.NewGuid();
            TextBox10.Text = Convert.ToString(conid);
            Guid_pat = new Guid(TextBox3.Text);
            var PersonConsultation = PersonConsultationFactory.Create(conid, Guid_pat, doc_id, conType, DateTime.Now);
            Button3.Enabled = false;

            consultationRepo.CreateOrUpdateEntity(PersonConsultation);



        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;

                Label3.Visible = false;
                Button3.Visible = false;
                Button3.Enabled = true;
                TextBox10.Visible = false;


        }




    }
}


