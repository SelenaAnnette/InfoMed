namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class MedicamentFactory
    {
        public Medicament Create(Guid id, string name, string code)
        {
            return new Medicament { Id = id, Name = name, Code = code, Description = string.Empty };
        }
    }
}
