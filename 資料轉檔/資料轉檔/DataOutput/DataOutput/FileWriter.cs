using DataOutput.Format;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataOutput
{
    public class FileWriter
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        public Queue<LogFormat> _logQueue = new Queue<LogFormat>(100);
        public string _filePath = null;
        private static readonly object syncRoot = new object();
        private static FileWriter instance;
        private FileWriter()
        {
            
            SetBaseData();
            
        }
        public static FileWriter GetInstance()
        {
            if (instance == null) //確保當前沒有這個物件存在
            {
                lock (syncRoot)//鎖住當前狀態
                {
                    if (instance == null)//再次確認該物件不存在
                    {
                        instance = new FileWriter();
                    }
                }
            }
            return instance;
        }
        public void SetBaseData()
        {
            
            //LoggingConfiguration config = new LoggingConfiguration();
            //FileTarget fileTarget = new FileTarget
            //{
            //    FileName = $"{_filePath}\\log\\log.txt",
            //    Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            //};
            //FileTarget fileTargetError = new FileTarget
            //{
            //    FileName = $"{_filePath}\\log\\logError.txt",
            //    Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            //};
            //FileTarget fileTargetInfo = new FileTarget
            //{
            //    FileName = $"{_filePath}\\log\\logInfo.txt",
            //    Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} [${uppercase:${level}}] ${message}",
            //};
            //config.AddRule(LogLevel.Trace, LogLevel.Trace, fileTarget);
            //config.AddRule(LogLevel.Error, LogLevel.Fatal, fileTargetError);
            //config.AddRule(LogLevel.Info, LogLevel.Info, fileTargetInfo);
            //LogManager.Configuration = config;
        }
        public void WriteTxt(string format , string content)
        {
            //switch (format) {
            //    case ("Trace"):
                    
            
            
            
            
            
            
            
            
            //}
        }
    }
}
