using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Format
{
    public class TraFuhordFormat
    {
        public string seqno = string.Empty;
        public string ordno = string.Empty;
        public string company = string.Empty;
        public string actno = string.Empty;
        public string exhno = string.Empty;
        public string comno = string.Empty;
        public string comtype = string.Empty;
        public string comym = string.Empty;
        public string callput = string.Empty;
        public string stkprcdec = string.Empty;
        public string stkprcfra = string.Empty;
        public string denominator = string.Empty;
        public string currency = string.Empty;
        public string country = string.Empty;
        public string mtype = string.Empty;
        public string abdealer = string.Empty;
        public string solution = string.Empty;
        public string ordcmd = string.Empty;
        public string ordtype = string.Empty;
        public string effknd = string.Empty;
        public string ps = string.Empty;
        public string offset = string.Empty;
        public string newps = string.Empty;
        public string dtrade = string.Empty;
        public string hedge = string.Empty;
        public string spread = string.Empty;
        public string legsno = string.Empty;
        public string ordqty = string.Empty;
        public string ordprcdec = string.Empty;
        public string ordprcfra = string.Empty;
        public string triprcdec = string.Empty;
        public string triprcfra = string.Empty;
        public string preprcdec = string.Empty;
        public string preprcfra = string.Empty;
        public string orddt = string.Empty;
        public string ordtm = string.Empty;
        public string updt = string.Empty;
        public string logdt = string.Empty;
        public string code = string.Empty;
        public string status = string.Empty;
        public string srctype = string.Empty;
        public string omcnt = string.Empty;
        public string servercnt = string.Empty;
        public string clientseq = string.Empty;
        public string branchno = string.Empty;
        public string @operator = string.Empty;
        public string roleid = string.Empty;
        public string grpname = string.Empty;
        public string gserialno = string.Empty;
        public string gbatchno = string.Empty;
        public string exhordsn = string.Empty;
        public string expiredt = string.Empty;
        public string rectime = string.Empty;
        public string closetrd = string.Empty;
        public string remqty = string.Empty;
        public string dealqty = string.Empty;
        public string cxlqty = string.Empty;
        public string addqty = string.Empty;
        public string aeno = string.Empty;
        public string errormsg = string.Empty;
        public string lossmargin = string.Empty;
        public string hostname = string.Empty;
        public string hostip = string.Empty;
        public string ip = string.Empty;
        public string etype = string.Empty;
        public override string ToString()
        {
            return seqno.Trim() + "," +
ordno.Trim() + "," +
company.Trim() + "," +
actno.Trim() + "," +
exhno.Trim() + "," +
comno.Trim() + "," +
comtype.Trim() + "," +
comym.Trim() + "," +
callput.Trim() + "," +
stkprcdec.Trim() + "," +
stkprcfra.Trim() + "," +
denominator.Trim() + "," +
currency.Trim() + "," +
country.Trim() + "," +
mtype.Trim() + "," +
abdealer.Trim() + "," +
solution.Trim() + "," +
ordcmd.Trim() + "," +
ordtype.Trim() + "," +
effknd.Trim() + "," +
ps.Trim() + "," +
offset.Trim() + "," +
newps.Trim() + "," +
dtrade.Trim() + "," +
hedge.Trim() + "," +
spread.Trim() + "," +
legsno.Trim() + "," +
ordqty.Trim() + "," +
ordprcdec.Trim() + "," +
ordprcfra.Trim() + "," +
triprcdec.Trim() + "," +
triprcfra.Trim() + "," +
preprcdec.Trim() + "," +
preprcfra.Trim() + "," +
orddt.Trim() + "," +
ordtm.Trim() + "," +
updt.Trim() + "," +
logdt.Trim() + "," +
code.Trim() + "," +
status.Trim() + "," +
srctype.Trim() + "," +
omcnt.Trim() + "," +
servercnt.Trim() + "," +
clientseq.Trim() + "," +
branchno.Trim() + "," +
@operator.Trim() + "," +
roleid.Trim() + "," +
grpname.Trim() + "," +
gserialno.Trim() + "," +
gbatchno.Trim() + "," +
exhordsn.Trim() + "," +
expiredt.Trim() + "," +
rectime.Trim() + "," +
closetrd.Trim() + "," +
remqty.Trim() + "," +
dealqty.Trim() + "," +
cxlqty.Trim() + "," +
addqty.Trim() + "," +
aeno.Trim() + "," +
errormsg.Trim() + "," +
lossmargin.Trim() + "," +
hostname.Trim() + "," +
hostip.Trim() + "," +
ip.Trim() + "," +
etype.Trim();
        }
        public string GetPropertiesString()
        {
            StringBuilder column = new StringBuilder();
            Type type = typeof(TraFuhordFormat);
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
