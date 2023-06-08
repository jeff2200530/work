using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Insert
{
    public abstract class InsertBase:MainProcessorBase
    {
        public abstract void SetBaseData();
        public abstract void SetDt();
        public abstract void Insert();
    }
}
