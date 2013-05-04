namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class MedicamentApplicationWayFactory
    {
        public MedicamentApplicationWay Create(Guid id, string name)
        {
            return new MedicamentApplicationWay { Id = id, Name = name, Description = string.Empty };
        }

        public MedicamentApplicationWay Create(Guid id, string name, string description)
        {
            return new MedicamentApplicationWay { Id = id, Name = name, Description = description };
        }
    }
}
