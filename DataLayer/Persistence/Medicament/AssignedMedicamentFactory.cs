namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class AssignedMedicamentFactory
    {
        public AssignedMedicament Create(Guid id, Guid personConsultationId, Guid medicamentId, Guid medicamentApplicationWayId, double dosage, DateTime startDate, int dayCount, int timesAtDay, int eachDays)
        {
            this.Validate(dayCount, timesAtDay, eachDays, dosage);
            var tempStartDate = startDate.Date;
            var finishDate = tempStartDate.AddDays(dayCount);
            var frequency = Math.Round((double)eachDays / timesAtDay, 3);

            return new AssignedMedicament
                       {
                           Id = id,
                           MedicamentId = medicamentId,
                           PersonConsultationId = personConsultationId,
                           Dosage = dosage,
                           StartDate = tempStartDate,
                           FinishDate = finishDate,
                           Frequency = frequency,
                           MedicamentApplicationWayId = medicamentApplicationWayId
                       };
        }

        private void Validate(int dayCount, int timesAtDay, int eachDays, double dosage)
        {
            if (dosage <= 0)
            {
                throw new ArgumentException("dosage should be positive value");
            }

            if ((dosage % 0.5) != 0)
            {
                throw new ArgumentException("dosage should be multiple of 0.5");
            }

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
