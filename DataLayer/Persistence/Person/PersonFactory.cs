namespace DataLayer.Persistence.Person
{
    using System;

    using Domain;
    using Domain.Person;

    public class PersonFactory
    {
        public Person Create(Guid id, string firstName, string middleName, string lastName, DateTime birthday, Sex sex)
        {

            return new Person { Id = id, FirstName = firstName, MiddleName = middleName, LastName = lastName, Birthday = birthday, Sex = sex.GetStringValue() };
        }
    }
}
