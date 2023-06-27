using DataInput.Format;
using DataInput.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInput
{
    public static class QueryFactory
    {
        public static QueryBase GetProcess(FormFormat input)
        {

            switch (input.processName)
            {
                case "hodrt":
                    return new HODRT(input);
                case "hmtht":
                    return new HMTHT(input);
                case "trafuhtrd":
                    return new TraFuhtrd(input);
                case "trafuhord":
                    return new TraFuhord(input);
                default:
                    throw new Exception("missing matching ProcessName");
            }


        }


    }
}
