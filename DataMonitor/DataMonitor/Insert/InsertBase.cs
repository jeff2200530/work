using DataMonitor.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Insert
{
    public abstract class InsertBase : MainProcessorBase
    {
        public int _count = 0;
        public int _success=0;
        public int _fail=0;
        public FileWriter _writer = FileWriter.GetInstance();
        public Reference _reference = Reference.GetInstance();
        
        public abstract void SetData(DataTable dt);
        public DataTable TxtToDt(string path)
        {
            string folderName=Path.GetFileNameWithoutExtension(Path.GetDirectoryName(path));
            string filaName = Path.GetFileName(path);
            StringBuilder _logMessage = new StringBuilder();
            _logMessage.AppendLine($"{DateTime.Now} 開始讀取日期：{folderName}，分公司{filaName}資料");
            try
            {
                StreamReader sr = File.OpenText(path);
                DataTable dt = new DataTable();
                string firstLine = sr.ReadLine();
                int count = 0;
                int fail = 0;

                //欄名
                string[] colName = firstLine.Split(',');
                foreach (var i in colName)
                {
                    DataColumn col = dt.Columns.Add(i.ToString());
                }
                string nextLine;


                //內容
                while ((nextLine = sr.ReadLine()) != null)
                {

                    string[] every_row = nextLine.Split(',');
                    DataRow dr = dt.NewRow();
                    try
                    {
                        if (every_row.Length != dt.Columns.Count)
                            throw new Exception();
                       


                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = every_row[i];
                        }
                        dt.Rows.Add(dr);
                        count++;

                    }
                    catch
                    {
                        dt.Rows.Add(dr);
                        fail++;
                        _logMessage.AppendLine($"{DateTime.Now} 日期：{folderName}，分公司{filaName}，第{count + 1}筆欄位不符");
                        continue;
                    }
                }

                sr.Close();
                _logMessage.AppendLine($"{DateTime.Now} 日期：{folderName}，分公司{filaName}，轉換成功{count}筆,失敗{fail}筆");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });
                return dt;
            }
            catch
            {
                _logMessage.AppendLine($"{DateTime.Now} 日期：{folderName}，分公司{filaName}，讀取失敗");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });
                return null;
            }
        }


    }
}
