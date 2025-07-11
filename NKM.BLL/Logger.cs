using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKM.BLL
{
    public class Logger : ILogger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath = "log.txt")
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void Log(Exception ex)
        {
            Log($"[EXCEPTION] {ex.Message}\n{ex.StackTrace}");
        }
    }
}
