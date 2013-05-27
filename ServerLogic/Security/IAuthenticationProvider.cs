namespace ServerLogic.Security
{
    using System;

    using Domain.Group;
    using Domain.Person;

    public interface IAuthenticationProvider
    {
        Person CreateUser(Guid personId, string username, string password, Groups groupName);

        Person Validate(string username, string password, Groups groupName);
    }
}