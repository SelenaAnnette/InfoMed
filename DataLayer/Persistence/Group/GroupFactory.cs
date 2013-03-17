namespace DataLayer.Persistence.Group
{
    using System;

    using Domain.Group;

    public class GroupFactory
    {
        public Group Create(Guid id, string name)
        {
            return new Group { Id = id, Name = name };
        }
    }
}
