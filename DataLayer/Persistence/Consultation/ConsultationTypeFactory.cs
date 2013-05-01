namespace DataLayer.Persistence.Consultation
{
    using System;

    using Domain.Consultation;

    public class ConsultationTypeFactory
    {
        public ConsultationType Create(Guid id, string name)
        {
            return new ConsultationType { Id = id, Name = name };
        }
    }
}
