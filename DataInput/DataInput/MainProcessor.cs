using DataInput.Format;
using DataInput.Insert;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInput
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
            #region Log
            logger.Trace("Execute開始執行");
            #endregion

            _request = request;
            _process = InsertFactory.GetProcess(request);

            if (_process != null)
            {
                #region Log
                logger.Trace($"Execute結束 執行{_request.table}轉換");
                #endregion
                ExecuteInsert();
            }
            else
            {
                #region Log
                logger.Error($"Execute失敗，無此資料");
                #endregion
            }
        }
        public void ExecuteInsert()
        {
            #region Log
            logger.Trace($"ExecuteInsert開始");
            #endregion

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            DataTable data = new DataTable();
            foreach (var file in _request.file)
            {
                data = _process.TxtToDt(file.FullName);
                _process.SetData(data);
            }
            stopWatch.Stop();
            #region Log
            logger.Trace($"檔案：{_request.table}，成功{_process._success}，失敗{_process._fail}，共{_process._count}筆，耗用時間{stopWatch.ElapsedMilliseconds}");
            logger.Trace($"ExecuteInsert結束");
            #endregion
        }
    }
}

