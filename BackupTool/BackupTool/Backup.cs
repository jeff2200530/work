using BackupFile;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataInput
{
    public class Backup : MainProcessorBase
    {
        public List<string> foldersToCompress = new List<string>();
        public string zipFilePath = null;
        public void backup()
        {
            GetFilePathList();
            if (foldersToCompress.Count > 0)
            {
                SetZipFileName();
                Execute();
            }
        }
        public void Execute()
        {
            string functionName = "";
            switch (_function)
            {
                case ("A"):
                    functionName = "Compress & Delete";
                    break;
                case ("B"):
                    functionName = "Delete";
                    break;
                case ("C"):
                    functionName = "Compress";
                    break;
            }
            logger.Trace($"Execute {functionName} Start");
            if (!File.Exists(zipFilePath))
                using (ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    foreach (string folderPath in foldersToCompress)
                    {
                        string folderName = Path.GetFileName(folderPath);
                        string entryPrefix = $"{folderName}\\";

                        switch (_function)
                        {
                            case ("A"):
                                CompressFolder(folderPath, zipArchive, entryPrefix);
                                DeleteFolder(folderPath);
                                break;
                            case ("B"):
                                DeleteFolder(folderPath);
                                break;
                            case ("C"):
                                CompressFolder(folderPath, zipArchive, entryPrefix);
                                break;
                        }
                    }
                }
            else
            {
                logger.Trace($"file：{Path.GetFileName(zipFilePath)} exist");
            }

            if (_function == "B")
                File.Delete(zipFilePath);
            Console.WriteLine("資料夾壓縮完成.");
            logger.Trace($"Execute {functionName} finish!");
        }
        public void SetZipFileName()
        {

            //設定zip檔名
            string startDate = null;
            string endDate = null;
            logger.Trace("SetZipFileName Start");
            try
            {
                startDate = Path.GetFileName(foldersToCompress[foldersToCompress.Count - 1]);
                endDate = Path.GetFileName(foldersToCompress[0]);
                zipFilePath = _filePath + "\\";
                if (foldersToCompress.Count == 1)
                {
                    zipFilePath = zipFilePath + $"{endDate}.zip";
                }
                else
                {
                    zipFilePath = zipFilePath + $"{startDate}-{endDate}.zip";
                }
            }
            catch (Exception ex)
            {
                logger.Error("SetZipFileName Error");
                Console.WriteLine("SetZipFileName Error");
                Console.WriteLine(ex);
            }

            logger.Trace("SetZipFileName finish");
        }
        public void GetFilePathList()
        {

            //取得壓縮路徑清單
            logger.Trace("GetFilePathList Start");
            try
            {
                for (int day = 0; day < _backupDays; day++)
                {
                    string folderName = DateTime.Now.AddDays(-day).ToString("yyyyMMdd");
                    string folderPath = $"{_filePath}\\{folderName}";
                    if (Directory.Exists(folderPath))
                    {
                        foldersToCompress.Add($"{_filePath}\\{folderName}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetFilePathList Error");
                Console.WriteLine("GetFilePathList Error");
                Console.WriteLine(ex);
            }
            logger.Trace($"GetFilePathList Finish，共{foldersToCompress.Count}個Folder");
        }
        public void DeleteFolder(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                    Directory.Delete(folderPath, true);
            }
            catch (Exception ex)
            {
                logger.Error($"Delete {Path.GetFileName(folderPath)} File Error");
                Console.WriteLine($"Delete {Path.GetFileName(folderPath)} File Error");
                Console.WriteLine(ex);
            }
            logger.Trace($"Delete {Path.GetFileName(folderPath)} file");
        }
        public void CompressFolder(string folderPath, ZipArchive zipArchive, string entryPrefix)
        {
            string[] files = Directory.GetFiles(folderPath);
            string[] subFolders = Directory.GetDirectories(folderPath);
            try
            {


                foreach (string file in files)
                {
                    string entryPath = entryPrefix + Path.GetFileName(file);
                    zipArchive.CreateEntryFromFile(file, entryPath);
                }

                foreach (string subFolder in subFolders)
                {
                    string subFolderName = Path.GetFileName(subFolder);
                    string subFolderEntryPrefix = entryPrefix + subFolderName + "\\";

                    CompressFolder(subFolder, zipArchive, subFolderEntryPrefix);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Compress File Error");
                Console.WriteLine($"Compress File Error");
                Console.WriteLine(ex);
            }
            if (subFolders.Length + files.Length > 0)
            {
                logger.Trace($"folder：{Path.GetFileName(folderPath)}，內有{subFolders.Length}個folder、{files.Length}個file");
                logger.Trace($"Compress {Path.GetFileName(folderPath)} file");
                Console.WriteLine(Path.GetFileName(folderPath));
                Console.WriteLine(Path.Combine("", @"D:\\test\\backupfile"));
            }
        }
    }
}
