﻿using System;

namespace ServerLogic.Logger
{
    using System.IO;

    public class FileLogger : ILogger
    {
        private readonly string directoryPath;

        private readonly string errorFilePath;

        private readonly string infoFilePath;

        private static readonly object LockObject = new object();

        public FileLogger()
        {
            this.directoryPath = string.Format("{0}\\Server log", Environment.CurrentDirectory);
            this.infoFilePath = string.Format("{0}\\InfoLog.txt", this.directoryPath);
            this.errorFilePath = string.Format("{0}\\ErrorLog.txt", this.directoryPath);
        }

        public void LogMessage(string message)
        {
            lock (LockObject)
            {
                try
                {
                    if (!Directory.Exists(this.directoryPath))
                    {
                        Directory.CreateDirectory(this.directoryPath);
                    }

                    if (!File.Exists(this.infoFilePath))
                    {
                        File.Create(this.infoFilePath);
                    }

                    using (var streamWriter = new StreamWriter(this.infoFilePath, true))
                    {
                        streamWriter.WriteLine("{0}  INFO  {1}", DateTime.Now, message);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void LogError(string error)
        {
            lock (LockObject)
            {
                try
                {
                    if (!Directory.Exists(this.directoryPath))
                    {
                        Directory.CreateDirectory(this.directoryPath);
                    }

                    if (!File.Exists(this.errorFilePath))
                    {
                        File.Create(this.errorFilePath);
                    }

                    using (var streamWriter = new StreamWriter(this.errorFilePath, true))
                    {
                        streamWriter.WriteLine("{0}  ERROR  {1}", DateTime.Now, error);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
