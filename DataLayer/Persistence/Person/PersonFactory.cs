namespace DataLayer.Persistence.Person
{
    using System;

    using Domain.Person;

    public class PersonFactory
    {
        public Person Create(Guid id, string firstName, string middleName, string lastName)
        {
            return new Person { Id = id, FirstName = firstName, MiddleName = middleName, LastName = lastName };
        }
    }
}
