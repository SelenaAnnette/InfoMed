namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class AssignedMedicamentFactory
    {
        public AssignedMedicament Create(Guid personId, Guid medicamentId, double dosage, string measure, double frequency)
        {
            return new AssignedMedicament { MedicamentId = medicamentId, PersonId = personId, IsActual = true,  Dosage = dosage, Measure = measure, Frequency = frequency };
        }
    }
}
