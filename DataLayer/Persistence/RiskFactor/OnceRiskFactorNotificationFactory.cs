namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class OnceRiskFactorNotificationFactory
    {
        public OnceRiskFactorNotification Create(Guid id, Guid personId, DateTime sendingDate, string text)
        {
            return new OnceRiskFactorNotification { Id = id, PersonId = personId, IsActive = true, SendingDate = sendingDate, Text = text};
        }
    }
}
