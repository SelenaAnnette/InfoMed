using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
    public partial class patient_info_input : System.Web.UI.Page
    {
        public Guid Guid_pat = new Guid("11111111-1111-1111-1111-111111111111");
        public IPersonConsultationRepository consultationRepo = Binder.NinjectKernel.Get<IPersonConsultationRepository>();
        public IConsultationTypeRepository consultationTypeRepo = Binder.NinjectKernel.Get<IConsultationTypeRepository>();
        public IPersonRepository personRepo = Binder.NinjectKernel.Get<IPersonRepository>();
        public IMedicamentRepository medicamentRepo = Binder.NinjectKernel.Get<IMedicamentRepository>();
        public IAssignedMedicamentRepository AssignedMedicamentRepo = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
        public AssignedMedicamentFactory AssignedMedicamentFactory = new AssignedMedicamentFactory();
        public PersonConsultationFactory PersonConsultationFactory = new PersonConsultationFactory();
        public IContactTypeRepository ContactTypeRepo = Binder.NinjectKernel.Get<IContactTypeRepository>();
        public IPersonContactRepository PersonContactRepo = Binder.NinjectKernel.Get<IPersonContactRepository>();
        public string[] a = new string[10];

        public Guid Guid_mobile;
        public Guid Guid_phone;
        public Guid Guid_pcard;
        public Guid Guid_email;
        public Guid Guid_address;
        public Guid Guid_mpnum;





        public Guid o;



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



            var foundPerson = personRepo.GetAll().ToList();
           
            GridView1.DataSource = foundPerson;
            GridView1.DataBind();








        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {

                    Int16 num1 = Convert.ToInt16(e.CommandArgument);
                   
                    TextBox18.Text = GridView1.Rows[num1].Cells[6].Text;

                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox16.Text = "";
                    TextBox17.Text = "";
                    TextBox19.Text = "";
                    TextBox20.Text = "";
                    TextBox21.Text = "";
                    GridView2.DataSource = "";
                    GridView3.DataSource = "";
                    GridView4.DataSource = "";

                    Guid_pat = new Guid(TextBox18.Text);


                    
                }
                catch (Exception m) { }

            }


               if (e.CommandName == "Select")
            {
               

                    Int16 num1 = Convert.ToInt16(e.CommandArgument);
                   
                    TextBox18.Text = GridView1.Rows[num1].Cells[6].Text;

                    Guid_mobile = ContactTypeRepo.GetEntitiesByQuery(p => p.Title == "Mobile").First().Id;
                    Guid_phone = ContactTypeRepo.GetEntitiesByQuery(ph => ph.Title == "Phone").First().Id;
                    Guid_pcard = ContactTypeRepo.GetEntitiesByQuery(pc => pc.Title == "Patient card number").First().Id;
                    Guid_email = ContactTypeRepo.GetEntitiesByQuery(em => em.Title == "Email").First().Id;
                    Guid_address = ContactTypeRepo.GetEntitiesByQuery(ad => ad.Title == "Address").First().Id;
                    Guid_mpnum = ContactTypeRepo.GetEntitiesByQuery(mp => mp.Title == "Medical insurance policy").First().Id;

                    TextBox2.Text = personRepo.GetEntitiesByQuery(p => p.Id == Guid_pat).First().LastName;
                    TextBox16.Text = personRepo.GetEntitiesByQuery(p => p.Id == Guid_pat).First().FirstName;
                    TextBox17.Text = personRepo.GetEntitiesByQuery(p => p.Id == Guid_pat).First().MiddleName;


                    try
                    {
                        TextBox3.Text = PersonContactRepo.GetEntitiesByQuery(mob => mob.ContactTypeId == Guid_mobile && mob.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        TextBox19.Text = PersonContactRepo.GetEntitiesByQuery(pho => pho.ContactTypeId == Guid_phone && pho.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        TextBox4.Text = PersonContactRepo.GetEntitiesByQuery(em => em.ContactTypeId == Guid_email && em.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        TextBox5.Text = PersonContactRepo.GetEntitiesByQuery(adr => adr.ContactTypeId == Guid_address && adr.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        TextBox20.Text = PersonContactRepo.GetEntitiesByQuery(pd => pd.ContactTypeId == Guid_pcard && pd.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        TextBox21.Text = PersonContactRepo.GetEntitiesByQuery(mpn => mpn.ContactTypeId == Guid_mpnum && mpn.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }

                   try
                     {
                       GridView2.DataSource = personRepo.GetEntityById(Guid_pat).PersonHospitalizations;
                     }
                    catch (Exception m)
                    { }
                   try
                     {
                       GridView3.DataSource = personRepo.GetEntityById(Guid_pat).PersonDiseases;
                     }
                    catch (Exception m)
                    { }
                   try
                    {
                       GridView4.DataSource = personRepo.GetEntityById(Guid_pat).PersonOperations;
                    }
                    catch (Exception m)
                    { }
               

            }
        }

       

    }
}