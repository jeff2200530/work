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

namespace DataInput
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
            _connectString = ConfigurationManager.AppSettings["SGTP_TRADE"];
            _db = new SqlConnection(_connectString);
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget
            {
                FileName = $"{_filePath}\\log\\log.txt",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            };
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;
        }
    }
}
