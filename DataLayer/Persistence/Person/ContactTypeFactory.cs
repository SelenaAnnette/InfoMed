namespace DataLayer.Persistence.Person
{
    using System;

    using Domain.Person;

    public class ContactTypeFactory
    {
        public ContactType Create(Guid id, string title)
        {
            return new ContactType { Id = id, Title = title };
        }
    }
}
