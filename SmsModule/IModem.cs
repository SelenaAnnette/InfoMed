namespace SmsModule
{
    using System;

    public interface IModem
    {
        bool Initialize();

        bool CheckConnection();

        bool SendSms(string phone_number, string message);

        Sms[] GetAllSms();

        bool DeleteByDate(DateTime dt);
    }
}
