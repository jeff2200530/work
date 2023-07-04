using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOutput.Extension_Methods
{
    public static class transNlogTarget
    {

        public static void Write(this Logger logger, Level level, string content, string date,string tableName)
        {
            
            string _filePath = ConfigurationManager.AppSettings["filePath"];
            var target = (FileTarget)LogManager.Configuration.FindTargetByName(level.ToString());
            if (date == "")
            {
                target.FileName = $"{_filePath}\\log\\{level.ToString()}-log.txt";
            }
            else
            {
                target.FileName = $"{_filePath}\\log\\{tableName}\\{date}\\{level.ToString()}-log.txt";
            }
            switch (level.ToString())
            {
                case ("trace"):
                    logger.Trace(content);
                    break;
                case ("info"):
                    logger.Info(content);
                    break;
                case ("warn"):
                    logger.Warn(content);
                    break;
                case ("error"):
                    logger.Error(content);
                    break;
            }
        }
        public  enum Level{
            trace=0,
            info,
            warn,
            error
        }
    }
}
