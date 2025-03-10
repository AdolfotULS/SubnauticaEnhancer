using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SubnauticaEnhancer
{
    public class Logger
    {
        public enum LogLevel
        {
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL
        }
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private static LogLevel currentLogLevel = LogLevel.DEBUG;
        private static string logFilePath = "mod_log.txt";
        private static bool consoleInitialized;

        public static void Initialize(LogLevel logLevel = LogLevel.DEBUG, string logFile = null)
        {
            currentLogLevel = logLevel;
            if (logFile != null) logFilePath = logFile;

            InitializeConsole();
        }

        public static void Log(LogLevel level, string message)
        {
            if (level < currentLogLevel) return;

            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";
            PrintToConsole(level, logMessage);
            AppendLogToFile(logMessage);
        }

        public static void Debug(string message)
        {
            Log(LogLevel.DEBUG, message);
        }

        public static void Info(string message)
        {
            Log(LogLevel.INFO, message);
        }

        public static void Warn(string message)
        {
            Log(LogLevel.WARN, message);
        }

        public static void Error(string message)
        {
            Log(LogLevel.ERROR, message);
        }

        public static void Fatal(string message)
        {
            Log(LogLevel.FATAL, message);
        }

        private static void PrintToConsole(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.DEBUG:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case LogLevel.INFO:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogLevel.WARN:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.FATAL:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void AppendLogToFile(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, message + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] Failed to write to log file: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void InitializeConsole()
        {
            if (consoleInitialized) return;

            AllocConsole();
            var consoleWindow = GetConsoleWindow();
            ShowWindow(consoleWindow, SW_SHOW);
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Console.OutputEncoding = Encoding.Default;
            consoleInitialized = true;
        }

        public static void CloseConsole()
        {
            FreeConsole();
            consoleInitialized = false;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}