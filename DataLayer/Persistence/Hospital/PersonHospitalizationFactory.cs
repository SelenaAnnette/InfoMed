namespace DataLayer.Persistence.Hospital
{
    using System;

    using Domain.Hospital;

    public class PersonHospitalizationFactory
    {
        public PersonHospitalization Create(Guid id, Guid hospitalDepartmentId, Guid personId, DateTime hospitalizationDate, string hospitalizationReason)
        {
            return new PersonHospitalization { Id = id, HospitalDepartmentId = hospitalDepartmentId, PersonId = personId, HospitalizationDate = hospitalizationDate,
                HospitalizationReason = hospitalizationReason };
        }

        public PersonHospitalization Create(Guid id, Guid hospitalDepartmentId, Guid personId, DateTime hospitalizationDate, DateTime dischargeDate, string hospitalizationReason)
        {
            return new PersonHospitalization { Id = id, HospitalDepartmentId = hospitalDepartmentId, PersonId = personId, HospitalizationDate = hospitalizationDate,
                DischargeDate = dischargeDate, HospitalizationReason = hospitalizationReason };
        }
    }
}
