namespace DataLayer.Persistence.Measuring
{
    using System;

    using Domain.Measuring;

    public class MeasuringTypeFactory
    {
        public MeasuringType Create(Guid id, string title)
        {
            return new MeasuringType { Id = id, Title = title };
        }
    }
}
