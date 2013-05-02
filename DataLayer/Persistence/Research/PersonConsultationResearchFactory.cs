namespace DataLayer.Persistence.Research
{
    using System;

    using Domain.Research;

    public class PersonConsultationResearchFactory
    {
        public PersonConsultationResearch Create(Guid id, Guid consultationId, Guid researchId, string conclusion)
        {
            return new PersonConsultationResearch { Id = id, ConsultationId = consultationId, ResearchId = researchId, Conclusion = conclusion };
        }
    }
}
