﻿using DataOutput.Extension_Methods;
using DataOutput.Format;
using DataTransfer.Format;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataOutput.Extension_Methods.transNlogTarget;

namespace DataOutput.Insert
{
    public class TraFuhord : InsertBase
    {
        public Dictionary<string, List<DataRow>> _cntDic = new Dictionary<string, List<DataRow>>();
        public Dictionary<string, string> _origsourceDic = new Dictionary<string, string>();
        public override void SetData(DataTable dt)
        {
            #region Log
            //logger.Trace($"SetDate開始");
            logger.Write(Level.trace, $"SetDate開始", "", "");

            #endregion

            #region 設定cnt
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                string key = dt.Rows[row]["orddt"].ToString() + "," + dt.Rows[row]["ordno"].ToString() + "," + dt.Rows[row]["company"].ToString() + "," + dt.Rows[row]["actno"].ToString();
                if (!_cntDic.ContainsKey(key))
                {
                    List<DataRow> list = new List<DataRow>();
                    list.Add(dt.Rows[row]);
                    _cntDic.Add(key, list);
                }
                else
                {
                    _cntDic[key].Add(dt.Rows[row]);
                }

            }
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
            catch(Exception ex)
            {
                #region Log
                //logger.Error($"sql連線失敗 錯誤訊息:{ex}");
                logger.Write(Level.error, $"sql連線失敗 錯誤訊息:{ex}", "", "");
                #endregion
            }

