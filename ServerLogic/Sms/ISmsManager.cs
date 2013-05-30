namespace ServerLogic.Sms
{
    using SmsModule;

    public interface ISmsManager
    {
        void SaveNewSmses(Sms[] smses);

        void CheckSmsForNotifications();
    }
}