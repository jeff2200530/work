using DataMonitor.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataMonitor
{
    public class DataProcessor : MainProcessorBase
    {
        public MainProcessor _process = new MainProcessor();
        public FileWriter _writer =FileWriter.GetInstance();
        public DataProcessor(/*FormFormat input*/)
        {
        }
        public void fileWatcher()
        {
            Console.WriteLine($"監聽路徑{_filePath + "hmtht"}");
            if (!Directory.Exists(_filePath + $"hmtht\\file"))
            {
                Directory.CreateDirectory(_filePath + $"hmtht");
                Directory.CreateDirectory(_filePath + $"hmtht\\file");
            }
            
            Thread hmthtThread = new Thread(() =>
            {
                FileSystemWatcher hmthtWatcher = new FileSystemWatcher
                {
                    // 設定要監看的資料夾
                    Path = _filePath ,
                    // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
                    NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size,
                    // 設定要監看的檔案類型
                    //Filter = "*.txt",

                    // 設定是否監看子資料夾
                    IncludeSubdirectories = true,
                    // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
                    EnableRaisingEvents = true

                }; hmthtWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });

            });
            
            Console.WriteLine($"監聽路徑{_filePath + "hodrt"}");
            if (!Directory.Exists(_filePath + $"hodrt\\file"))
            {
                Directory.CreateDirectory(_filePath + $"hodrt");
                Directory.CreateDirectory(_filePath + $"hodrt\\file");
            }

            Thread hodrtThread = new Thread(() =>
            {
                FileSystemWatcher hodrtWatcher = new FileSystemWatcher
                {
                    // 設定要監看的資料夾
                    Path = _filePath + $"hodrt\\file",
                    // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
                    NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size,
                    // 設定要監看的檔案類型
                    //Filter = "*.txt",

                    // 設定是否監看子資料夾
                    IncludeSubdirectories = true,
                    // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
                    EnableRaisingEvents = true

                };
                hodrtWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });
            });
           
            Console.WriteLine($"監聽路徑{_filePath + "trafuhord"}");
            if (!Directory.Exists(_filePath + $"trafuhord\\file"))
            {
                Directory.CreateDirectory(_filePath + $"trafuhord");
                Directory.CreateDirectory(_filePath + $"trafuhord\\file");
            }

            Thread traFuhordThread = new Thread(() => {
                FileSystemWatcher traFuhordWatcher = new FileSystemWatcher
                {
                    // 設定要監看的資料夾
                    Path = _filePath + $"trafuhord\\file",
                    // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
                    NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size,
                    // 設定要監看的檔案類型
                    //Filter = "*.txt",

                    // 設定是否監看子資料夾
                    IncludeSubdirectories = true,
                    // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
                    EnableRaisingEvents = true
                };
                traFuhordWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });
            });
          
            Console.WriteLine($"監聽路徑{_filePath + "trafuhtrd"}");
            if (!Directory.Exists(_filePath + $"trafuhtrd\\file"))
            {
                Directory.CreateDirectory(_filePath + $"trafuhtrd");
                Directory.CreateDirectory(_filePath + $"trafuhtrd\\file");
            }

            Thread traFuhtrdThread = new Thread(() => {
                FileSystemWatcher traFuhtrdWatcher = new FileSystemWatcher
                {
                    // 設定要監看的資料夾
                    Path = _filePath + $"trafuhtrd\\file",
                    // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
                    NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size,
                    // 設定要監看的檔案類型
                    //Filter = "*.txt",

                    // 設定是否監看子資料夾
                    IncludeSubdirectories = true,
                    // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
                    EnableRaisingEvents = true

                };
                traFuhtrdWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });
            });

            hmthtThread.Start();
            hodrtThread.Start();
            traFuhordThread.Start();
            traFuhtrdThread.Start();
        }
        /// <summary>
        /// 添加檔案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FileCreated(object sender, FileSystemEventArgs e)
        {
            //判斷添加的是檔案才執行
            if (!e.FullPath.EndsWith(".txt"))
            {
                //資料表名稱
                string folderName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(Path.GetDirectoryName(e.FullPath)));
                //檔案日期
                string fileName = Path.GetFileName(e.FullPath);
                

                StringBuilder _logMessage = new StringBuilder();
                _logMessage.AppendLine($"{DateTime.Now} FileCreated開始執行  檔案：{folderName}，日期{fileName}");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });

                DirectoryInfo dirInfo = new DirectoryInfo(e.FullPath);
                //取得那天日期資料夾的所有分公司資料
                FileInfo[] files = dirInfo.GetFiles();

                //執行，傳入檔案及資料表名稱
                if(_process!=null)
                _process.Execute(new Request() { file = files, table = folderName });

                _logMessage = new StringBuilder();
                _logMessage.AppendLine($"{DateTime.Now} FileCreated結束執行");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });
            }
            
        }



    }
}
