namespace DataLayer.Persistence.Hospital
{
    using System;

    using Domain.Hospital;

    public class PersonHospitalizationFactory
    {
        public PersonHospitalization Create(Guid id, Guid hospitalDepartmentId, Guid personId, DateTime hospitalizationDate, string hospitalizationsReason)
        {
            return new PersonHospitalization { Id = id, HospitalDepartmentId = hospitalDepartmentId, PersonId = personId, HospitalizationDate = hospitalizationDate,
                HospitalizationsReason = hospitalizationsReason };
        }

        public PersonHospitalization Create(Guid id, Guid hospitalDepartmentId, Guid personId, DateTime hospitalizationDate, DateTime dischargeDate, string hospitalizationsReason)
        {
            return new PersonHospitalization { Id = id, HospitalDepartmentId = hospitalDepartmentId, PersonId = personId, HospitalizationDate = hospitalizationDate,
                DischargeDate = dischargeDate, HospitalizationsReason = hospitalizationsReason };
        }
    }
}
