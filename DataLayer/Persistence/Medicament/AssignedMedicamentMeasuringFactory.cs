namespace DataLayer.Persistence.Medicament
{
    using System;

    using Domain.Medicament;

    public class AssignedMedicamentMeasuringFactory
    {
        public AssignedMedicamentMeasuring Create(Guid id, Guid measuringTypeId, Guid assignedMedicamentId, int timeIntervalInSeconds)
        {
            return new AssignedMedicamentMeasuring
            {
                Id = id,
                AssignedMedicamentId = assignedMedicamentId,
                MeasuringTypeId = measuringTypeId,
                TimeIntervalInSeconds = timeIntervalInSeconds
            };
        }
    }
}
