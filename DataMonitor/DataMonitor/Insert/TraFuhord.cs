using DataMonitor.Format;
using DataTransfer.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Insert
{
    public class TraFuhord : InsertBase
    {
        public Dictionary<string, List<DataRow>> _cntDic = new Dictionary<string, List<DataRow>>();
        public Dictionary<string, string> _origsourceDic = new Dictionary<string, string>();
        public override void SetData(DataTable dt)
        {
            StringBuilder _logMessage = new StringBuilder();
            _logMessage.AppendLine($"{DateTime.Now} SetDate開始");


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
            _cntDic.ToString();

            //Console.WriteLine();


            int count = dt.Rows.Count;
            int success = 0;
            int fail = 0;

            _db.Open();
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                string key = dt.Rows[row]["orddt"].ToString() + "," + dt.Rows[row]["ordno"].ToString() + "," + dt.Rows[row]["company"].ToString() + "," + dt.Rows[row]["actno"].ToString();

                if (_reference._futOrignDic.ContainsKey(dt.Rows[row]["srctype"].ToString()))
                    dt.Rows[row]["srctype"] = _reference._futOrignDic[dt.Rows[row]["srctype"].ToString()];

                if (dt.Rows[row]["ordcmd"].ToString() == "0" & !_origsourceDic.ContainsKey(key))
                    _origsourceDic.Add(key, dt.Rows[row]["srctype"].ToString());

                string table = "fhord";
                string strSQL = $"insert into {table} (tdate,rseq,tseq,netseq,ordno,bhno,cseq,exchange,mtype,etype,comtype,comno,comym,comname,strikeprice,cptype,bstype,opentype,dtrade,itype,ordknd,price,preprice,ordtype,cmd,currency,legsno,source,origsource,cnt,tqty,uqty,cqty,mqty,sales,operator,idate,itime,ftime,stime,clientip,serverip,statuscode,statustext,replyfrom,rtime,tfetime,cano,plaintext,ciphertext,modifytime) values (@tdate,@rseq,@tseq,@netseq,@ordno,@bhno,@cseq,@exchange,@mtype,@etype,@comtype,@comno,@comym,@comname,@strikeprice,@cptype,@bstype,@opentype,@dtrade,@itype,@ordknd,@price,@preprice,@ordtype,@cmd,@currency,@legsno,@source,@origsource,@cnt,@tqty,@uqty,@cqty,@mqty,@sales,@operator,@idate,@itime,@ftime,@stime,@clientip,@serverip,@statuscode,@statustext,@replyfrom,@rtime,@tfetime,@cano,@plaintext,@ciphertext,@modifytime)";
                SqlCommand writeCommand = new SqlCommand(strSQL, _db);
                try
                {
                    writeCommand.Parameters.Clear();
                    writeCommand.Parameters.AddWithValue("@tdate", dt.Rows[row]["orddt"]);
                    writeCommand.Parameters.AddWithValue("@rseq", "");//empty
                    writeCommand.Parameters.AddWithValue("@tseq", dt.Rows[row]["seqno"]);
                    writeCommand.Parameters.AddWithValue("@netseq", dt.Rows[row]["seqno"]);
                    writeCommand.Parameters.AddWithValue("@ordno", dt.Rows[row]["ordno"]);
                    writeCommand.Parameters.AddWithValue("@bhno", dt.Rows[row]["company"]);
                    writeCommand.Parameters.AddWithValue("@cseq", dt.Rows[row]["actno"]);
                    writeCommand.Parameters.AddWithValue("@exchange", dt.Rows[row]["exhno"]);
                    writeCommand.Parameters.AddWithValue("@mtype", dt.Rows[row]["mtype"]);
                    writeCommand.Parameters.AddWithValue("@etype", dt.Rows[row]["etype"]);
                    writeCommand.Parameters.AddWithValue("@comtype", dt.Rows[row]["comtype"]);
                    writeCommand.Parameters.AddWithValue("@comno", dt.Rows[row]["comno"]);
                    writeCommand.Parameters.AddWithValue("@comym", dt.Rows[row]["comym"]);

                    string commodity = "";
                    if (_reference._commodityDic.ContainsKey(dt.Rows[row]["comno"].ToString()))
                        commodity = _reference._commodityDic[dt.Rows[row]["comno"].ToString()];
                    writeCommand.Parameters.AddWithValue("@comname", commodity);//查詢商品名稱

                    writeCommand.Parameters.AddWithValue("@strikeprice", dt.Rows[row]["stkprcdec"]);
                    writeCommand.Parameters.AddWithValue("@cptype", dt.Rows[row]["callput"]);
                    writeCommand.Parameters.AddWithValue("@bstype", dt.Rows[row]["ps"]);
                    writeCommand.Parameters.AddWithValue("@opentype", dt.Rows[row]["newps"]);
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
                    writeCommand.Parameters.AddWithValue("@idate", dt.Rows[row]["orddt"]);
                    writeCommand.Parameters.AddWithValue("@itime", dt.Rows[row]["ordtm"]);
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

                    writeCommand.Parameters.AddWithValue("@rtime", dt.Rows[row]["rectime"]);
                    writeCommand.Parameters.AddWithValue("@tfetime", dt.Rows[row]["rectime"]);
                    writeCommand.Parameters.AddWithValue("@cano", "");
                    writeCommand.Parameters.AddWithValue("@plaintext", "");
                    writeCommand.Parameters.AddWithValue("@ciphertext", ""); 
                    writeCommand.Parameters.AddWithValue("@modifytime", DateTime.Now.ToString("yyyyMMdd"));
                    

                    foreach (SqlParameter parameter in writeCommand.Parameters)
                    {
                        strSQL = strSQL.Replace(parameter.ParameterName.ToString(), parameter.Value.ToString());
                    }
                    writeCommand.ExecuteNonQuery();

                    success++;
                    _success++;
                    _logMessage.AppendLine($"{DateTime.Now} 寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");
                    _logMessage.AppendLine($"ordno:{dt.Rows[row]["ordno"]} company:{dt.Rows[row]["company"]} actno:{dt.Rows[row]["actno"]} orddt:{dt.Rows[row]["orddt"]} legsno:{dt.Rows[row]["legsno"]} omcnt:{dt.Rows[row]["omcnt"]} itype:{itype}");
                    //Console.WriteLine($"{DateTime.Now} 寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");

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
    }
}
