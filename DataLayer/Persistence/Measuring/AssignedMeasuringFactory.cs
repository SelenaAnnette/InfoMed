namespace DataLayer.Persistence.Measuring
{
    using System;

    using Domain.Measuring;

    public class AssignedMeasuringFactory
    {
        public AssignedMeasuring Create(Guid id, Guid personConsultationId, DateTime startDate, int dayCount, int timesAtDay, int eachDays)
        {
            this.Validate(dayCount, timesAtDay, eachDays);
            var tempStartDate = startDate.Date;
            var finishDate = tempStartDate.AddDays(dayCount);
            var frequency = Math.Round((double)eachDays / timesAtDay, 3);

            return new AssignedMeasuring
                       {
                           Id = id,
                           PersonConsultationId = personConsultationId,
                           StartDate = tempStartDate,
                           FinishDate = finishDate,
                           Frequency = frequency
                       };
        }

        private void Validate(int dayCount, int timesAtDay, int eachDays)
        {
            if (dayCount <= 0)
            {
                throw new ArgumentException("dayCount should be positive value");
            }

            if (timesAtDay <= 0)
            {
                throw new ArgumentException("timesAtDay should be positive value");
            }

            if (eachDays <= 0)
            {
                throw new ArgumentException("eachDays should be positive value");
            }

            if (eachDays > 1 && timesAtDay > 1)
            {
                throw new ArgumentException("one of eachDays and timesAtDay should be 1");
            }
        }
    }
}
