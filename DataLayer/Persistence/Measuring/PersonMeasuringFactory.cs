namespace DataLayer.Persistence.Measuring
{
    using System;

    using Domain.Measuring;

    public class PersonMeasuringFactory
    {
        public PersonMeasuring Create(Guid id, Guid measuringTypeId, Guid personId, DateTime measuringDate, double value)
        {
            this.Validate(value);

            return new PersonMeasuring { Id = id, MeasuringTypeId = measuringTypeId, PersonId = personId, MeasuringDate = measuringDate, Value = value };
        }

        private void Validate(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value should be a positive number");
            }
        }
    }
}
