namespace DataLayer.Persistence.Group
{
    using System;

    using Domain.Group;

    public class PersonGroupFactory
    {
        public PersonGroup Create(Guid personId, Guid groupId, DateTime entryDate)
        {
            return new PersonGroup { PersonId = personId, GroupId = groupId, EntryDate = entryDate };
        }
    }
}
