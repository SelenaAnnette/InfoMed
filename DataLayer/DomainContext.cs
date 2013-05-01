namespace DataLayer
{
    using System.Data.Entity;

    using Domain.AllergicReaction;
    using Domain.Consultation;
    using Domain.Disease;
    using Domain.Group;
    using Domain.Hospital;
    using Domain.Measuring;
    using Domain.Medicament;
    using Domain.Operation;
    using Domain.Person;
    using Domain.RiskFactor;
    using Domain.Symptom;

    class DomainContext : DbContext
    {
        public DomainContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<MeasuringType> MeasuringTypes { get; set; }

        public DbSet<AssignedMedicament> AssignedMedicaments { get; set; }
        
        public DbSet<Medicament> Medicaments { get; set; }
        
        public DbSet<ContactType> ContactTypes { get; set; }
        
        public DbSet<Credentials> Credentials { get; set; }
        
        public DbSet<Group> Groups { get; set; }
        
        public DbSet<Person> Persons { get; set; }
        
        public DbSet<PersonContact> PersonContacts { get; set; }
        
        public DbSet<PersonGroup> PersonGroups { get; set; }
        
        public DbSet<RiskFactor> RiskFactors { get; set; }

        public DbSet<AssignedRiskFactor> AssignedRiskFactors { get; set; }
        
        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<AssignedSymptom> AssignedSymptoms { get; set; }

        public DbSet<PersonPerson> PersonPersons { get; set; }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<AllergicReaction> AllergicReactions { get; set; }

        public DbSet<PersonDisease> PersonDiseases { get; set; }

        public DbSet<PersonOperation> PersonOperations { get; set; }

        public DbSet<PersonAllergicReaction> PersonAllergicReactions { get; set; }

        public DbSet<ConsultationType> ConsultationTypes { get; set; }

        public DbSet<PersonConsultation> PersonConsultations { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<HospitalDepartment> HospitalDepartments { get; set; }

        public DbSet<PersonHospitalization> PersonHospitalizations { get; set; }
    }
}
