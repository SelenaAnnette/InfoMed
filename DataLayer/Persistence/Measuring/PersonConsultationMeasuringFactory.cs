namespace DataLayer.Persistence.Measuring
{
    using System;

    using Domain.Measuring;

    public class PersonConsultationMeasuringFactory
    {
        public PersonConsultationMeasuring Create(Guid id, Guid personConsultationId, Guid measuringTypeId, double value)
        {
            return new PersonConsultationMeasuring { Id = id, PersonConsultationId = personConsultationId, MeasuringTypeId = measuringTypeId, Value = value };
        }
    }
}
