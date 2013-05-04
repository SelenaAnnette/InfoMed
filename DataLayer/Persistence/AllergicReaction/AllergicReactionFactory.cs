namespace DataLayer.Persistence.AllergicReaction
{
    using System;

    using Domain.AllergicReaction;

    public class AllergicReactionFactory
    {
        public AllergicReaction Create(Guid id, string name)
        {
            return new AllergicReaction { Id = id, Name = name, Description = string.Empty };
        }

        public AllergicReaction Create(Guid id, string name, string description)
        {
            return new AllergicReaction { Id = id, Name = name, Description = description };
        }
    }
}
