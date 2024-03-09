using VRCore.Interface;

namespace VRDataReader.Framework
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
