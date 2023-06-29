using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systex.Cube.Log.Spec;

namespace BackupTool
{
    public  class FileWriterProcessor
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();
        public void Write(LogFormatBase log) {
            try
            {
                MappedDiagnosticsLogicalContext.SetScoped("processName", log.processName);
                _logger.Properties["key"] = log.key;
                _logger.Properties["processName"] = log.processName;
                _logger.Properties["functionName"] = log.functionName;
                _logger.Properties["level"] = log.level;
                _logger.Properties["startTime"] = log.startTime;
                _logger.Properties["endTime"] = log.endTime;
                _logger.Properties["message"] = log.message;

                switch (log.level.ToUpper())
                {
                    case "TRACE":
                        _logger.Trace(log.message);
                        break;
                    case "DEBUG":
                        _logger.Debug(log.message);
                        break;
                    case "INFO":
                        _logger.Info(log.message);
                        break;
                    case "WARN":
                        _logger.Warn(log.message);
                        break;
                    case "ERROR":
                        _logger.Error(log.message);
                        break;
                    case "FATAL":
                        _logger.Fatal(log.message);
                        break;
                }        
            }
            catch (Exception ex)
            { 
            }
        }
    }
}
