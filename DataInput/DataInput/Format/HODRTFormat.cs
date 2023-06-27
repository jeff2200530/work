using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataInput.Format
{
    public class HODRTFormat 
    {
        public string TDATE = string.Empty;
        public string TSEQ = string.Empty;
        public string BHNO = string.Empty;
        public string CSEQ = string.Empty;
        public string DSEQ = string.Empty;
        public string ORIGN = string.Empty;
        public string IDNO = string.Empty;
        public string OTYPE = string.Empty;
        public string MTYPE = string.Empty;
        public string ETYPE = string.Empty;
        public string BSTYPE = string.Empty;
        public string TTYPE = string.Empty;
        public string STOCK = string.Empty;
        public string QTY = string.Empty;
        public string PRICE = string.Empty;
        public string MARKET = string.Empty;
        public string SALES = string.Empty;
        public string IDATE = string.Empty;
        public string ITIME = string.Empty;
        public string PSTATUS = string.Empty;
        public string NETSEQ = string.Empty;
        public string RSTATUS = string.Empty;
        public string RTIME = string.Empty;
        public string TSETIME = string.Empty;
        public string BFQTY = string.Empty;
        public string AFQTY = string.Empty;
        public string MQTY = string.Empty;
        public string MAMT = string.Empty;
        public string CQTY = string.Empty;
        public string FORCE = string.Empty;
        public string NOVICE = string.Empty;
        public string CPRINT = string.Empty;
        public string IORDER = string.Empty;
        public string TIMEOUT = string.Empty;
        public string SIP = string.Empty;
        public string STELNO = string.Empty;
        public string TIMEFACTOR = string.Empty;
        public string HAMT = string.Empty;
        public string HSTATUS = string.Empty;
        public string HSTIME = string.Empty;
        public string HRTIME = string.Empty;
        public string RAMT = string.Empty;
        public string PCOND = string.Empty;
        public string SINO = string.Empty;
        public string OPERATOR = string.Empty;
        public string ORDKIND = string.Empty;
        public string INSTRUCTION = string.Empty;
        public string NETNO = string.Empty;
        public string GSERIALNO = string.Empty;
        public string GBATCHNO = string.Empty;
        public string CLIENT_INPUT_TIME = string.Empty;
        public string ABDEALER = string.Empty;
        public string OTHERNETNO = string.Empty;
        public string OTHERTSEQ = string.Empty;
        public string OTHERFIXTAG = string.Empty;
        public string CHASE = string.Empty;
        public string ERRTYPE = string.Empty;
        public string ERRCODE = string.Empty;
        public string ERRMSG = string.Empty;
        public string REPLYFROM = string.Empty;
        public string IP = string.Empty;
        public string SIGNDATA = string.Empty;
        public string CREATEDATE = string.Empty;
        public string UPDATEDATE = string.Empty;
        public string STATUSTEXT = string.Empty;
        public string HOSTIP = string.Empty;
        public string PREORDER = string.Empty;
        public string RECMD_BROKER = string.Empty;
        public string SETLMHD = string.Empty;
        public string FIRSTSELL = string.Empty;
        public string ORDKND = string.Empty;
        public string PREPRICE = string.Empty;
        public string EFQTY = string.Empty;
        public string REJECTTAG = string.Empty;


        public override string ToString()
        {
            return TDATE.Trim() + "," + TSEQ.Trim() + "," + BHNO.Trim() + "," + CSEQ.Trim() + "," + DSEQ.Trim() + "," + ORIGN.Trim() + "," + IDNO.Trim() + "," + OTYPE.Trim() + "," +
MTYPE.Trim() + "," +
ETYPE.Trim() + "," +
BSTYPE.Trim() + "," +
TTYPE.Trim() + "," +
STOCK.Trim() + "," +
QTY.Trim() + "," +
PRICE.Trim() + "," +
MARKET.Trim() + "," +
SALES.Trim() + "," +
IDATE.Trim() + "," +
ITIME.Trim() + "," +
PSTATUS.Trim() + "," +
NETSEQ.Trim() + "," +
RSTATUS.Trim() + "," +
RTIME.Trim() + "," +
TSETIME.Trim() + "," +
BFQTY.Trim() + "," +
AFQTY.Trim() + "," +
MQTY.Trim() + "," +
MAMT.Trim() + "," +
CQTY.Trim() + "," +
FORCE.Trim() + "," +
NOVICE.Trim() + "," +
CPRINT.Trim() + "," +
IORDER.Trim() + "," +
TIMEOUT.Trim() + "," +
SIP.Trim() + "," +
STELNO.Trim() + "," +
TIMEFACTOR.Trim() + "," +
HAMT.Trim() + "," +
HSTATUS.Trim() + "," +
HSTIME.Trim() + "," +
HRTIME.Trim() + "," +
RAMT.Trim() + "," +
PCOND.Trim() + "," +
SINO.Trim() + "," +
OPERATOR.Trim() + "," +
ORDKIND.Trim() + "," +
INSTRUCTION.Trim() + "," +
NETNO.Trim() + "," +
GSERIALNO.Trim() + "," +
GBATCHNO.Trim() + "," +
CLIENT_INPUT_TIME.Trim() + "," +
ABDEALER.Trim() + "," +
OTHERNETNO.Trim() + "," +
OTHERTSEQ.Trim() + "," +
OTHERFIXTAG.Trim() + "," +
CHASE.Trim() + "," +
ERRTYPE.Trim() + "," +
ERRCODE.Trim() + "," +
ERRMSG.Trim() + "," +
REPLYFROM.Trim() + "," +
IP.Trim() + "," +
SIGNDATA.Trim() + "," +
CREATEDATE.Trim() + "," +
UPDATEDATE.Trim() + "," +
STATUSTEXT.Trim() + "," +
HOSTIP.Trim() + "," +
PREORDER.Trim() + "," +
RECMD_BROKER.Trim() + "," +
SETLMHD.Trim() + "," +
FIRSTSELL.Trim() + "," +
ORDKND.Trim() + "," +
PREPRICE.Trim() + "," +
EFQTY.Trim() + "," +
REJECTTAG.Trim();


        }
        public string GetPropertiesString()
        {
            StringBuilder column = new StringBuilder();
            Type type = typeof(HODRTFormat);
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (var field in fieldInfos)
            {
                column.Append(field.Name.ToString() + ",");
            }
            column.Remove(column.Length - 1, 1);
            return column.ToString();

        }
    }
}
