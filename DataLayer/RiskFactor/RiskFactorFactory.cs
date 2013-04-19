namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class RiskFactorFactory
    {
        public RiskFactor Create(Guid id, string title, string measuring)
        {
            return new RiskFactor { Id = id, Title = title, Measuring = measuring, Description = string.Empty };
        }

        public RiskFactor Create(Guid id, string title, string measuring, string description)
        {
            return new RiskFactor { Id = id, Title = title, Measuring = measuring, Description = description };
        }
    }
}
