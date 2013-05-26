namespace ServerLogic.Security
{
    using System;
    using System.Linq;

    using DataLayer.Persistence.Group;
    using DataLayer.Persistence.Person;

    using Domain;
    using Domain.Group;
    using Domain.Person;

    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IGroupRepository groupRepository;

        private readonly IPersonGroupRepository personGroupRepository;

        private readonly ICredentialsRepository credentialsRepository;

        private readonly IPersonRepository personRepository;

        private readonly PersonGroupFactory personGroupFactory;

        private readonly CredentialsFactory credentialsFactory;

        public AuthenticationProvider(IGroupRepository groupRepository, IPersonGroupRepository personGroupRepository, ICredentialsRepository credentialsRepository, IPersonRepository personRepository)
        {
            this.groupRepository = groupRepository;
            this.personGroupRepository = personGroupRepository;
            this.credentialsRepository = credentialsRepository;
            this.personRepository = personRepository;
            this.personGroupFactory = new PersonGroupFactory();
            this.credentialsFactory = new CredentialsFactory();
        }

        public Person CreateUser(Guid personId, string login, string password, Groups groupName)
        {
            var group = this.groupRepository.GetEntitiesByQuery(v => v.Name == groupName.GetStringValue()).First();
            var personGroup = this.personGroupFactory.Create(Guid.NewGuid(), personId, group.Id, DateTime.Now);
            this.personGroupRepository.CreateOrUpdateEntity(personGroup);
            var credentials = this.credentialsFactory.Create(Guid.NewGuid(), personId, login, password);
            this.credentialsRepository.CreateOrUpdateEntity(credentials);

            return this.personRepository.GetEntityById(personId);
        }

        public Person Validate(string login, string password, Groups groupName)
        {
            var credentials = this.credentialsRepository.GetEntitiesByQuery(v => v.Login == login && v.Password == password)
                .FirstOrDefault();
            if (credentials == null)
            {
                return null;
            }

            var personGroup = this.personGroupRepository.GetEntitiesByQuery(v => v.PersonId == credentials.PersonId && v.Group.Name == groupName.GetStringValue()).FirstOrDefault();
            if (personGroup == null)
            {
                return null;
            }

            return this.personRepository.GetEntityById(credentials.PersonId);
        }
    }
}