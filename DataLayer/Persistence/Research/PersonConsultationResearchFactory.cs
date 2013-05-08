namespace DataLayer.Persistence.Research
{
    using System;

    using Domain.Research;

    public class PersonConsultationResearchFactory
    {
        public PersonConsultationResearch Create(Guid id, Guid personConsultationId, Guid researchId, string conclusion)
        {
            return new PersonConsultationResearch { Id = id, PersonConsultationId = personConsultationId, ResearchId = researchId, Conclusion = conclusion };
        }
    }
}
