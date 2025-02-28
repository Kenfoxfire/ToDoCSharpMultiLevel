
namespace ToDoApp.Utils
{
   
    public class Utils
    {
        public enum LogType
        {
            Info,
            Warning,
            Error
        }

        public static void Log(string msg, LogType type)
        {
            switch (type)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }


            string logMessage = type switch
            {
                LogType.Info => $"[INFO]: {msg}",
                LogType.Warning => $"[WARNING]: {msg}",
                LogType.Error => $"[ERROR]: {msg}",
                _ => msg
            };

            Console.WriteLine(logMessage);
            Console.ResetColor();
        }
    }
}
