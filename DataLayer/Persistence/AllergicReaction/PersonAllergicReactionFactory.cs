namespace DataLayer.Persistence.AllergicReaction
{
    using System;

    using Domain.AllergicReaction;

    public class PersonDiseaseFactory
    {
        public PersonAllergicReaction Create(Guid personId, Guid allergicReactionId, DateTime allergicReactionDate)
        {
            return new PersonAllergicReaction { PersonId = personId, AllergicReactionId = allergicReactionId, AllergicReactionDate = allergicReactionDate };
        }
    }
}
