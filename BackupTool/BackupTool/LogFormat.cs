using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systex.Cube.Log.Spec;
using Systex.Cube.Models;

namespace BackupTool
{
    public class LogFormat : LogFormatBase
    {
        public static LogFormatBase Compose(LogLevel level, string key, string processName, string functionName
                                            , string startTime, string endTime, string message)
        {
            LogFormatBase logFormat = new LogFormatBase();
            logFormat.level = level.ToString();
            logFormat.key = key;
            logFormat.processName = processName;
            logFormat.functionName = functionName;
            logFormat.startTime = startTime;
            logFormat.endTime = endTime;
            logFormat.message = message;

            return logFormat;
        }

        public static string ToString(LogFormatBase logFormat)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("level=" + logFormat.level);
            stringBuilder.Append(", processName=" + logFormat.processName);
            stringBuilder.Append(", functionName=" + logFormat.functionName);
            stringBuilder.Append(", startTime=" + logFormat.startTime);
            stringBuilder.Append(", endTime=" + logFormat.endTime);
            stringBuilder.Append(", message=" + logFormat.message);

            return stringBuilder.ToString();
        }
    }
}
