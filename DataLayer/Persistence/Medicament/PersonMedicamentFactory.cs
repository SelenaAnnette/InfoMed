namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class PersonMedicamentFactory
    {
        public PersonMedicament Create(Guid id, Guid medicamentId, Guid personId, DateTime recordingDate)
        {
            return new PersonMedicament { Id = id, MedicamentId = medicamentId, PersonId = personId, RecordingDate = recordingDate };
        }
    }
}
