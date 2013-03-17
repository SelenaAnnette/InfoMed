namespace DataLayer
{
    using System.Data.Entity;

    using Domain.Measuring;
    using Domain.Medicament;
    using Domain.Message;    
    using Domain.RiskFactor;
    using Domain.Symptom;

    class TrashDomainContext : DbContext
    {
        public TrashDomainContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<PersonMeasuring> PersonMeasurings { get; set; }

        public DbSet<PersonMedicament> PersonMedicaments { get; set; }

        public DbSet<PersonRiskFactor> PersonRiskFactors { get; set; }

        public DbSet<PersonSymptom> PersonSymptoms { get; set; }
    }
}
