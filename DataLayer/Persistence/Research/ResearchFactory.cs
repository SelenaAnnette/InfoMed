namespace DataLayer.Persistence.Research
{
    using System;

    using Domain.Research;

    public class ResearchFactory
    {
        public Research Create(Guid id, string title)
        {
            return new Research { Id = id, Title = title, Description = string.Empty };
        }

        public Research Create(Guid id, string title, string description)
        {
            return new Research { Id = id, Title = title, Description = description };
        }
    }
}
