namespace SmsModule
{
    public interface IModem
    {
        bool Initialize();

        bool CheckConnection();

        bool SendSms(string phone_number, string message);
    }
}
