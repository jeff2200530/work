using Dapper;
using DataInput.Format;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInput.Query
{
    public class TraFuhord:QueryBase
    {
        public Queue<List<TraFuhordFormat>> _queue = new Queue<List<TraFuhordFormat>>();
        public List<TraFuhordFormat> _traFuhordList = new List<TraFuhordFormat>();
        public TraFuhordFormat data = new TraFuhordFormat();

        public TraFuhord(FormFormat input)
        {
            try
            {
                _input = input;
                if (input.function != "getProperties")
                    SetBaseData(_input);
            }
            catch
            {
                _logMessage.AppendLine($"{input.processName}初始化-失敗");
            }
        }

        public void CheckParameters()
        {
        }


        public override void SetBaseData(FormFormat input)
        {
           

            _logMessage = new StringBuilder();
            try
            {
                _logMessage.AppendLine($"{DateTime.Now}  設定參數-開始");

                _company = "company";
                _date = "orddt";
                _table = "trafuhord";

                _queryBhnoCommand = $"select distinct {_company} from {_table} where {_date}='{input.startDate}'";

                _db.Open();
                _bhnoArray = _db.Query<string>(_queryBhnoCommand).ToArray();
                _db.Close();


                if (string.IsNullOrEmpty(input.column))
                {
                    _queryCommand = $"select * from {_table} where {_date}='{input.startDate}'";
                }
                else
                {
                    _queryCommand = $"select {input.column} from {_table} where {_date}='{input.startDate}'";
                }


                _logMessage.AppendLine($"{DateTime.Now}  設定參數-結束\nSQL語法{_queryCommand}");


            }
            catch
            {
                _logMessage.AppendLine($"{DateTime.Now}  設定參數-失敗\nSQL語法{_queryCommand}\n查詢分公司語法{_queryBhnoCommand}");
                _db.Close();
            }
            _Writer.WriteTxt(_logMessage.ToString(), $"\\{_input.processName}\\log\\{_input.startDate.Replace("/", "")} Readlog.txt");
        }

        public override void Query()
        {
            StringBuilder _logMessage = new StringBuilder();
            int count = 0;
            _db.Open();
            try
            {
                if (_bhnoArray.Length != 0)
                {
                    for (int i = 0; i < _bhnoArray.Length; i++)
                    {
                        _traFuhordList = _db.Query<TraFuhordFormat>(_queryCommand + $" and {_company}='{_bhnoArray[i]}' order by ordtm").ToList();
                        if (_traFuhordList.Count != 0)
                        {
                            _queue.Enqueue(_traFuhordList);
                            count += _traFuhordList.Count;
                            _logMessage.AppendLine($"{DateTime.Now}  {_bhnoArray[i]}資料 讀取筆數{_traFuhordList.Count} 累積總筆數{count}");
                        }
                    }
                }
                else
                {
                    _logMessage.AppendLine("查無資料");
                }

            }

            catch
            {
                _logMessage.AppendLine("查詢失敗");
            }
            _db.Close();
            _Writer.WriteTxt(_logMessage.ToString(), $"\\{_input.processName}\\log\\{_input.startDate.Replace("/", "")} Readlog.txt");
        }


        public override void toString()
        {
            if (!Directory.Exists($"{ _Writer._filePath }\\{_input.processName}\\file\\{ _input.startDate.Replace("/", "")}"))
            {
                Directory.CreateDirectory($"{ _Writer._filePath }\\{_input.processName}\\{ _input.startDate.Replace("/", "")}");

                StringBuilder _logMessage = new StringBuilder();

                int count = 0;
                int i = 0;
                while (i < _bhnoArray.Length)
                {
                    //queue有資料
                    if (_queue.Count != 0)
                    {
                        //取出一家bhno資料
                        var list = _queue.Dequeue();


                        //寫檔
                        using (StreamWriter sw = File.CreateText($"{_Writer._filePath}\\{_input.processName}\\{_input.startDate.Replace("/", "")}\\{list[0].company.Trim()}.txt"))
                        {
                            _content = new StringBuilder();

                            //寫欄位
                            _content.AppendLine(data.GetPropertiesString());
                            //寫每一筆資料
                            foreach (var item in list)
                            {
                                _content.AppendLine(item.ToString());
                                count++;
                            }
                            sw.Write(_content);
                            _logMessage.AppendLine($"{DateTime.Now} {list[0].company}資料 寫入筆數{list.Count} 累積總筆數{count}");
                        }
                        i++;
                    }


                }
                Directory.Move($"{ _Writer._filePath }\\{_input.processName}\\{ _input.startDate.Replace("/", "")}", $"{ _Writer._filePath }\\{_input.processName}\\file\\{ _input.startDate.Replace("/", "")}");
                _Writer.WriteTxt(_logMessage.ToString(), $"\\{_input.processName}\\log\\{_input.startDate.Replace("/", "")} Writelog.txt");

            }

        }
        public override string GetProperties()
        {
            return data.GetPropertiesString();
        }
    }
}
