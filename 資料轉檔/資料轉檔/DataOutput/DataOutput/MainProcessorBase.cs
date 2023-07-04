using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOutput
{
    public class MainProcessorBase
    {
        public string []_source = null;
        public string _filePath = null;
        public string _connectString = null;
        public SqlConnection _db = null;
        public Logger logger = LogManager.GetCurrentClassLogger();
        

        public MainProcessorBase()
        {
            SetBaseData();
        }
        public void SetBaseData()
        {
            
            _source = ConfigurationManager.AppSettings["source"].ToLower().Split(",");
            _filePath = ConfigurationManager.AppSettings["filePath"];
            _connectString = ConfigurationManager.AppSettings["connectString"];
            _db = new SqlConnection(_connectString);

            
          

            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget trace = new FileTarget
            {
                Name="trace",
                FileName =$"{_filePath}\\log\\logTrace.txt",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            FileTarget error = new FileTarget
            {
                Name = "error",
                FileName = $"{_filePath}\\log\\logError.txt",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            FileTarget info = new FileTarget
            {
                Name ="info",
                FileName = $"{_filePath}\\log\\logInfo.txt",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };

            config.AddRule(LogLevel.Trace, LogLevel.Trace,trace);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, error);
            config.AddRule(LogLevel.Info, LogLevel.Info, info);
            LogManager.Configuration = config;
       
        }
    }
}
