using System;
using System.IO;
using System.Text;

namespace portfolio.Services
{
    /// <summary>
    /// Log seviyesi enumeration
    /// </summary>
    public enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Critical = 4
    }

    /// <summary>
    /// Uygulamanın tüm işlemlerini günlüğe kaydeder.
    /// </summary>
    public static class Logger
    {
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Bir mesajı günlüğe yaz
        /// </summary>
        /// <param name="message">Günlüğe yazılacak mesaj</param>
        /// <param name="level">Log seviyesi</param>
        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (!ConfigManager.LoggingEnabled)
                return;

            lock (_lockObject)
            {
                try
                {
                    if (!Directory.Exists(ConfigManager.LogDirectory))
                    {
                        Directory.CreateDirectory(ConfigManager.LogDirectory);
                    }

                    string logFileName = $"portfolio_{DateTime.Now:yyyy-MM-dd}.log";
                    string logFilePath = Path.Combine(ConfigManager.LogDirectory, logFileName);

                    string logMessage = FormatLogMessage(message, level);

                    File.AppendAllText(logFilePath, logMessage + Environment.NewLine, Encoding.UTF8);

                    CheckLogFileSize(logFilePath);
                }
                catch
                {
                    // Logging hatası sessiz geçilir
                }
            }
        }

        /// <summary>
        /// Exception'ı günlüğe yaz
        /// </summary>
        /// <param name="ex">Exception nesnesi</param>
        public static void LogException(Exception ex)
        {
            if (ex == null) return;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Exception: {ex.GetType().Name}");
            sb.AppendLine($"Message: {ex.Message}");
            sb.AppendLine($"StackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                sb.AppendLine($"InnerException: {ex.InnerException.Message}");
            }

            Log(sb.ToString(), LogLevel.Error);
        }

        /// <summary>
        /// Log mesajını formatla
        /// </summary>
        private static string FormatLogMessage(string message, LogLevel level)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level,-8}] {message}";
        }

        /// <summary>
        /// Log dosyası boyutunu kontrol et ve gerekirse arşivle
        /// </summary>
        private static void CheckLogFileSize(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            long fileSizeBytes = new FileInfo(filePath).Length;
            long maxSizeBytes = ConfigManager.MaxLogFileSizeMB * 1024 * 1024;

            if (fileSizeBytes > maxSizeBytes)
            {
                string archivePath = Path.Combine(
                    ConfigManager.LogDirectory,
                    $"portfolio_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log.bak"
                );

                File.Move(filePath, archivePath);
            }
        }
    }
}
