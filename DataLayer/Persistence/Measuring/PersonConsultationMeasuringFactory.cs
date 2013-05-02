namespace DataLayer.Persistence.Measuring
{
    using System;

    using Domain.Measuring;

    public class PersonConsultationMeasuringFactory
    {
        public PersonConsultationMeasuring Create(Guid id, Guid consultationId, Guid measuringTypeId, double value)
        {
            return new PersonConsultationMeasuring { Id = id, ConsultationId = consultationId, MeasuringTypeId = measuringTypeId, Value = value};
        }
    }
}
