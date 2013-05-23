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
        public PersonFactory PersonFactory = new PersonFactory();
        public PersonContactFactory PersonContactFactory = new PersonContactFactory();
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

                    LastNameBox.Text = "";
                    MobileBox.Text = "";
                    EmailBox.Text = "";
                    AddressBox.Text = "";
                    FirstNameBox.Text = "";
                    MiddleNameBox.Text = "";
                    PhoneBox.Text = "";
                    PatientCardNumBox.Text = "";
                    InsuranceBox.Text = "";
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
                    

                    LastNameBox.Text = personRepo.GetEntitiesByQuery(p => p.Id == Guid_pat).First().LastName;
                    FirstNameBox.Text = personRepo.GetEntitiesByQuery(p => p.Id == Guid_pat).First().FirstName;
                    MiddleNameBox.Text = personRepo.GetEntitiesByQuery(p => p.Id == Guid_pat).First().MiddleName;


                    try
                    {
                        MobileBox.Text = PersonContactRepo.GetEntitiesByQuery(mob => mob.ContactTypeId == Guid_mobile && mob.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        PhoneBox.Text = PersonContactRepo.GetEntitiesByQuery(pho => pho.ContactTypeId == Guid_phone && pho.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        EmailBox.Text = PersonContactRepo.GetEntitiesByQuery(em => em.ContactTypeId == Guid_email && em.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        AddressBox.Text = PersonContactRepo.GetEntitiesByQuery(adr => adr.ContactTypeId == Guid_address && adr.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        PatientCardNumBox.Text = PersonContactRepo.GetEntitiesByQuery(pd => pd.ContactTypeId == Guid_pcard && pd.PersonId == Guid_pat).First().Value;
                    }
                    catch (Exception m)
                    { }
                    try
                    {
                        InsuranceBox.Text = PersonContactRepo.GetEntitiesByQuery(mpn => mpn.ContactTypeId == Guid_mpnum && mpn.PersonId == Guid_pat).First().Value;
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
                   Button1.Enabled = true;

            }
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            Button1.Enabled = false;
            Button2.Enabled = true;
            GridView1.Enabled = false;
           
            PhoneBox0.Visible = true;
            PatientCardNumBox0.Visible = true;
            InsuranceBox0.Visible = true;
            
            MobileBox0.Visible = true;
            EmailBox0.Visible = true;
            AddressBox0.Visible = true;

            
            PhoneBox0.Text = PhoneBox.Text;
            PatientCardNumBox0.Text = PatientCardNumBox.Text;
            InsuranceBox0.Text = InsuranceBox.Text;
            
            MobileBox0.Text = MobileBox.Text;
            EmailBox0.Text = EmailBox.Text;
            AddressBox0.Text = AddressBox.Text;

            
        }

        public void Button2_Click(object sender, EventArgs e)
        {
           
            Button1.Enabled = true;
            Button2.Enabled = false;
            GridView1.Enabled = true;

            
            Guid_pat = new Guid(TextBox18.Text);
            Guid_mobile = ContactTypeRepo.GetEntitiesByQuery(p => p.Title == "Mobile").First().Id;
            Guid_phone = ContactTypeRepo.GetEntitiesByQuery(ph => ph.Title == "Phone").First().Id;
            Guid_pcard = ContactTypeRepo.GetEntitiesByQuery(pc => pc.Title == "Patient card number").First().Id;
            Guid_email = ContactTypeRepo.GetEntitiesByQuery(em => em.Title == "Email").First().Id;
            Guid_address = ContactTypeRepo.GetEntitiesByQuery(ad => ad.Title == "Address").First().Id;
            Guid_mpnum = ContactTypeRepo.GetEntitiesByQuery(mp => mp.Title == "Medical insurance policy").First().Id;

            Guid Guid_person_mobile_id;
            Guid Guid_person_phone_id;
            Guid Guid_person_address_id;
            Guid Guid_person_pcard_id;
            Guid Guid_person_mpnum_id;
            Guid Guid_person_email_id;




            //условие: пустой ли мобильный телефон. назначение нового ID
            if (MobileBox0.Text != MobileBox.Text)
            {
                if (MobileBox.Text == "" || MobileBox.Text == " ")
                {
                    try
                    {
                        Guid_person_mobile_id = Guid.NewGuid();
                        var mobileChange = PersonContactFactory.Create(Guid_person_mobile_id, Guid_pat, Guid_mobile, MobileBox.Text);
                        PersonContactRepo.CreateOrUpdateEntity(mobileChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }


                else
                {
                    try
                    {
                     Guid_person_mobile_id = PersonContactRepo.GetEntitiesByQuery(mi => mi.ContactTypeId == Guid_mobile && mi.PersonId == Guid_pat).First().Id;
                     var mobileChange = PersonContactFactory.Create(Guid_person_mobile_id, Guid_pat, Guid_mobile, MobileBox0.Text);
                     PersonContactRepo.CreateOrUpdateEntity(mobileChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }                    
            }
            

            //условие: пустой ли домашний телефон. назначение нового ID
            if (PhoneBox0.Text != PhoneBox.Text)
            {
                if (PhoneBox.Text == "" || PhoneBox.Text == " ")
                {
                    try
                    {
                    Guid_person_phone_id = Guid.NewGuid();
                    var PhoneChange = PersonContactFactory.Create(Guid_person_phone_id, Guid_pat, Guid_phone, PhoneBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(PhoneChange);

                     }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
                else
                {

                    try
                    {
                    Guid_person_phone_id = PersonContactRepo.GetEntitiesByQuery(mi => mi.ContactTypeId == Guid_phone && mi.PersonId == Guid_pat).First().Id;
                    var PhoneChange = PersonContactFactory.Create(Guid_person_phone_id, Guid_pat, Guid_phone, PhoneBox.Text);
                    PersonContactRepo.CreateOrUpdateEntity(PhoneChange);

                     }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
            }



            //условие: пустой ли адрес. назначение нового ID
            if (AddressBox0.Text != AddressBox.Text)
            {
                if (AddressBox.Text == "" || AddressBox.Text == " ")
                {

                    try
                    {
                    Guid_person_address_id = Guid.NewGuid();
                    var AddressChange = PersonContactFactory.Create(Guid_person_address_id, Guid_pat, Guid_address, AddressBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(AddressChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
                else
                {
                    try
                    {
                    Guid_person_address_id = PersonContactRepo.GetEntitiesByQuery(mi => mi.ContactTypeId == Guid_address && mi.PersonId == Guid_pat).First().Id;
                    var AddressChange = PersonContactFactory.Create(Guid_person_address_id, Guid_pat, Guid_address, AddressBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(AddressChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
            }

            //условие: пустой ли номер пациента. назначение нового ID
            if (PatientCardNumBox0.Text != PatientCardNumBox.Text)
            {
                if (PatientCardNumBox.Text == "" || PatientCardNumBox.Text == " ")
                {
                    try
                    {
                        Guid_person_pcard_id = Guid.NewGuid();
                        var pCardChange = PersonContactFactory.Create(Guid_person_pcard_id, Guid_pat, Guid_pcard, PatientCardNumBox0.Text);
                        PersonContactRepo.CreateOrUpdateEntity(pCardChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
                else
                {
                    try
                    {
                    Guid_person_pcard_id = PersonContactRepo.GetEntitiesByQuery(mi => mi.ContactTypeId == Guid_pcard && mi.PersonId == Guid_pat).First().Id;
                    var pCardChange = PersonContactFactory.Create(Guid_person_pcard_id, Guid_pat, Guid_pcard, PatientCardNumBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(pCardChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
            }


            //условие: пустой ли номер страхового полиса. назначение нового ID
            if (InsuranceBox0.Text != InsuranceBox.Text)
            {
                if (InsuranceBox.Text == "" || InsuranceBox.Text == " ")
                {
                    try
                    {
                    Guid_person_mpnum_id = Guid.NewGuid();
                    var mpnumChange = PersonContactFactory.Create(Guid_person_mpnum_id, Guid_pat, Guid_mpnum, InsuranceBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(mpnumChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
                else
                {
                    try
                    {
                    Guid_person_mpnum_id = PersonContactRepo.GetEntitiesByQuery(mi => mi.ContactTypeId == Guid_mpnum && mi.PersonId == Guid_pat).First().Id;
                    var mpnumChange = PersonContactFactory.Create(Guid_person_mpnum_id, Guid_pat, Guid_mpnum, InsuranceBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(mpnumChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
            }

            //условие: пустой ли e-mail. назначение нового ID
            if (EmailBox0.Text != EmailBox.Text)
            {
                if (EmailBox.Text == "" || EmailBox.Text == " ")
                {
                    try
                    {
                    Guid_person_email_id = Guid.NewGuid();
                    var emailChange = PersonContactFactory.Create(Guid_person_email_id, Guid_pat, Guid_email, EmailBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(emailChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
                else
                {
                    try
                    {
                    Guid_person_email_id = PersonContactRepo.GetEntitiesByQuery(mi => mi.ContactTypeId == Guid_email && mi.PersonId == Guid_pat).First().Id;
                    var emailChange = PersonContactFactory.Create(Guid_person_email_id, Guid_pat, Guid_email, EmailBox0.Text);
                    PersonContactRepo.CreateOrUpdateEntity(emailChange);
                    }
                    catch (Exception l)
                    {
                        Label1.Text = l.Message;
                    }
                }
            }



            /*if (FirstNameBox0.Text != "" && MiddleNameBox0.Text != "" && LastNameBox0.Text != "")
            {
                var personChange = PersonFactory.Create(Guid_pat, FirstNameBox.Text, MiddleNameBox.Text, LastNameBox.Text);
                personRepo.CreateOrUpdateEntity(personChange);
            }
            else
            { 
                Label1.Text = "FATAL ERROR!";             
            }*/


            

           
            PhoneBox0.Visible = false;
            PatientCardNumBox0.Visible = false;
            InsuranceBox0.Visible = false;           
            MobileBox0.Visible = false;
            EmailBox0.Visible = false;
            AddressBox0.Visible = false;

        }

       

    }
}