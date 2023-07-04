using DataInput.Format;
using DataInput.Insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInput.Factory
{
    public static class InsertFactory
    {
        public static InsertBase GetProcess(FormFormat input)
        {

            switch (input.processName)
            {
                case ("hmtht"):
                    return null;
                default:
                    throw new Exception("missing matching ProcessName");
            }
        }
    }
}
