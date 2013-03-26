namespace DataLayer.Persistence.Measuring
{
    using System;

    using Domain.Measuring;

    public class MeasuringTypeFactory
    {
        public MeasuringType Create(Guid id, string title, string measuring)
        {
            return new MeasuringType { Id = id, Title = title, Measuring = measuring, Description = string.Empty };
        }

        public MeasuringType Create(Guid id, string title, string measuring, string description)
        {
            return new MeasuringType { Id = id, Title = title, Measuring = measuring, Description = description };
        }
    }
}
