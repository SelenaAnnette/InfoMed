using System;

namespace ServerLogic.Logger
{
    using System.IO;

    public class FileLogger : ILogger
    {
        private readonly string directoryPath;

        private readonly string errorFilePath;

        private readonly string infoFilePath;

        private object lockObject;

        public FileLogger()
        {
            this.lockObject = new object();            
            this.directoryPath = string.Format("{0}\\Server log", Environment.CurrentDirectory);
            this.infoFilePath = string.Format("{0}\\InfoLog.txt", this.directoryPath);
            this.errorFilePath = string.Format("{0}\\ErrorLog.txt", this.directoryPath);
        }

        public void LogMessage(string message)
        {
            lock (this.lockObject)
            {
                if (!Directory.Exists(this.directoryPath))
                {
                    Directory.CreateDirectory(this.directoryPath);
                }

                using (var fileStream = File.Open(this.infoFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("{0}  INFO  {1}", DateTime.Now, message);
                    }
                }
            }
        }

        public void LogError(string error)
        {
            lock (this.lockObject)
            {
                if (!Directory.Exists(this.directoryPath))
                {
                    Directory.CreateDirectory(this.directoryPath);
                }

                using (var fileStream = File.Open(this.errorFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("{0}  ERROR  {1}", DateTime.Now, error);
                    }
                }
            }
        }
    }
}
