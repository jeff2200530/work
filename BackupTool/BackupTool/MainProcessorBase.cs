using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFile
{
    public class MainProcessorBase
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        public static string _filePath;
        public string _function;
        public int _backupDays;
        public MainProcessorBase()
        {
            SetBaseData();
        }


        public void SetBaseData()
        {
            _filePath = ConfigurationManager.AppSettings["filePath"];
            _function = ConfigurationManager.AppSettings["function"].ToUpper();
            _backupDays = Convert.ToInt32(ConfigurationManager.AppSettings["backupDays"]);
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget
            {
                FileName = $"{_filePath}\\log.txt",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;

        }

    }
    
}
