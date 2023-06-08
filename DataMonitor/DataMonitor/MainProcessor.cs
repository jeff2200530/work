using DataMonitor.Format;
using DataMonitor.Insert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor
{
    public class MainProcessor : MainProcessorBase
    {
        public FileWriter _writer = FileWriter.GetInstance();
        public InsertBase _process = null;
        public Request _request = null;
        public MainProcessor() : base()
        {
        }

        public void Execute(Request request)
        {
            StringBuilder _logMessage = new StringBuilder();
            _logMessage.AppendLine($"{DateTime.Now} Execute開始執行");



            _request = request;
            //挑選實作
            _process = InsertFactory.GetProcess(request);


            if (_process != null)
            {
                _logMessage.AppendLine($"{DateTime.Now} Execute結束 執行{_request.table}轉換");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });
                ExecuteInsert();

            }
        }
        public void ExecuteInsert()
        {
            StringBuilder _logMessage = new StringBuilder();
            _logMessage.AppendLine($"{DateTime.Now} ExecuteInsert開始");
            _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            DataTable data = new DataTable();
            
            //將檔案中每個分公司資料(txt)轉為datatable
            foreach (var file in _request.file)
            {
                
                data = _process.TxtToDt(file.FullName);
                //依資料表進行格式填入、轉換並insert進資料庫
                _process.SetData(data);


            }

            stopWatch.Stop();

            _logMessage = new StringBuilder();
            _logMessage.AppendLine($"{DateTime.Now} 檔案：{_request.table}，成功{_process._success}，失敗{_process._fail}，共{_process._count}筆，耗用時間{stopWatch.ElapsedMilliseconds}");
            _logMessage.AppendLine($"{DateTime.Now} ExecuteInsert結束");
            _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });
        }
    }
}

