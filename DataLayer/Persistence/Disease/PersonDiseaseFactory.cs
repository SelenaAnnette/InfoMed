namespace DataLayer.Persistence.Disease
{
    using System;

    using Domain.Disease;

    public class PersonDiseaseFactory
    {
        public PersonDisease Create(Guid personId, Guid diseaseId, DateTime diseaseDate)
        {
            return new PersonDisease { PersonId = personId, DiseaseId = diseaseId, DiseaseDate = diseaseDate };
        }
    }
}
