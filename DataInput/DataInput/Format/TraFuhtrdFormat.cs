using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataInput.Format
{
    public class TraFuhtrdFormat
    {
        public string trdno = string.Empty;
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
        public string abdealer = string.Empty;
        public string ps = string.Empty;
        public string dtrade = string.Empty;
        public string hedge = string.Empty;
        public string spread = string.Empty;
        public string legsno = string.Empty;
        public string updt = string.Empty;
        public string logdt = string.Empty;
        public string code = string.Empty;
        public string status = string.Empty;
        public string branchno = string.Empty;
        public string @operator = string.Empty;
        public string grpname = string.Empty;
        public string gserialno = string.Empty;
        public string gbatchno = string.Empty;
        public string exhtrdsn = string.Empty;
        public string trdqty = string.Empty;
        public string trdprcdec = string.Empty;
        public string trdprcfra = string.Empty;
        public string trddt = string.Empty;
        public string trdtm = string.Empty;
        public string mtype = string.Empty;
        public string newps = string.Empty;
        public string effknd = string.Empty;
        public string ordtype = string.Empty;
        public string srctype = string.Empty;
        public string aeno = string.Empty;
        public string clientseq = string.Empty;
        public string exhordsn = string.Empty;
        public string etype = string.Empty;
        public override string ToString()
        {
            return trdno.Trim() + "," +
seqno.Trim() + "," +
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
abdealer.Trim() + "," +
ps.Trim() + "," +
dtrade.Trim() + "," +
hedge.Trim() + "," +
spread.Trim() + "," +
legsno.Trim() + "," +
updt.Trim() + "," +
logdt.Trim() + "," +
code.Trim() + "," +
status.Trim() + "," +
branchno.Trim() + "," +
@operator.Trim() + "," +
grpname.Trim() + "," +
gserialno.Trim() + "," +
gbatchno.Trim() + "," +
exhtrdsn.Trim() + "," +
trdqty.Trim() + "," +
trdprcdec.Trim() + "," +
trdprcfra.Trim() + "," +
trddt.Trim() + "," +
trdtm.Trim() + "," +
mtype.Trim() + "," +
newps.Trim() + "," +
effknd.Trim() + "," +
ordtype.Trim() + "," +
srctype.Trim() + "," +
aeno.Trim() + "," +
clientseq.Trim() + "," +
exhordsn.Trim() + "," +
etype.Trim();

        }
        public string GetPropertiesString()
        {
            StringBuilder column = new StringBuilder();
            Type type = typeof(TraFuhtrdFormat);
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
