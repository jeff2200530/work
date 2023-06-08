using DataMonitor.Format;
using DataTransfer.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataMonitor.Insert
{
    public class HMTHT : InsertBase
    {
        public string stockName = "";
        public Dictionary<string, List<HMTHTFormat>> _cntDic = new Dictionary<string, List<HMTHTFormat>>();
        
        public override void SetData(DataTable dt)
        {
            StringBuilder _logMessage = new StringBuilder();
            _logMessage.AppendLine($"{DateTime.Now} SetDate開始");

            int count = dt.Rows.Count;
            int success = 0;
            int fail = 0;

            _db.Open();
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                string table = "shtrd";
                string strSQL = $"insert into {table} (tdate,mdate,bhno,cseq,netseq,dseq,mseq,mtype,etype,ttype,bstype,stock,stockname,origsource,mtime,rtime,mqty,mprice,sales,modifytime) values (@tdate,@mdate,@bhno,@cseq,@netseq,@dseq,@mseq,@mtype,@etype,@ttype,@bstype,@stock,@stockname,@origsource,@mtime,@rtime,@mqty,@mprice,@sales,@modifytime)";
                SqlCommand writeCommand = new SqlCommand(strSQL, _db);
                try
                {
                    writeCommand.Parameters.Clear();
                    writeCommand.Parameters.AddWithValue("@tdate", dt.Rows[row]["TDATE"]);
                    writeCommand.Parameters.AddWithValue("@mdate", dt.Rows[row]["TDATE"]);
                    writeCommand.Parameters.AddWithValue("@bhno", dt.Rows[row]["BHNO"]);
                    writeCommand.Parameters.AddWithValue("@cseq", dt.Rows[row]["CSEQ"]);
                    writeCommand.Parameters.AddWithValue("@netseq", dt.Rows[row]["NETSEQ"]);
                    writeCommand.Parameters.AddWithValue("@dseq", dt.Rows[row]["DSEQ"]);
                    writeCommand.Parameters.AddWithValue("@mseq", dt.Rows[row]["MSEQ"]);
                    writeCommand.Parameters.AddWithValue("@mtype", dt.Rows[row]["MTYPE"]);
                    writeCommand.Parameters.AddWithValue("@etype", dt.Rows[row]["ETYPE"]);
                    writeCommand.Parameters.AddWithValue("@ttype", dt.Rows[row]["TTYPE"]);
                    writeCommand.Parameters.AddWithValue("@bstype", dt.Rows[row]["BSTYPE"]);
                    writeCommand.Parameters.AddWithValue("@stock", dt.Rows[row]["STOCK"]);

                    SetStockName(dt.Rows[row], out stockName);
                 
                    writeCommand.Parameters.AddWithValue("@stockname", stockName);


                    if (_reference._secOrignDic.ContainsKey(dt.Rows[row]["ORIGN"].ToString()))
                        dt.Rows[row]["ORIGN"] = _reference._secOrignDic[dt.Rows[row]["ORIGN"].ToString()];

                    writeCommand.Parameters.AddWithValue("@origsource", dt.Rows[row]["ORIGN"]);
                    writeCommand.Parameters.AddWithValue("@mtime", dt.Rows[row]["MTIME"]);
                    writeCommand.Parameters.AddWithValue("@rtime", dt.Rows[row]["RTIME"]);
                    writeCommand.Parameters.AddWithValue("@mqty", dt.Rows[row]["QTY"]);
                    writeCommand.Parameters.AddWithValue("@mprice", dt.Rows[row]["PRICE"]);
                    writeCommand.Parameters.AddWithValue("@sales", dt.Rows[row]["SALES"]);
                    writeCommand.Parameters.AddWithValue("@modifytime", DateTime.Now.ToString("yyyyMMdd"));

                    foreach (SqlParameter parameter in writeCommand.Parameters)
                    {
                        strSQL = strSQL.Replace(parameter.ParameterName.ToString(), parameter.Value.ToString());
                    }

                    writeCommand.ExecuteNonQuery();

                   

                    success++;
                    _success++;
                    _logMessage.AppendLine($"{DateTime.Now} 寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");
                    Console.WriteLine($"{DateTime.Now} 寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    fail++;
                    _fail++;
                    _logMessage.AppendLine($"{DateTime.Now} 寫入第{row + 1}筆失敗，失敗{fail}筆，SQL語法{strSQL}");

                    continue;
                }
            }
            _count += count;
            _db.Close();
            _logMessage.AppendLine($"{DateTime.Now} SetDate結束，成功{success}筆，失敗{fail}筆");
            _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });
        }

        public void SetStockName(DataRow row,out string stockName )
        {
            if (_reference._stockDic.ContainsKey(row["STOCK"].ToString()))
                stockName = _reference._stockDic[row["STOCK"].ToString()];
            else
                stockName = "";

        }



    }


   
}
