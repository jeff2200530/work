using DataInput.Format;
using DataInput.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataInput
{
    public class MainProcessor : MainProcessorBase
    {
        public QueryBase _process = null;
        public string _processName = null;
        public FileWriter _fileWriter = new FileWriter();
        public FormFormat _input = null;
        public string _properties = null;





        public MainProcessor() : base()
        {
            if (_execute == "auto" & _tableName != null)
            {
                


                foreach (var table in _tableName)
                {
                    Console.WriteLine($"execute {table}...");
                    if (_execute == "auto" & (table == "trafuhord" | table == "trafuhtrd"))
                    {
                        _endTime = DateTime.Now.ToString("yyyy/MM/dd");
                        _startTime = DateTime.Now.AddDays(-6).ToString("yyyy/MM/dd");
                    }
                    else if (_execute == "auto" & (table == "hmtht" | table == "hodrt"))
                    {
                        _endTime = DateTime.Now.ToString("yyyyMMdd");
                        _startTime = DateTime.Now.AddDays(-6).ToString("yyyyMMdd");
                    }
                    _input = new FormFormat { processName = table, function = "query", startDate = _startTime, endDate = _endTime };

                    new MainProcessor(_input);

                }
            }




        }
        public MainProcessor(FormFormat input)
        {

            Task thrad = new Task(() =>
            {
                DateTime nowDate = DateTime.ParseExact(input.startDate.Replace("/", ""), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                DateTime endDate = DateTime.ParseExact(input.endDate.Replace("/", ""), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                //startDate開始執行到endDate結束
                while (DateTime.Compare(nowDate, endDate) != 1)
                {
                    //挑選實作
                    _process = QueryFactory.GetProcess(input);

                    //選取執行function
                    switch (input.function)
                    {

                        case ("query"):
                            ExecuteQuery();
                            break;
                        case ("getProperties"):
                            ExecuteGetProperties();
                            break;

                        default:
                            break;
                    }

                    nowDate = nowDate.AddDays(1);
                    if (input.processName == "hmtht" | input.processName == "hodrt")
                        input.startDate = nowDate.ToString("yyyyMMdd");
                    else
                        input.startDate = nowDate.ToString("yyyy/MM/dd");
                }
                nowDate = nowDate.AddDays(-1);
                if (input.processName == "hmtht" | input.processName == "hodrt")
                    input.startDate = nowDate.ToString("yyyyMMdd");
                else
                    input.startDate = nowDate.ToString("yyyy/MM/dd");
               

            });

            thrad.Start();
            Task.WaitAll(thrad);
            _process = QueryFactory.GetProcess(input);
            switch (input.function)
            {

                
                case ("getProperties"):
                    ExecuteGetProperties();
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// 執行資料查詢讀取
        /// </summary>
        public void ExecuteQuery()
        {
            Task subThread1 = new Task(() =>
            {
                _process.Query();
            });
            Task subThread2 = new Task(() =>
            {
                _process.toString();
            });

            subThread1.Start();
            subThread2.Start();
            Task.WaitAll(subThread1, subThread2);

        }
        public void ExecuteGetProperties()
        {
            _properties = _process.GetProperties();
        }

    }
}
