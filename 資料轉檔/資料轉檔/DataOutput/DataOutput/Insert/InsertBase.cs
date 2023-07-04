using DataOutput.Extension_Methods;
using DataOutput.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataOutput.Extension_Methods.transNlogTarget;

namespace DataOutput.Insert
{
    public abstract class InsertBase : MainProcessorBase
    {
        public int _etypeCount = 0;
        public int _count = 0;
        public int _success = 0;
        public int _fail = 0;
        //public FileWriter _writer = FileWriter.GetInstance();
        public Reference _reference = Reference.GetInstance();

        public abstract void SetData(DataTable dt);
        public DataTable TxtToDt(string path)
        {
            
            string folderName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(path));
            string tableName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(path))));
            string fileName = Path.GetFileNameWithoutExtension(path);
            //StringBuilder _logMessage = new StringBuilder();
            //_logMessage.AppendLine($"{DateTime.Now} 開始讀取日期：{folderName}，分公司{filaName}資料");
            //logger.Trace($"開始讀取日期：{folderName}，分公司{fileName}資料");
            logger.Write(Level.trace, $"開始讀取日期：{folderName}，分公司{fileName}資料", "","");
            try
            {
                StreamReader sr = File.OpenText(path);
                DataTable dt = new DataTable();
                string firstLine = sr.ReadLine();
                int count = 0;
                int fail = 0;

                //欄名
                string[] colName = firstLine.Split('|');
                foreach (var i in colName)
                {
                    DataColumn col = dt.Columns.Add(i.ToString());
                }
                string nextLine;


                //內容
                int r = 0;
                while ((nextLine = sr.ReadLine()) != null)
                {
                    r++;
                    string[] every_row = nextLine.Split('|');
                    DataRow dr = dt.NewRow();
                    try
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = every_row[i];
                        }
                        if (every_row.Length != colName.Length)
                        {
                            
                            throw new Exception();
                        }
                        //if (r== 1)
                        //{
                            
                        //    string row = string.Join("|", dr.ItemArray);
                        //    logger.Error($"正確檔案：{tableName} 日期：{folderName}，分公司{fileName}，第{r}筆欄位不符，總欄位{ colName.Length}個，欄位{every_row.Length}，{row}");
                        //}



                        dt.Rows.Add(dr);
                        count++;

                    }
                    catch(Exception ex)
                    {
                        //logger.Error(ex);
                        string row = string.Join("|", dr.ItemArray);

                        //dt.Rows.Add(dr);
                        fail++;
                        //_logMessage.AppendLine($"{DateTime.Now} 日期：{folderName}，分公司{filaName}，第{r}筆欄位不符，{row}");
                        //logger.Error($"檔案：{tableName} 日期：{folderName}，分公司{fileName}，第{r}筆欄位不符，總欄位{ colName.Length}個，欄位{every_row.Length}，{row}");
                        logger.Write(Level.error, $"檔案：{tableName} 日期：{folderName}，分公司{fileName}，第{r}筆欄位不符，總欄位{ colName.Length}個，欄位{every_row.Length}，{row}", "","");
                        continue;
                    }
                }

                sr.Close();
                //_logMessage.AppendLine($"{DateTime.Now} 日期：{folderName}，分公司{filaName}，轉換成功{count}筆,失敗{fail}筆");
                //_writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "\\Log\\log.txt" });
                //logger.Info($"檔案：{tableName} 日期：{folderName}，分公司{fileName}，轉換成功{count}筆,失敗{fail}筆");
                logger.Write(Level.info, $"檔案：{tableName} 日期：{folderName}，分公司{fileName}，轉換成功{count}筆,失敗{fail}筆", "","");

                return dt;
            }
            catch
            {
                //_logMessage.AppendLine($"{DateTime.Now} 日期：{folderName}，分公司{filaName}，讀取失敗");
                //_writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "\\Log\\log.txt" });
                //logger.Error($"檔案：{tableName} 日期：{folderName}，分公司{fileName}，讀取失敗");
                logger.Write(Level.error, $"檔案：{tableName} 日期：{folderName}，分公司{fileName}，讀取失敗", "", "");
                return null;
            }
        }


    }
}
