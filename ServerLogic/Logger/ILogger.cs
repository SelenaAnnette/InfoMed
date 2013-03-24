namespace ServerLogic.Logger
{
    public interface ILogger
    {
        void LogMessage(string message);

        void LogError(string error);
    }
}
