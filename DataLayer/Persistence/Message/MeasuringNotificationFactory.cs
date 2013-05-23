namespace DataLayer.Persistence.Message
{
    using System;

    using Domain.Message;

    public class MeasuringNotificationFactory
    {
        public MeasuringNotification Create(Guid id, Guid assignedMeasuringId, Guid personId, Guid measuringTypeId, DateTime sendingDate, string text)
        {
            return new MeasuringNotification { Id = id, AssignedMeasuringId = assignedMeasuringId, PersonId = personId, MeasuringTypeId = measuringTypeId, SendingDate = sendingDate, Text = text, IsActive = true };
        }
    }
}
