namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class MedicamentFactory
    {
        public Medicament Create(Guid id, string name, string code, Guid medicamentFormId)
        {
            return new Medicament { Id = id, Name = name, Code = code, MedicamentFormId = medicamentFormId, Description = string.Empty };
        }

        public Medicament Create(Guid id, string name, string code, Guid medicamentFormId, string description)
        {
            return new Medicament { Id = id, Name = name, Code = code, MedicamentFormId = medicamentFormId, Description = description };
        }
    }
}
