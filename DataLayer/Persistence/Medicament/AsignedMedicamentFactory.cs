namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class AsignedMedicamentFactory
    {
        public AsignedMedicament Create(Guid personId, Guid medicamentId, double dosage, string measure, DateTime frequency)
        {
            return new AsignedMedicament { MedicamentId = medicamentId, PersonId = personId, IsActual = true,  Dosage = dosage, Measure = measure, Frequency = frequency };
        }
    }
}
