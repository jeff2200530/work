using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataInput.Format
{
    public class HMTHTFormat
    {
        public string TDATE = string.Empty;
        public string BHNO = string.Empty;
        public string CSEQ = string.Empty;
        public string MSEQ = string.Empty;
        public string DSEQ = string.Empty;
        public string MTYPE = string.Empty;
        public string IDNO = string.Empty;
        public string TTYPE = string.Empty;
        public string ETYPE = string.Empty;
        public string BSTYPE = string.Empty;
        public string STOCK = string.Empty;
        public string QTY = string.Empty;
        public string PRICE = string.Empty;
        public string SALES = string.Empty;
        public string NETSEQ = string.Empty;
        public string ORIGN = string.Empty;
        public string MTIME = string.Empty;
        public string NOVICE = string.Empty;
        public string IORDER = string.Empty;
        public string TSEQ = string.Empty;
        public string OTYPE = string.Empty;
        public string SINO = string.Empty;
        public string ORDKIND = string.Empty;
        public string INSTRUCTION = string.Empty;
        public string INVALIDTIME = string.Empty;
        public string UPDATEDATE = string.Empty;
        public string UPTIME = string.Empty;
        public string UPUSER = string.Empty;
        public string TSENO = string.Empty;
        public string RTIME = string.Empty;
        public string TAX = string.Empty;
        public string FEE = string.Empty;
        public string APAR = string.Empty;
        public override string ToString()
        {
            return TDATE.Trim() + "|" +
BHNO.Trim() + "|" +
CSEQ.Trim() + "|" +
MSEQ.Trim() + "|" +
DSEQ.Trim() + "|" +
MTYPE.Trim() + "|" +
IDNO.Trim() + "|" +
TTYPE.Trim() + "|" +
ETYPE.Trim() + "|" +
BSTYPE.Trim() + "|" +
STOCK.Trim() + "|" +
QTY.Trim() + "|" +
PRICE.Trim() + "|" +
SALES.Trim() + "|" +
NETSEQ.Trim() + "|" +
ORIGN.Trim() + "|" +
MTIME.Trim() + "|" +
NOVICE.Trim() + "|" +
IORDER.Trim() + "|" +
TSEQ.Trim() + "|" +
OTYPE.Trim() + "|" +
SINO.Trim() + "|" +
ORDKIND.Trim() + "|" +
INSTRUCTION.Trim() + "|" +
INVALIDTIME.Trim() + "|" +
UPDATEDATE.Trim() + "|" +
UPTIME.Trim() + "|" +
UPUSER.Trim() + "|" +
TSENO.Trim() + "|" +
RTIME.Trim() + "|" +
TAX.Trim() + "|" +
FEE.Trim() + "|" +
APAR.Trim();

        }
        public string GetPropertiesString()
        {
            StringBuilder column = new StringBuilder();
            Type type = typeof(HMTHTFormat);
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (var field in fieldInfos)
            {
                column.Append(field.Name.ToString() + "|");
            }
            column.Remove(column.Length - 1, 1);
            return column.ToString();

        }
    }
    
}
