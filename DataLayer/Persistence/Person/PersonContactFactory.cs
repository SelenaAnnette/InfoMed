namespace DataLayer.Persistence.Person
{
    using System;

    using Domain.Person;

    public class PersonContactFactory
    {
        public PersonContact Create(Guid id, Guid personId, Guid contactTypeId, string value)
        {
            return new PersonContact { Id = id, PersonId = personId, ContactTypeId = contactTypeId, Value = value };
        }
    }
}
