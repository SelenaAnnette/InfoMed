namespace DataLayer.Persistence.Message
{
    using System;

    using Domain.Message;

    public class NotificationFactory
    {
        public Notification Create(Guid id, Guid personId, Guid medicamentId, DateTime sendingDate, string text)
        {
            return new Notification { Id = id, PersonId = personId, MedicamentId = medicamentId, SendingDate = sendingDate, Text = text, IsActive = true };
        }
    }
}
