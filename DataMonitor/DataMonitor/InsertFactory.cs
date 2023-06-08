using DataMonitor.Format;
using DataMonitor.Insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor
{
    public static class InsertFactory
    {
        public static InsertBase GetProcess(Request request) {
            switch (request.table)
            {
                case ("hmtht"):
                    return new HMTHT();
                case ("hodrt"):
                    return new HODRT();
                case ("trafuhord"):
                    return new TraFuhord();
                case ("trafuhtrd"):
                    return new TraFuhtrd();
                default:
                    return null;
                   
            }

        }
    }
}
