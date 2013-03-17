namespace DataLayer.Persistence.Person
{
    using System;

    using Domain.Person;

    public class PersonPersonFactory
    {
        public PersonPerson Create(Guid id, Guid firstPersonId, Guid secondPersonId)
        {
            return new PersonPerson { Id = id, FirstPersonId = firstPersonId, SecondPersonId = secondPersonId, IsExist = true };
        }
    }
}
