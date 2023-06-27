using NLog;
using System;

namespace LogWriterTool
{
    public class Program
    {
        static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Trace("logTrace測試");
            logger.Error("logError測試");
            logger.Debug("logDebug測試");
            logger.Info("logInfo測試");
            logger.Fatal("logFatal測試");
            logger.Warn("logWarn測試");
            Console.WriteLine("Hello World!");
            Console.WriteLine("NLog測試!!!");
            Console.ReadLine();
        }
    }
}
