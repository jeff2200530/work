using DataOutput.Format;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataOutput
{
    public class Monitor : MainProcessorBase
    {
        public MainProcessor _process = new MainProcessor();
        public FileWriter _writer = FileWriter.GetInstance();


        public Monitor(/*FormFormat input*/)
        {

        }

        public void fileWatcher()
        {

            #region hmtht
            //Console.WriteLine($"監聽路徑{_filePath + "\\hmtht"}");
            if (!Directory.Exists(_filePath + $"\\hmtht\\file"))
            {
                Directory.CreateDirectory(_filePath + $"\\hmtht");
                Directory.CreateDirectory(_filePath + $"\\hmtht\\file");
            }


            //    Thread hmthtThread = new Thread(() =>
            //{

            FileSystemWatcher hmthtWatcher = new FileSystemWatcher
            {
                // 設定要監看的資料夾
                Path = $"{_filePath}",
                // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
                NotifyFilter = NotifyFilters.Attributes //檔案或資料夾的屬性變更
                             | NotifyFilters.CreationTime //檔案或資料夾的建立時間變更
                             | NotifyFilters.DirectoryName //資料夾名稱的變更
                             | NotifyFilters.FileName //檔案名稱的變更
                             | NotifyFilters.LastAccess //檔案或資料夾的存取時間變更
                             | NotifyFilters.LastWrite //檔案或資料夾的最後修改時間變更
                             | NotifyFilters.Security //檔案或資料夾的安全性變更
                             | NotifyFilters.Size, //檔案或資料夾的尺寸變更
                // 設定要監看的檔案類型
                //Filter = "*.txt",

                // 設定是否監看子資料夾
                IncludeSubdirectories = true,
                // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
                EnableRaisingEvents = true

            };
            hmthtWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });
            //});
            #endregion
            //#region hodrt
            ////Console.WriteLine($"監聽路徑{_filePath + "\\hodrt"}");
            //if (!Directory.Exists(_filePath + $"\\hodrt\\file"))
            //{
            //    Directory.CreateDirectory(_filePath + $"\\hodrt");
            //    Directory.CreateDirectory(_filePath + $"\\hodrt\\file");
            //}

            //Thread hodrtThread = new Thread(() =>
            //{


            //    FileSystemWatcher hodrtWatcher = new FileSystemWatcher
            //    {
            //        // 設定要監看的資料夾
            //        Path = _filePath + $"\\hodrt\\file",
            //        // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
            //        NotifyFilter = NotifyFilters.Attributes
            //                     | NotifyFilters.CreationTime
            //                     | NotifyFilters.DirectoryName
            //                     | NotifyFilters.FileName
            //                     | NotifyFilters.LastAccess
            //                     | NotifyFilters.LastWrite
            //                     | NotifyFilters.Security
            //                     | NotifyFilters.Size,
            //        // 設定要監看的檔案類型
            //        //Filter = "*.txt",

            //        // 設定是否監看子資料夾
            //        IncludeSubdirectories = true,
            //        // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
            //        EnableRaisingEvents = true

            //    };
            //    hodrtWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });

            //});
            //#endregion
            //#region trafuhord
            ////Console.WriteLine($"監聽路徑{_filePath + "\\trafuhord"}");
            //if (!Directory.Exists(_filePath + $"\\trafuhord\\file"))
            //{
            //    Directory.CreateDirectory(_filePath + $"\\trafuhord");
            //    Directory.CreateDirectory(_filePath + $"\\trafuhord\\file");
            //}

            //Thread traFuhordThread = new Thread(() =>
            //{

            //    FileSystemWatcher traFuhordWatcher = new FileSystemWatcher
            //    {
            //        // 設定要監看的資料夾
            //        Path = _filePath + $"\\trafuhord\\file",
            //        // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
            //        NotifyFilter = NotifyFilters.Attributes
            //                     | NotifyFilters.CreationTime
            //                     | NotifyFilters.DirectoryName
            //                     | NotifyFilters.FileName
            //                     | NotifyFilters.LastAccess
            //                     | NotifyFilters.LastWrite
            //                     | NotifyFilters.Security
            //                     | NotifyFilters.Size,
            //        // 設定要監看的檔案類型
            //        //Filter = "*.txt",

            //        // 設定是否監看子資料夾
            //        IncludeSubdirectories = true,
            //        // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
            //        EnableRaisingEvents = true


            //    };
            //    traFuhordWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });

            //});
            //#endregion
            //#region trafuhtrd
            ////Console.WriteLine($"監聽路徑{_filePath + "\\trafuhtrd"}");
            //if (!Directory.Exists(_filePath + $"\\trafuhtrd\\file"))
            //{
            //    Directory.CreateDirectory(_filePath + $"\\trafuhtrd");
            //    Directory.CreateDirectory(_filePath + $"\\trafuhtrd\\file");
            //}

            //Thread traFuhtrdThread = new Thread(() =>
            //{

            //    FileSystemWatcher traFuhtrdWatcher = new FileSystemWatcher
            //    {
            //        // 設定要監看的資料夾
            //        Path = $"{_filePath}\\trafuhtrd\\file",
            //        // 設定要監看的變更類型，這裡設定監看最後修改時間與修改檔名的變更事件
            //        NotifyFilter = NotifyFilters.Attributes
            //                     | NotifyFilters.CreationTime
            //                     | NotifyFilters.DirectoryName
            //                     | NotifyFilters.FileName
            //                     | NotifyFilters.LastAccess
            //                     | NotifyFilters.LastWrite
            //                     | NotifyFilters.Security
            //                     | NotifyFilters.Size,

            //        // 設定要監看的檔案類型
            //        //Filter = "*.txt",

            //        // 設定是否監看子資料夾
            //        IncludeSubdirectories = true,
            //        // 設定是否啟動元件，必須要設定為 true，否則監看事件是不會被觸發
            //        EnableRaisingEvents = true

            //    };
            //    traFuhtrdWatcher.Created += FileCreated; //檔案屬性變更、建立的檔案和刪除的檔案 });

            //});
            //#endregion
            //hmthtThread.Start();
            //hodrtThread.Start();
            //traFuhordThread.Start();
            //traFuhtrdThread.Start();



        }
        /// <summary>
        /// 新增檔案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FileCreated(object sender, FileSystemEventArgs e)
        {
            ////Console.WriteLine("create!");

            //判斷新增的為檔案再執行
            if (!e.FullPath.EndsWith(".txt"))
            {
                string folderName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(Path.GetDirectoryName(e.FullPath)));
                if (_source.Contains(folderName))
                {
                    string fileName = Path.GetFileName(e.FullPath);

                    #region Log
                    logger.Trace($"FileCreated開始執行  資料夾：{folderName}，檔案：{fileName}");
                    #endregion

                    DirectoryInfo dirInfo = new DirectoryInfo(e.FullPath);

                    FileInfo[] files = dirInfo.GetFiles();

                    logger.Trace($"FileCreated結束執行");


                    if (files.Length != 0)
                        _process.Execute(new Request() { file = files, table = folderName });

                    #region Log
                    logger.Trace($"FileCreated結束執行");
                    #endregion
                }
            }
        }
    }
}
