namespace DataLayer.Persistence.Person
{
    using System;

    using Domain.Person;

    public class CredentialsFactory
    {
        public Credentials Create(Guid id, Guid personId, string login, string password)
        {
            return new Credentials { Id = id, PersonId = personId, Login = login, Password = password };
        }
    }
}
