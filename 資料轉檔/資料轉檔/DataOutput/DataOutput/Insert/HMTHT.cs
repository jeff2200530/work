using DataOutput.Extension_Methods;
using DataOutput.Format;
using DataTransfer.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static DataOutput.Extension_Methods.transNlogTarget;

namespace DataOutput.Insert
{
    public class HMTHT : InsertBase
    {
        public Dictionary<string, List<HMTHTFormat>> _cntDic = new Dictionary<string, List<HMTHTFormat>>();
        
        public override void SetData(DataTable dt)
        {
            #region Log
            logger.Trace($"SetDate開始");
            #endregion
            int count = dt.Rows.Count;
            int success = 0;
            int fail = 0;

            try
            {
                _db.Open();
                #region Log
                //logger.Trace($"sql連線成功");
                logger.Write(Level.trace, $"sql連線成功", "", "");

                #endregion 
            }
            catch (Exception ex)
            {
                #region Log
                //logger.Error($"sql連線失敗 錯誤訊息:{ex}");
                logger.Write(Level.error, $"sql連線失敗 錯誤訊息:{ex}", "", "");
                #endregion
            }

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                string table = "shtrd";
                string strSQL = $"insert into {table} (tdate,mdate,bhno,cseq,netseq,dseq,mseq,mtype,etype,ttype,bstype,stock,stockname,origsource,mtime,rtime,mqty,mprice,sales,modifytime) values (@tdate,@mdate,@bhno,@cseq,@netseq,@dseq,@mseq,@mtype,@etype,@ttype,@bstype,@stock,@stockname,@origsource,@mtime,@rtime,@mqty,@mprice,@sales,@modifytime)";
                SqlCommand writeCommand = new SqlCommand(strSQL, _db);
                try
                {
                    #region 參數設定
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
                    string stockname = "";
                    if (_reference._stockDic.ContainsKey(dt.Rows[row]["STOCK"].ToString()))
                        stockname = _reference._stockDic[dt.Rows[row]["STOCK"].ToString()];
                    writeCommand.Parameters.AddWithValue("@stockname", stockname);


                    if (_reference._secOrignDic.ContainsKey(dt.Rows[row]["ORIGN"].ToString()))
                        dt.Rows[row]["ORIGN"] = _reference._secOrignDic[dt.Rows[row]["ORIGN"].ToString()];

                    writeCommand.Parameters.AddWithValue("@origsource", dt.Rows[row]["ORIGN"]);
                    writeCommand.Parameters.AddWithValue("@mtime", dt.Rows[row]["MTIME"]);
                    writeCommand.Parameters.AddWithValue("@rtime", dt.Rows[row]["RTIME"]);
                    writeCommand.Parameters.AddWithValue("@mqty", dt.Rows[row]["QTY"]);
                    writeCommand.Parameters.AddWithValue("@mprice", dt.Rows[row]["PRICE"]);
                    writeCommand.Parameters.AddWithValue("@sales", dt.Rows[row]["SALES"]);
                    writeCommand.Parameters.AddWithValue("@modifytime", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"));
                    #endregion

                    #region 重組sql語法
                    foreach (SqlParameter parameter in writeCommand.Parameters)
                    {
                        strSQL = strSQL.ReplaceOne(parameter.ParameterName.ToString(), $"'{parameter.Value.ToString()}'");
                    }
                    #endregion

                    writeCommand.ExecuteNonQuery();

                    success++;
                    _success++;
                    #region Log
                    //logger.Trace($"寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");
                    logger.Write(Level.trace, $"寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}", dt.Rows[row]["TDATE"].ToString(),table);

                    #endregion
                    //Console.WriteLine($"{DateTime.Now} 寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex);
                    fail++;
                    _fail++;
                    #region Log
                    //logger.Error($"寫入第{row + 1}筆失敗，失敗{fail}筆，SQL語法{strSQL} 錯誤訊息:{ex}");
                    logger.Write(Level.error, $"寫入第{row + 1}筆失敗，失敗{fail}筆，SQL語法{strSQL} 錯誤訊息:{ex}", "", "");
                    #endregion
                    continue;
                }
            }
            _count += count;
            _db.Close();
            #region Log 
            //logger.Info($"檔案：hmtht 日期：{ dt.Rows[0]["TDATE"]}，分公司{ dt.Rows[0]["BHNO"]}，成功{success}筆，失敗{fail}筆，SetDate結束");
            logger.Write(Level.info, $"檔案：hmtht 日期：{ dt.Rows[0]["TDATE"].ToString()}，分公司{ dt.Rows[0]["BHNO"].ToString()}，成功{success}筆，失敗{fail}筆，SetDate結束", "", "");
            #endregion
        }
    }
}
