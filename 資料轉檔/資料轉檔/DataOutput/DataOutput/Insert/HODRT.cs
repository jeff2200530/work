using DataOutput.Extension_Methods;
using DataOutput.Format;
using DataTransfer.Format;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataOutput.Extension_Methods.transNlogTarget;

namespace DataOutput.Insert
{
    public class HODRT : InsertBase
    {
        public Dictionary<string, List<DataRow>> _cntDic = new Dictionary<string, List<DataRow>>();
        public Dictionary<string, string> _origsourceDic = new Dictionary<string, string>();
        public override void SetData(DataTable dt)
        {
            #region Log
            //logger.Trace($"SetDate開始");
            logger.Write(Level.trace, $"SetDate開始", "","");

            #endregion
            int count = dt.Rows.Count;
            int success = 0;
            int fail = 0;
            try
            {
                _db.Open();
                #region Log
               // logger.Trace($"sql連線成功");
                logger.Write(Level.trace, $"sql連線成功", "","");

                #endregion
            }
            catch (Exception ex)
            {
                #region Log
                //logger.Error($"sql連線失敗 錯誤訊息:{ex}");
                logger.Write(Level.error, $"sql連線失敗 錯誤訊息:{ex}", "","");

                #endregion
            }


            for (int row = 0; row < dt.Rows.Count; row++)
            {
                #region 設定cnt
                string key = dt.Rows[row]["TDATE"].ToString() + "," + dt.Rows[row]["BHNO"].ToString() + "," + dt.Rows[row]["CSEQ"].ToString() + "," + dt.Rows[row]["DSEQ"].ToString();
                if (_cntDic.ContainsKey(key) == false)
                {
                    List<DataRow> list = new List<DataRow>();
                    list.Add(dt.Rows[row]);
                    _cntDic.Add(key, list);
                }
                else
                {
                    _cntDic[key].Add(dt.Rows[row]);
                }
                #endregion

                #region 設定origsource
                if (_reference._secOrignDic.ContainsKey(dt.Rows[row]["ORIGN"].ToString()))
                    dt.Rows[row]["ORIGN"] = _reference._secOrignDic[dt.Rows[row]["ORIGN"].ToString()];

                if (dt.Rows[row]["OTYPE"].ToString() == "I" & !_origsourceDic.ContainsKey(key))
                {
                    _origsourceDic.Add(key, dt.Rows[row]["ORIGN"].ToString());
                }
                #endregion

                string table = "shord";
                string strSQL = $"insert into {table} (tdate,rseq,tseq,bhno,cseq,netseq,dseq,cnt,source,origsource,otype,mtype,etype,bstype,ttype,ordknd,stock,stockname,tqty,efqty,mqty,cqty,uqty,bfqty,afqty,price,preprice,pcond,sales,operator,reserve,idate,itime,ftime,stime,clientip,serverip,replyfrom,rejecttag,statuscode,statustext,rtime,tsetime,cano,plaintext,ciphertext,modifytime)　values (@tdate,@rseq,@tseq,@bhno,@cseq,@netseq,@dseq,@cnt,@source,@origsource,@otype,@mtype,@etype,@bstype,@ttype,@ordknd,@stock,@stockname,@tqty,@efqty,@mqty,@cqty,@uqty,@bfqty,@afqty,@price,@preprice,@pcond,@sales,@operator,@reserve,@idate,@itime,@ftime,@stime,@clientip,@serverip,@replyfrom,@rejecttag,@statuscode,@statustext,@rtime,@tsetime,@cano,@plaintext,@ciphertext,@modifytime)";
                SqlCommand writeCommand = new SqlCommand(strSQL, _db);
                try
                {
                    #region 設定參數
                    writeCommand.Parameters.Clear();
                    writeCommand.Parameters.AddWithValue("@tdate", dt.Rows[row]["TDATE"]);
                    writeCommand.Parameters.AddWithValue("@rseq", dt.Rows[row]["NETNO"]);
                    writeCommand.Parameters.AddWithValue("@tseq", dt.Rows[row]["TSEQ"]);
                    writeCommand.Parameters.AddWithValue("@bhno", dt.Rows[row]["BHNO"]);
                    writeCommand.Parameters.AddWithValue("@cseq", dt.Rows[row]["CSEQ"]);
                    writeCommand.Parameters.AddWithValue("@netseq", dt.Rows[row]["NETSEQ"]);
                    writeCommand.Parameters.AddWithValue("@dseq", dt.Rows[row]["DSEQ"]);
                    writeCommand.Parameters.AddWithValue("@cnt", _cntDic[key].Count);

                    writeCommand.Parameters.AddWithValue("@source", dt.Rows[row]["ORIGN"].ToString());

                    string origsource = "";
                    if (_origsourceDic.ContainsKey(key))
                        origsource = _origsourceDic[key];

                    writeCommand.Parameters.AddWithValue("@origsource", origsource);
                    writeCommand.Parameters.AddWithValue("@otype", dt.Rows[row]["OTYPE"]);
                    writeCommand.Parameters.AddWithValue("@mtype", dt.Rows[row]["MTYPE"]);
                    writeCommand.Parameters.AddWithValue("@etype", dt.Rows[row]["ETYPE"]);
                    writeCommand.Parameters.AddWithValue("@bstype", dt.Rows[row]["BSTYPE"]);
                    writeCommand.Parameters.AddWithValue("@ttype", dt.Rows[row]["TTYPE"]);
                    writeCommand.Parameters.AddWithValue("@ordknd", dt.Rows[row]["ORDKND"]);
                    writeCommand.Parameters.AddWithValue("@stock", dt.Rows[row]["STOCK"]);
                    string stockname = "";
                    if (_reference._stockDic.ContainsKey(dt.Rows[row]["STOCK"].ToString()))
                        stockname = _reference._stockDic[dt.Rows[row]["STOCK"].ToString()];
                    writeCommand.Parameters.AddWithValue("@stockname", stockname);
                    writeCommand.Parameters.AddWithValue("@tqty", dt.Rows[row]["QTY"]);
                    writeCommand.Parameters.AddWithValue("@efqty", dt.Rows[row]["EFQTY"]);
                    writeCommand.Parameters.AddWithValue("@mqty", dt.Rows[row]["MQTY"]);
                    writeCommand.Parameters.AddWithValue("@cqty", (int.Parse(dt.Rows[row]["EFQTY"].ToString()) - int.Parse(dt.Rows[row]["AFQTY"].ToString())).ToString());
                    writeCommand.Parameters.AddWithValue("@uqty", (int.Parse(dt.Rows[row]["AFQTY"].ToString()) - int.Parse(dt.Rows[row]["MQTY"].ToString())).ToString());
                    writeCommand.Parameters.AddWithValue("@bfqty", dt.Rows[row]["BFQTY"]);
                    writeCommand.Parameters.AddWithValue("@afqty", dt.Rows[row]["AFQTY"]);
                    writeCommand.Parameters.AddWithValue("@price", dt.Rows[row]["PRICE"]);
                    writeCommand.Parameters.AddWithValue("@preprice", dt.Rows[row]["PREPRICE"]);
                    writeCommand.Parameters.AddWithValue("@pcond", dt.Rows[row]["PCOND"]);
                    writeCommand.Parameters.AddWithValue("@sales", dt.Rows[row]["SALES"]);
                    writeCommand.Parameters.AddWithValue("@operator", dt.Rows[row]["OPERATOR"]);
                    writeCommand.Parameters.AddWithValue("@reserve", dt.Rows[row]["PREORDER"]);
                    writeCommand.Parameters.AddWithValue("@idate", dt.Rows[row]["IDATE"]);
                    writeCommand.Parameters.AddWithValue("@itime", dt.Rows[row]["ITIME"]);
                    writeCommand.Parameters.AddWithValue("@ftime", "");
                    writeCommand.Parameters.AddWithValue("@stime", "");
                    writeCommand.Parameters.AddWithValue("@clientip", dt.Rows[row]["IP"]);
                    writeCommand.Parameters.AddWithValue("@serverip", dt.Rows[row]["HOSTIP"]);
                    writeCommand.Parameters.AddWithValue("@replyfrom", dt.Rows[row]["REPLYFROM"]);
                    writeCommand.Parameters.AddWithValue("@rejecttag", dt.Rows[row]["REJECTTAG"]);
                    writeCommand.Parameters.AddWithValue("@statuscode", dt.Rows[row]["ERRCODE"]);
                    writeCommand.Parameters.AddWithValue("@statustext", dt.Rows[row]["ERRMSG"]);
                    writeCommand.Parameters.AddWithValue("@rtime", dt.Rows[row]["RTIME"]);
                    writeCommand.Parameters.AddWithValue("@tsetime", dt.Rows[row]["TSETIME"]);
                    writeCommand.Parameters.AddWithValue("@cano", "");
                    writeCommand.Parameters.AddWithValue("@plaintext", "");
                    writeCommand.Parameters.AddWithValue("@ciphertext", dt.Rows[row]["SIGNDATA"]);
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
                    logger.Write(Level.error, $"寫入第{row + 1}筆失敗，失敗{fail}筆，SQL語法{strSQL} 錯誤訊息:{ex}", "","");
                    #endregion
                    continue;
                }
            }
            _count += count;
            _db.Close();
            #region Log
            //logger.Info($"檔案：hodrt 日期：{ dt.Rows[0]["TDATE"]}，分公司{ dt.Rows[0]["BHNO"]}，成功{success}筆，失敗{fail}筆，SetDate結束");
            logger.Write(Level.info, $"檔案：hodrt 日期：{ dt.Rows[0]["TDATE"]}，分公司{ dt.Rows[0]["BHNO"]}，成功{success}筆，失敗{fail}筆，SetDate結束", "","");
            #endregion
        }
    }
}
