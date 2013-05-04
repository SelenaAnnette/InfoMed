namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class MedicamentFormFactory
    {
        public MedicamentForm Create(Guid id, string name, string measuring)
        {
            return new MedicamentForm { Id = id, Name = name, Measuring = measuring, Description = string.Empty };
        }

        public MedicamentForm Create(Guid id, string name, string measuring, string description)
        {
            return new MedicamentForm { Id = id, Name = name, Measuring = measuring, Description = description };
        }
    }
}
