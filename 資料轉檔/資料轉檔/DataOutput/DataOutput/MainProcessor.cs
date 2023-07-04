using DataOutput.Extension_Methods;
using DataOutput.Format;
using DataOutput.Insert;
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
using static DataOutput.Extension_Methods.transNlogTarget;

namespace DataOutput
{
    public class MainProcessor : MainProcessorBase
    {
        //public FileWriter _writer = FileWriter.GetInstance();
        public InsertBase _process = null;
        public Request _request = null;
        public int hmtht = 0;
        public int hodrt = 0; 
        public int trafuhord =0;
        public int trafuhtrd = 0;
        public MainProcessor() : base()
        {
        }
        public void Execute(Request request)
        {
            #region Log
            //logger.Trace("Execute開始執行");
            logger.Write(Level.trace, $"Execute開始執行", "", "");
            #endregion

            _request = request;
            _process = InsertFactory.GetProcess(request);

            if (_process != null)
            {
                #region Log
                //logger.Trace($"Execute結束 執行{_request.table}轉換");
                logger.Write(Level.trace, $"Execute結束 執行{_request.table}轉換", "", "");
                #endregion
                ExecuteInsert();
            }
            else
            {
                #region Log
                //logger.Error($"Execute失敗，沒有此資料表");
                logger.Write(Level.error, $"Execute失敗，沒有此資料表", "", "");
                #endregion
            }
        }
        public void ExecuteInsert()
        {
            #region Log
           // logger.Trace($"ExecuteInsert開始");
            logger.Write(Level.trace, $"ExecuteInsert開始", "", "");
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
            //logger.Info($"檔案：{_request.table}，成功{_process._success}，失敗{_process._fail}，共{_process._count}筆，耗用時間{stopWatch.ElapsedMilliseconds}");
            logger.Write(Level.info, $"檔案：{_request.table}，成功{_process._success}，失敗{_process._fail}，共{_process._count}筆，耗用時間{stopWatch.ElapsedMilliseconds}", "", "");
            switch (_request.table)
            {
                case ("hmtht"):
                    hmtht += _process._success;
                    //logger.Info($"檔案：{_request.table} 共{hmtht}筆");
                    logger.Write(Level.info, $"檔案：{_request.table} 共{hmtht}筆", "", "");
                    Console.WriteLine($"資料夾：{_request.table}，檔案：{ data.Rows[0]["TDATE"]} 資料筆數{_process._count} 執行完成!");

                    break;
                case ("hodrt"):
                    hodrt += _process._success;
                    //logger.Info($"檔案：{_request.table} 共{hodrt}筆");
                    logger.Write(Level.info, $"檔案：{_request.table} 共{hodrt}筆", "", "");
                    Console.WriteLine($"資料夾：{_request.table}，檔案：{ data.Rows[0]["TDATE"]} 資料筆數{_process._count} 執行完成!");
                    break;
                case ("trafuhord"):
                    trafuhord += _process._success;
                    //logger.Info($"檔案：{_request.table} 共{trafuhord}筆");
                    logger.Write(Level.info, $"檔案：{_request.table} 共{trafuhord}筆", "", "");
                    Console.WriteLine($"資料夾：{_request.table}，檔案：{ data.Rows[0]["orddt"].ToString().Replace("/", "")} 資料筆數{_process._count} 執行完成!");
                    break;
                case ("trafuhtrd"):
                    trafuhtrd += _process._success;
                    //logger.Info($"檔案：{_request.table} 共{trafuhtrd}筆");
                    logger.Write(Level.info, $"檔案：{_request.table} 共{trafuhtrd}筆", "", "");
                    Console.WriteLine($"資料夾：{_request.table}，檔案：{ data.Rows[0]["trddt"].ToString().Replace("/", "")} 資料筆數{_process._count} 執行完成!");
                    break;
            }

            //logger.Trace($"ExecuteInsert結束");
            
            logger.Write(Level.trace, $"ExecuteInsert結束", "", "");
            #endregion
        }
    }
}

