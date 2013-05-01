namespace DataLayer.Persistence.Disease
{
    using System;

    using Domain.Disease;

    public class DiseaseFactory
    {
        public Disease Create(Guid id, string name)
        {
            return new Disease { Id = id, Name = name, Description = string.Empty };
        }

        public Disease Create(Guid id, string name, string description)
        {
            return new Disease { Id = id, Name = name, Description = description };
        }
    }
}
