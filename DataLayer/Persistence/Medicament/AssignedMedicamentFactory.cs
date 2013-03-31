namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class AssignedMedicamentFactory
    {
        public AssignedMedicament Create(Guid id, Guid personId, Guid medicamentId, double dosage, string measure, DateTime startDate, int dayCount, int timesAtDay, int eachDays)
        {
            this.Validate(dayCount, timesAtDay, eachDays);
            var tempStartDate = startDate.Date;
            var finishDate = tempStartDate.AddDays(dayCount);
            var frequency = Math.Round((double)(eachDays / timesAtDay), 3);            
            return new AssignedMedicament { Id = id, MedicamentId = medicamentId, PersonId = personId, Dosage = dosage, Measure = measure, StartDate = startDate, FinishDate = finishDate, Frequency = frequency };
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