            for (int row = 0; row < dt.Rows.Count; row++)
            {
            

                #region 設定 origsource
                string key = dt.Rows[row]["orddt"].ToString() + "," + dt.Rows[row]["ordno"].ToString() + "," + dt.Rows[row]["company"].ToString() + "," + dt.Rows[row]["actno"].ToString();

                if (_reference._futOrignDic.ContainsKey(dt.Rows[row]["srctype"].ToString()))
                    dt.Rows[row]["srctype"] = _reference._futOrignDic[dt.Rows[row]["srctype"].ToString()];

                if (dt.Rows[row]["ordcmd"].ToString() == "0" & !_origsourceDic.ContainsKey(key))
                    _origsourceDic.Add(key, dt.Rows[row]["srctype"].ToString());
                #endregion
                string table = "fhord";
                string strSQL = $"insert into {table} (tdate,rseq,tseq,netseq,ordno,bhno,cseq,exchange,mtype,etype,comtype,comno,comym,comname,strikeprice,cptype,bstype,opentype,dtrade,itype,ordknd,price,preprice,ordtype,cmd,currency,legsno,source,origsource,cnt,tqty,uqty,cqty,mqty,sales,operator,reserve,idate,itime,ftime,stime,clientip,serverip,statuscode,statustext,replyfrom,rtime,tfetime,cano,plaintext,ciphertext,modifytime) values (@tdate,@rseq,@tseq,@netseq,@ordno,@bhno,@cseq,@exchange,@mtype,@etype,@comtype,@comno,@comym,@comname,@strikeprice,@cptype,@bstype,@opentype,@dtrade,@itype,@ordknd,@price,@preprice,@ordtype,@cmd,@currency,@legsno,@source,@origsource,@cnt,@tqty,@uqty,@cqty,@mqty,@sales,@operator,@reserve,@idate,@itime,@ftime,@stime,@clientip,@serverip,@statuscode,@statustext,@replyfrom,@rtime,@tfetime,@cano,@plaintext,@ciphertext,@modifytime)";
                SqlCommand writeCommand = new SqlCommand(strSQL, _db);
                try
                {
                    #region 設定參數
                    writeCommand.Parameters.Clear();
                    writeCommand.Parameters.AddWithValue("@tdate", dt.Rows[row]["orddt"].ToString().Replace("/",""));
                    writeCommand.Parameters.AddWithValue("@rseq", "");//empty
                    writeCommand.Parameters.AddWithValue("@tseq", dt.Rows[row]["seqno"]);
                    writeCommand.Parameters.AddWithValue("@netseq", dt.Rows[row]["seqno"]);
                    writeCommand.Parameters.AddWithValue("@ordno", dt.Rows[row]["ordno"]);
                    writeCommand.Parameters.AddWithValue("@bhno", dt.Rows[row]["company"]);
                    writeCommand.Parameters.AddWithValue("@cseq", dt.Rows[row]["actno"]);

                    string exchange = dt.Rows[row]["exhno"].ToString() + "EX";

                    writeCommand.Parameters.AddWithValue("@exchange",exchange);
                    writeCommand.Parameters.AddWithValue("@mtype", dt.Rows[row]["mtype"]);
                    string etype = dt.Rows[row]["etype"].ToString();
                    if (etype == "R")
                    {
                        etype = "I";
                    }

                    writeCommand.Parameters.AddWithValue("@etype", etype);
                    string comtype = dt.Rows[row]["comtype"].ToString();
                    if (comtype == "0")
                        comtype = "F";
                    else if (comtype == "1")
                        comtype = "O";
                    writeCommand.Parameters.AddWithValue("@comtype",comtype);
                    writeCommand.Parameters.AddWithValue("@comno", dt.Rows[row]["comno"]);
                    writeCommand.Parameters.AddWithValue("@comym", dt.Rows[row]["comym"]);

                    string commodity = "";
                    if (_reference._commodityDic.ContainsKey(dt.Rows[row]["comno"].ToString()))
                        commodity = _reference._commodityDic[dt.Rows[row]["comno"].ToString()];
                    writeCommand.Parameters.AddWithValue("@comname", commodity);//查詢商品名稱

                    writeCommand.Parameters.AddWithValue("@strikeprice", dt.Rows[row]["stkprcdec"]);
                    writeCommand.Parameters.AddWithValue("@cptype", dt.Rows[row]["callput"]);
                    writeCommand.Parameters.AddWithValue("@bstype", dt.Rows[row]["ps"]);

                    string opentype = dt.Rows[row]["newps"].ToString();
                    if (opentype == "Y")
                        opentype = "0";
                    else if (opentype == "N")
                        opentype = "1";

                    writeCommand.Parameters.AddWithValue("@opentype", opentype);
                    writeCommand.Parameters.AddWithValue("@dtrade", dt.Rows[row]["dtrade"]);

                    string itype = "0";
                    foreach (DataRow item in _cntDic[key])
                    {
                        //判斷同key 有沒有另外一個腳位=2 & 和自己相同cnt的資料 true=1(複式單)
                        if (item[26].ToString() == "2" & item[41].ToString() == dt.Rows[row]["omcnt"].ToString())
                        {
                            itype = "1";
                        }
                    }
                    writeCommand.Parameters.AddWithValue("@itype", itype);

                    writeCommand.Parameters.AddWithValue("@ordknd", dt.Rows[row]["effknd"]);
                    writeCommand.Parameters.AddWithValue("@price", dt.Rows[row]["ordprcdec"]);
                    writeCommand.Parameters.AddWithValue("@preprice", dt.Rows[row]["preprcdec"]);
                    writeCommand.Parameters.AddWithValue("@ordtype", dt.Rows[row]["ordtype"]);
                    writeCommand.Parameters.AddWithValue("@cmd", dt.Rows[row]["ordcmd"]);

                    string currency = "";
                    if (dt.Rows[row]["currency"].ToString() == "NTT")
                        currency = "NTD";
                    else if (dt.Rows[row]["currency"].ToString() == "UST")
                        currency = "USD";
                    else
                        currency = dt.Rows[row]["currency"].ToString();
                    writeCommand.Parameters.AddWithValue("@currency", currency);//修改文字


                    writeCommand.Parameters.AddWithValue("@legsno", dt.Rows[row]["legsno"]);
                    writeCommand.Parameters.AddWithValue("@source", dt.Rows[row]["srctype"]);

                    string origsource = "";
                    if (_origsourceDic.ContainsKey(key))
                        origsource = _origsourceDic[key];
                    writeCommand.Parameters.AddWithValue("@origsource", origsource);

                    writeCommand.Parameters.AddWithValue("@cnt", dt.Rows[row]["omcnt"]);
                    writeCommand.Parameters.AddWithValue("@tqty", dt.Rows[row]["ordqty"]);
                    writeCommand.Parameters.AddWithValue("@uqty", dt.Rows[row]["remqty"]);
                    writeCommand.Parameters.AddWithValue("@cqty", dt.Rows[row]["cxlqty"]);
                    writeCommand.Parameters.AddWithValue("@mqty", dt.Rows[row]["dealqty"]);
                    writeCommand.Parameters.AddWithValue("@sales", dt.Rows[row]["aeno"]);
                    writeCommand.Parameters.AddWithValue("@operator", dt.Rows[row]["operator"]);

                    writeCommand.Parameters.AddWithValue("@reserve", "N");

                    writeCommand.Parameters.AddWithValue("@idate", dt.Rows[row]["orddt"].ToString().Replace("/", ""));
                    string itime = dt.Rows[row]["ordtm"].ToString().Replace(":", "") + "000";
                    writeCommand.Parameters.AddWithValue("@itime", itime);
                    writeCommand.Parameters.AddWithValue("@ftime", "");
                    writeCommand.Parameters.AddWithValue("@stime", "");
                    writeCommand.Parameters.AddWithValue("@clientip", dt.Rows[row]["ip"]);
                    writeCommand.Parameters.AddWithValue("@serverip", dt.Rows[row]["hostip"]);
                    writeCommand.Parameters.AddWithValue("@statuscode", dt.Rows[row]["code"]);
                    writeCommand.Parameters.AddWithValue("@statustext", dt.Rows[row]["errormsg"]);

                    string replyfrom = "";
                    string code = dt.Rows[row]["code"].ToString().Trim();
                    if (code == "9996" | code == "9997" | code == "9998")
                        replyfrom = "CUBE";
                    else
                        replyfrom = "BE";
                    writeCommand.Parameters.AddWithValue("@replyfrom", replyfrom);

                    string rtime = dt.Rows[row]["rectime"].ToString().Replace(":", "") + "000";
                    writeCommand.Parameters.AddWithValue("@rtime",rtime);
                    string tfetime = dt.Rows[row]["rectime"].ToString().Replace(":", "");
                    writeCommand.Parameters.AddWithValue("@tfetime", tfetime);
                    writeCommand.Parameters.AddWithValue("@cano", "");
                    writeCommand.Parameters.AddWithValue("@plaintext", "");
                    writeCommand.Parameters.AddWithValue("@ciphertext", ""); 
                    writeCommand.Parameters.AddWithValue("@modifytime", DateTime.Now.ToString("yyyyMMdd hh:mm:ss:fff"));
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
                    logger.Write(Level.trace, $"寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}", dt.Rows[row]["orddt"].ToString().Replace("/", ""),table);
                    //logger.Trace($"ordno:{dt.Rows[row]["ordno"]} company:{dt.Rows[row]["company"]} actno:{dt.Rows[row]["orddt"]} legsno:{dt.Rows[row]["legsno"]} omcnt:{dt.Rows[row]["omcnt"]} itype:{itype}");
                    #endregion
                    ////Console.WriteLine($"{DateTime.Now} 寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");

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
            //logger.Info($"檔案：traFuhord 日期：{ dt.Rows[0]["orddt"].ToString().Replace("/", "")}，分公司{  dt.Rows[0]["company"]}，成功{success}筆，失敗{fail}筆，SetDate結束");
            logger.Write(Level.info, $"檔案：traFuhord 日期：{ dt.Rows[0]["orddt"].ToString().Replace("/", "")}，分公司{  dt.Rows[0]["company"]}，成功{success}筆，失敗{fail}筆，SetDate結束", "", "");
         
            #endregion
        }
    }
}

