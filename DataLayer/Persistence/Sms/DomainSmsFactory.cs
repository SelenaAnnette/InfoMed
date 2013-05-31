namespace DataLayer.Persistence.Sms
{
    using System;

    using Domain.Sms;

    public class DomainSmsFactory
    {
        public DomainSms Create(Guid id, string senderNumber, DateTime sendingDate, string text)
        {
            return new DomainSms { Id = id, SenderNumber = senderNumber, SendingDate = sendingDate, Text = text, IsRead = false };
        }
    }
}
