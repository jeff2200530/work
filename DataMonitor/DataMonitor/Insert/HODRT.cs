using DataMonitor.Format;
using DataTransfer.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Insert
{
    public class HODRT : InsertBase
    {
        public Dictionary<string, List<DataRow>> _cntDic = new Dictionary<string, List<DataRow>>();
        public Dictionary<string, string> _origsourceDic = new Dictionary<string, string>();
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

                string key = dt.Rows[row]["TDATE"].ToString() + "," + dt.Rows[row]["BHNO"].ToString() + "," + dt.Rows[row]["CSEQ"].ToString() + "," + dt.Rows[row]["DSEQ"].ToString();
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

                if (_reference._secOrignDic.ContainsKey(dt.Rows[row]["ORIGN"].ToString()))
                    dt.Rows[row]["ORIGN"] = _reference._secOrignDic[dt.Rows[row]["ORIGN"].ToString()];

                if (dt.Rows[row]["OTYPE"].ToString() == "I" & !_origsourceDic.ContainsKey(key))
                {
                    _origsourceDic.Add(key, dt.Rows[row]["ORIGN"].ToString());
                }


                string table = "shord";
                string strSQL = $"insert into {table} (tdate,rseq,tseq,bhno,cseq,netseq,dseq,cnt,source,origsource,otype,mtype,etype,bstype,ttype,ordknd,stock,stockname,tqty,efqty,mqty,cqty,uqty,bfqty,afqty,price,preprice,pcond,sales,operator,reserve,idate,itime,ftime,stime,clientip,serverip,replyfrom,rejecttag,statuscode,statustext,rtime,tsetime,cano,plaintext,ciphertext,modifytime)　values (@tdate,@rseq,@tseq,@bhno,@cseq,@netseq,@dseq,@cnt,@source,@origsource,@otype,@mtype,@etype,@bstype,@ttype,@ordknd,@stock,@stockname,@tqty,@efqty,@mqty,@cqty,@uqty,@bfqty,@afqty,@price,@preprice,@pcond,@sales,@operator,@reserve,@idate,@itime,@ftime,@stime,@clientip,@serverip,@replyfrom,@rejecttag,@statuscode,@statustext,@rtime,@tsetime,@cano,@plaintext,@ciphertext,@modifytime)";
                SqlCommand writeCommand = new SqlCommand(strSQL, _db);
                try
                {
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
                    writeCommand.Parameters.AddWithValue("@statuscode", "");
                    writeCommand.Parameters.AddWithValue("@statustext", "");
                    writeCommand.Parameters.AddWithValue("@rtime", dt.Rows[row]["RTIME"]);
                    writeCommand.Parameters.AddWithValue("@tsetime", dt.Rows[row]["TSETIME"]);
                    writeCommand.Parameters.AddWithValue("@cano", "");
                    writeCommand.Parameters.AddWithValue("@plaintext", "");
                    writeCommand.Parameters.AddWithValue("@ciphertext", dt.Rows[row]["SIGNDATA"]);
                    writeCommand.Parameters.AddWithValue("@modifytime", DateTime.Now.ToString("yyyyMMdd"));


                    foreach (SqlParameter parameter in writeCommand.Parameters)
                    {
                        strSQL = strSQL.Replace(parameter.ParameterName.ToString(), parameter.Value.ToString());
                    }

                    writeCommand.ExecuteNonQuery();

                    success++;
                    _success++;
                    _logMessage.AppendLine($"{DateTime.Now} 寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");


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
