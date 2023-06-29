using BackupFile;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systex.Cube.Log.Spec;

namespace DataInput
{
    public class Backup : MainProcessorBase
    {
        public List<string> foldersToCompress = new List<string>();
        public List<string> foldersToDelete = new List<string>();
        public string zipFilePath = null;
        public void backup()
        {


            //取得過期的檔案、資料夾
            GetFilePathList();
            if (foldersToCompress.Count > 0)
            {
                foreach (var folder in foldersToCompress)
                {
                    if (_function != "B")
                    { SetZipFileName(folder); }
                    Execute(folder);

                }
            }


            WriteEmail();
        }
        public void Execute(string folder)
        {
            //logger.Trace($"Execute {_functionName} Start");

            switch (_function)
            {
                case ("A"):
                    {
                        try
                        {
                            if (Directory.Exists(folder) & Directory.GetParent(folder).ToString() == _filePath)
                            {
                                using (ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                                {

                                    string folderName = Path.GetFileName(folder);
                                    string entryPrefix = $"{folderName}\\";
                                    if (Directory.Exists(folder) & Directory.GetParent(folder).ToString() == _filePath)
                                    {
                                        CompressFolder(folder, zipArchive, entryPrefix);
                                    }
                                }
                            }


                            DeleteFolder(folder, _fileNameExtension);


                            //logger.Trace($"Execute {_functionName} finish!");
                        }
                        catch (Exception ex)
                        {
                            logger.Error($"ZipFile Exist! Error Message:{ex}");
                        }
                    }
                    break;
                case ("B"):
                    {

                        DeleteFolder(folder, _fileNameExtension);

                        //logger.Trace($"Execute {_functionName} finish!");
                    }
                    break;
                case ("C"):
                    {
                        try
                        {
                            if (Directory.Exists(folder) & Directory.GetParent(folder).ToString() == _filePath)
                            {
                                using (ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                                {
                                    string folderName = Path.GetFileName(folder);
                                    string entryPrefix = $"{folderName}\\";

                                    CompressFolder(folder, zipArchive, entryPrefix);


                                }
                            }
                            //logger.Trace($"Execute {_functionName} finish!");


                        }
                        catch (Exception ex)
                        {
                            logger.Error($"ZipFile Exist! Error Message:{ex}");
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 設定zip檔名
        /// </summary>
        public void SetZipFileName(string folderName)
        {
            logger.Trace("SetZipFileName Start");
            try
            {
                zipFilePath = $"{folderName}.zip";
            }
            catch (Exception ex)
            {
                logger.Error("SetZipFileName Error");
                logger.Error($"Error Message:{ex}");
                Console.WriteLine("SetZipFileName Error");
                Console.WriteLine(ex);
            }

            logger.Trace($"SetZipFileName finish ZipFileName:{Path.GetFileName(zipFilePath)}");
        }
        /// <summary>
        /// 取得壓縮路徑清單
        /// </summary>
        public void GetFilePathList()
        {
            DateTime backupEndDate = DateTime.ParseExact(DateTime.Now.AddDays(-_backupDays + 1).ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
            logger.Trace("GetFilePathList Start");
            string[] files = null;
            string[] folders = null;
            int fileCount = 0;
            int folderCount = 0;
            try
            {
                files = Directory.GetFiles(_filePath);
                folders = Directory.GetDirectories(_filePath);
                foreach (var folder in folders)
                {
                    DateTime logDate = Directory.GetCreationTime(folder);

                    if (DateTime.Compare(logDate, backupEndDate) < 0)//期間外資料夾
                    {
                        folderCount++;
                        foldersToCompress.Add(folder);
                    }
                }
                foreach (var file in files)
                {
                    DateTime logDate = File.GetCreationTime(file);

                    if (DateTime.Compare(logDate, backupEndDate) < 0)//期間外檔案
                    {
                        fileCount++;
                        foldersToCompress.Add(file);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetFilePathList Error");
                logger.Error($"Error Message:{ex}");
                Console.WriteLine("GetFilePathList Error");
                Console.WriteLine(ex);
            }

            if ((foldersToCompress.Count) == 0)
                logger.Warn("沒有過期檔案");
            else
                logger.Trace($"GetFilePathList Finish，{backupEndDate.ToString("yyyyMMdd")}前，共{folderCount}個Folder，{fileCount}個File");


        }
        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="folderPath"></param>
        public void DeleteFolder(string folderPath, string[] fileNameExtension)
        {
            try
            {

                if (fileNameExtension[0] == "*")//無指定副檔名，檔案全刪
                {
                    if (Directory.Exists(folderPath))//刪資料夾
                        Directory.Delete(folderPath, true);
                    else if (File.Exists(folderPath))//刪檔案
                        File.Delete(folderPath);
                    logger.Trace($"Delete {Path.GetFileName(folderPath)} file");
                }
                else//刪除指定附檔名的檔案
                {
                    foreach (var extension in fileNameExtension)
                    {
                        if (Directory.Exists(folderPath))//資料夾
                        {
                            string[] files = Directory.GetFiles(folderPath, "*." + extension);
                            string[] subFolders = Directory.GetDirectories(folderPath);
                            foreach (string file in files)
                            {
                                File.Delete(file);
                                logger.Trace($"Delete {Path.GetFileName(folderPath)} fileName:{Path.GetFileName(file)}");
                            }
                            foreach (string subFolder in subFolders)
                            {
                                DeleteFolder(subFolder, fileNameExtension);
                            }
                        }
                        else if (File.Exists(folderPath))//檔案
                        {

                            if (Path.GetExtension(folderPath).Replace(".", "") == extension)
                            {
                                File.Delete(folderPath);
                                logger.Trace($"Delete {Path.GetFileName(folderPath)} file");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error($"Delete {Path.GetFileName(folderPath)} File Error");
                logger.Error($"Error Message:{ex}");
                Console.WriteLine($"Delete {Path.GetFileName(folderPath)} File Error");
                Console.WriteLine(ex);
            }

        }

        /// <summary>
        /// 壓縮資料夾
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="zipArchive"></param>
        /// <param name="entryPrefix"></param>
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
                logger.Error($"Error Message:{ex}");
                Console.WriteLine($"Compress File Error");
                Console.WriteLine(ex);
            }

            if (subFolders.Length + files.Length > 0)
            {
                logger.Trace($"folder：{Path.GetFileName(folderPath)}，內有{subFolders.Length}個folder、{files.Length}個file");
                logger.Trace($"Compress {Path.GetFileName(folderPath)} file");
                Console.WriteLine(Path.GetFileName(folderPath));
            }

        }
        public void WriteEmail()
        {
            int max = 20;

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {

                if (drive.IsReady == true)
                {
                    // 可用容量 MB
                    string FreeSpace = (drive.TotalFreeSpace / (1024 * 1024)).ToString("N0");
                    int FreeSpacePercent = Convert.ToInt32(Convert.ToDouble(drive.TotalFreeSpace) / Convert.ToDouble(drive.TotalSize) * 100);

                    if (FreeSpacePercent > max)
                        logger.Trace("超過使用量，寄出通知!");
                    logger.Trace($"硬碟使用量{FreeSpacePercent}%");
                }
            }
        }
    }
}
