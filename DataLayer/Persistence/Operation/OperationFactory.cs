namespace DataLayer.Persistence.Operation
{
    using System;

    using Domain.Operation;

    public class OperationFactory
    {
        public Operation Create(Guid id, string name)
        {
            return new Operation { Id = id, Name = name, Description = string.Empty };
        }

        public Operation Create(Guid id, string name, string description)
        {
            return new Operation { Id = id, Name = name, Description = description };
        }
    }
}
