namespace DataLayer.Persistence.Measuring
{
    using System;

    using Domain.Measuring;

    public class PersonMeasuringFactory
    {
        public PersonMeasuring Create(Guid id, Guid measuringTypeId, Guid personId, DateTime measuringDate, double value)
        {
            return new PersonMeasuring { Id = id, MeasuringTypeId = measuringTypeId, PersonId = personId, MeasuringDate = measuringDate, Value = value };
        }
    }
}
