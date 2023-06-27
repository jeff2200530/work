using DataInput.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInput
{
    public class DataProcessor : MainProcessorBase
    {

        public Queue<DataTable> _hodrtQueue = new Queue<DataTable>();
        public Queue<DataTable> _hmthtQueue = new Queue<DataTable>();
        public Queue<DataTable> _traFuhordQueue = new Queue<DataTable>();
        public Queue<DataTable> _traFuhtrdQueue = new Queue<DataTable>();

        public DataProcessor(/*FormFormat input*/) {
            //fileWatcher(input);
        }

        public void fileWatcher(FormFormat input)
        {
            Task t = new Task(() =>
            {
                FileSystemWatcher watcher = new FileSystemWatcher
                {
                    // 設定要監看的資料夾
                    Path = $"{_filePath}\\{input.processName}\\{input.startDate}",
                    // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
                    NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size,
                    // 設定要監看的檔案類型
                    Filter = "*.txt",
                    // 設定是否監看子資料夾
                    IncludeSubdirectories = true,
                    // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
                    EnableRaisingEvents = true

                };
                watcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案
            });
            t.Start();
            Console.WriteLine("開始監聽");
        }
        public void FileCreated(object sender, FileSystemEventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(e.FullPath.ToString());
            FileInfo[] files = dirInfo.Parent.GetFiles();
            foreach (var file in files)
            {
              
                        _hodrtQueue.Enqueue(TxtToDt(file.FullName));
                 
            }
        }
        public DataTable TxtToDt(string path)
        {

            try
            {
                StreamReader sr = File.OpenText(path);
                DataTable dt = new DataTable();
                string firstLine = sr.ReadLine();
                string[] colName = firstLine.Split(',');
                foreach (var i in colName)
                {
                    DataColumn col = dt.Columns.Add(i.ToString());
                }
                string nextLine;
                while ((nextLine = sr.ReadLine()) != null)
                {
                    string[] every_row = nextLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dr[i] = every_row[i];
                    }
                    dt.Rows.Add(dr);
                }
                sr.Close();
                return dt;
            }
            catch
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("查詢");
                DataRow dr = dt.NewRow();
                dr[0] = "查無資料";
                dt.Rows.Add(dr);
                return dt ;
            }
        }
    }
}
