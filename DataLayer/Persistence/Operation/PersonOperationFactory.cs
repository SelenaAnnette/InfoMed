namespace DataLayer.Persistence.Operation
{
    using System;

    using Domain.Operation;

    public class PersonOperationFactory
    {
        public PersonOperation Create(Guid personId, Guid operatonId, DateTime operationDate)
        {
            return new PersonOperation { PersonId = personId, OperationId = operatonId, OperationDate = operationDate };
        }
    }
}
