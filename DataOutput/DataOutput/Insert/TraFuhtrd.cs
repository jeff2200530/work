using DataOutput.Extension_Methods;
using DataOutput.Format;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOutput.Insert
{
    public class TraFuhtrd : InsertBase
    {
        public Dictionary<string, List<DataRow>> _itypeDic = new Dictionary<string, List<DataRow>>();
        public Dictionary<string, List<DataRow>> _cntDic = new Dictionary<string, List<DataRow>>();
        public override void SetData(DataTable dt)
        {
            #region Log
            logger.Trace($"SetDate開始");
            #endregion
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                #region 設定itype
                string key = dt.Rows[row]["trddt"].ToString() + "," + dt.Rows[row]["ordno"].ToString() + "," + dt.Rows[row]["company"].ToString() + "," + dt.Rows[row]["actno"].ToString();
                if (!_itypeDic.ContainsKey(key))
                {
                    List<DataRow> list = new List<DataRow>();
                    list.Add(dt.Rows[row]);
                    _itypeDic.Add(key, list);
                }
                else
                {
                    _itypeDic[key].Add(dt.Rows[row]);
                }
                #endregion
            }
            int count = dt.Rows.Count;
            int success = 0;
            int fail = 0;

            try
            {
                _db.Open();
                #region Log
                logger.Trace($"sql連線成功");
                #endregion
            }
            catch(Exception ex)
            {
                #region Log
                logger.Error($"sql連線失敗 錯誤訊息:{ex}");
                #endregion
            }

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                #region 設定cnt
                string key = dt.Rows[row]["trddt"].ToString() + "," + dt.Rows[row]["ordno"].ToString() + "," + dt.Rows[row]["company"].ToString() + "," + dt.Rows[row]["actno"].ToString();
                if (!(_cntDic.ContainsKey(key)) == true)
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

                string table = "fhtrd";
                string strSQL = $"insert into {table} (mdate,tdate,mseq,netseq,ordno,bhno,cseq,bstype,exchange,mtype,etype,comtype,legsno,comno,comym,comname,strikeprice,cptype,itype,mprice,mqty,mtime,rtime,modifytime,cnt) values (@mdate,@tdate,@mseq,@netseq,@ordno,@bhno,@cseq,@bstype,@exchange,@mtype,@etype,@comtype,@legsno,@comno,@comym,@comname,@strikeprice,@cptype,@itype,@mprice,@mqty,@mtime,@rtime,@modifytime,@cnt)";
                SqlCommand writeCommand = new SqlCommand(strSQL, _db);
                try
                {
                    #region 設定參數
                    writeCommand.Parameters.Clear();
                    writeCommand.Parameters.AddWithValue("@mdate", dt.Rows[row]["trddt"]);
                    writeCommand.Parameters.AddWithValue("@tdate", dt.Rows[row]["trddt"]);
                    writeCommand.Parameters.AddWithValue("@mseq", dt.Rows[row]["exhtrdsn"]);
                    writeCommand.Parameters.AddWithValue("@netseq", dt.Rows[row]["seqno"]);
                    writeCommand.Parameters.AddWithValue("@ordno", dt.Rows[row]["ordno"]);
                    writeCommand.Parameters.AddWithValue("@bhno", dt.Rows[row]["company"]);
                    writeCommand.Parameters.AddWithValue("@cseq", dt.Rows[row]["actno"]);
                    writeCommand.Parameters.AddWithValue("@bstype", dt.Rows[row]["ps"]);
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

                    string itype = "0";
                    string legsno = "1";
                    if (_itypeDic.ContainsKey(key))
                    {
                        foreach (DataRow r1 in _itypeDic[key])
                        {
                            string start1 = dt.Rows[row][30].ToString().Substring(0, 2);
                            string start2 = new string(r1[30].ToString().Substring(0, 2).ToCharArray().Reverse<char>().ToArray<char>());
                            string e1 = dt.Rows[row][30].ToString().Substring((dt.Rows[row][30].ToString().Length - 5));
                            string e2 = r1[30].ToString().Substring(r1[30].ToString().Length - 5);
                            //判斷相同key值的mseq 前兩碼互為相反 ex. ab,ba  後五碼相同 ex. 00001,00001    ex. ab0001,ba0001
                            //皆相同itype=1(複式單)
                            if (start1 == start2 & e1 == e2)
                            {
                                itype = "1";
                                //判斷相同key值得複式單 如果另一個複式單legsno為1 則此複式單的legsno改為2  
                                if (r1[20].ToString() != "2")
                                    legsno = "2";
                            }
                            //把字典中的這筆legsno修改為2
                            if (r1 == dt.Rows[row])
                            {
                                r1[20] = "2";
                            }
                        }
                    }
                    writeCommand.Parameters.AddWithValue("@itype", itype);
                    writeCommand.Parameters.AddWithValue("@legsno", legsno);

                    writeCommand.Parameters.AddWithValue("@mprice", dt.Rows[row]["trdprcdec"]);
                    writeCommand.Parameters.AddWithValue("@mqty", dt.Rows[row]["trdqty"]);
                    writeCommand.Parameters.AddWithValue("@mtime", dt.Rows[row]["trdtm"]);
                    writeCommand.Parameters.AddWithValue("@rtime", dt.Rows[row]["logdt"]);
                    writeCommand.Parameters.AddWithValue("@modifytime", DateTime.Now.ToString("yyyyMMdd"));
                    writeCommand.Parameters.AddWithValue("@cnt", _cntDic[key].Count);
                    #endregion

                    #region 重組sql語法
                    foreach (SqlParameter parameter in writeCommand.Parameters)
                    {
                        strSQL = strSQL.ReplaceOne(parameter.ParameterName.ToString(), $"'{parameter.Value.ToString()}'");
                    }
                    #endregion
                    writeCommand.ExecuteNonQuery();
                    //Console.WriteLine($"trddt:{dt.Rows[row]["trddt"]} company:{dt.Rows[row]["company"]} actno:{dt.Rows[row]["actno"]} ordno:{dt.Rows[row]["ordno"]} exhtrdsn:{dt.Rows[row]["exhtrdsn"]}  itype:{itype} legsno:{legsno}");                    success++;
                    _success++;

                    #region Log
                    logger.Trace($"寫入第{row + 1}筆成功，成功{success}筆，SQL語法{strSQL}");
                    #endregion
                }

                catch (Exception ex)
                {
                    //Console.WriteLine(ex);
                    fail++;
                    _fail++;
                    #region Log
                    logger.Error($"寫入第{row + 1}筆失敗，失敗{fail}筆，SQL語法{strSQL} 錯誤訊息:{ex}");
                    #endregion
                    continue;
                }
            }
             _count += count;
            _db.Close();
            #region Log
            logger.Trace($"SetDate結束，成功{success}筆，失敗{fail}筆");
            #endregion
        }
    }
}
