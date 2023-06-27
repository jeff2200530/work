using DataOutput.Format;
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
        public Queue<LogFormat> _logQueue = new Queue<LogFormat>(100);
        public string _filePath = null;
        private static readonly object syncRoot = new object();
        private static FileWriter instance;
        private FileWriter()
        {
            SetBaseData();
            Task thread = new Task(() =>
            {
                while (true)
                {
                    if (_logQueue.Count > 0)
                    {
                        var log = _logQueue.Dequeue();
                        Thread.Sleep(100);
                        if (log != null)
                            WriteTxt(log.content, log.path);
                    }
                }
            });
            thread.Start();
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
            _filePath = ConfigurationManager.AppSettings["filePath"];
            if (!Directory.Exists($"{_filePath}\\Log"))
            {
                Directory.CreateDirectory($"{_filePath}\\Log");

            }
        }
        public void WriteTxt(string content, string fileName)
        {
            using (StreamWriter sw = File.AppendText(_filePath + fileName))
            {
                sw.Write(content);
                sw.Close();
            }
        }
    }
}
