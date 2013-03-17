namespace DataLayer
{
    using System.Data.Entity;

    using Domain.Group;
    using Domain.Measuring;
    using Domain.Medicament;
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

        public DbSet<AsignedMedicament> AsignedMedicaments { get; set; }
        
        public DbSet<Medicament> Medicaments { get; set; }
        
        public DbSet<ContactType> ContactTypes { get; set; }
        
        public DbSet<Credentials> Credentials { get; set; }
        
        public DbSet<Group> Groups { get; set; }
        
        public DbSet<Person> Persons { get; set; }
        
        public DbSet<PersonContact> PersonContacts { get; set; }
        
        public DbSet<PersonGroup> PersonGroups { get; set; }
        
        public DbSet<RiskFactor> RiskFactors { get; set; }

        public DbSet<AsignedRiskFactor> AsignedRiskFactors { get; set; }
        
        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<AsignedSymptom> AsignedSymptoms { get; set; }

        public DbSet<PersonPerson> PersonPersons { get; set; }
    }
}
