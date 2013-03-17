namespace DataLayer.Persistence.Person
{
    using System;

    using Domain.Person;

    public class CredentialsFactory
    {
        public Credentials Create(Person person, string login, string password)
        {
            return new Credentials { PersonId = person.Id, Login = login, Password = password };
        }
    }
}
