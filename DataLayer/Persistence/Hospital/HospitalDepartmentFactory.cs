namespace DataLayer.Persistence.Hospital
{
    using System;

    using Domain.Hospital;

    public class HospitalDepartmentFactory
    {
        public HospitalDepartment Create(Guid id, Guid hospitalId, string name)
        {
            return new HospitalDepartment { Id = id, HospitalId = hospitalId, Name = name };
        }
    }
}
