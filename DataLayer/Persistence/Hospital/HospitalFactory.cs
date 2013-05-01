namespace DataLayer.Persistence.Hospital
{
    using System;

    using Domain.Hospital;

    public class HospitalFactory
    {
        public Hospital Create(Guid id, string name)
        {
            return new Hospital { Id = id, Name = name };
        }
    }
}
