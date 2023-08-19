/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using System;
using System.IO;
using UnityEngine;

namespace CoxlinCore
{
    /// <summary>
    /// Allows use to write to a log file that users cane easily get to
    /// </summary>
    public static class LogFileWriter
    {
        private static string _logFilePath;

        public static void Initialize()
        {
            if (Application.isEditor)
            {
                return;
            }

            // Get the application's data path in a platform-agnostic way
            string dataPath = Application.persistentDataPath;
            _logFilePath = Path.Combine(dataPath, "error_log.txt");
            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath);
            }

            // Hook into log messages
            Application.logMessageReceived += HandleLog;
        }

        private static void HandleLog(string logMessage, string stackTrace, LogType type)
        {
            if (type != LogType.Error && type != LogType.Exception) return;
            WriteToFile(logMessage, stackTrace);
        }

        private static void WriteToFile(string logMessage, string stackTrace)
        {
            try
            {
                string logContent = string.Format(
                    "Error: {0}\nStackTrace: {1}\nTimestamp: {2}\n----------------------------------\n",
                    logMessage, stackTrace, DateTime.Now);
                File.AppendAllText(_logFilePath, logContent);
            }
            catch (Exception e)
            {
                Debug.LogError("Error writing to file: " + e);
            }
        }
    }
}