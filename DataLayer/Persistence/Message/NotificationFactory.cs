namespace DataLayer.Persistence.Message
{
    using System;

    using Domain.Message;

    public class NotificationFactory
    {
        public Notification Create(Guid id, Guid personId, DateTime sendingDate, string text)
        {
            return new Notification { Id = id, PersonId = personId, SendingDate = sendingDate, Text = text };
        }
    }
}
