namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class RiskFactorFactory
    {
        public RiskFactor Create(Guid id, string name)
        {
            return new RiskFactor { Id = id, Name = name, Description = string.Empty };
        }
    }
}
