using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFile
{
    public class MainProcessorBase
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        public static string _filePath;
        public string _function;
        public string _functionName;
        public int _backupDays;
        public string[] _fileNameExtension;
        public MainProcessorBase()
        {
         
            SetBaseData();
        }


        public void SetBaseData()
        {
           
            _filePath = ConfigurationManager.AppSettings["filePath"];
            _function = ConfigurationManager.AppSettings["function"].ToUpper();
            switch (_function)
            {
                case ("A"):
                    _functionName = "Compress & Delete";
                    break;
                case ("B"):
                    _functionName = "Delete";
                    break;
                case ("C"):
                    _functionName = "Compress";
                    break;
            }
            _backupDays = Convert.ToInt32(ConfigurationManager.AppSettings["backupDays"]);
            _fileNameExtension = ConfigurationManager.AppSettings["fileName extension"].Split(",");
            //LoggingConfiguration config = new LoggingConfiguration();
            //FileTarget fileTarget = new FileTarget
            //{
            //    FileName = $"{_filePath}\\log.txt",
            //    Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            //};
            //config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            //LogManager.Configuration = config;



            logger.Trace($"FilePathL:{_filePath} | FunctionName:{_functionName} | BackDays:{_backupDays} | FileExtension:{ ConfigurationManager.AppSettings["fileName extension"]}");
        }
       
    }
}

