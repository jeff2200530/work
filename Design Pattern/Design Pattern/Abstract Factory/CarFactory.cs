using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Design_Pattern.Abstract_Factory.Audi;

namespace Design_Pattern.Abstract_Factory
{
    public abstract class AbstractFactory{
        public abstract Audi createAudi();
        public abstract BMW createBMW();
    }

    public class JeepFactory : AbstractFactory {
        public override Audi createAudi() {
            return new AudiJeep();
        }
        public override BMW createBMW() {
            return new BMWJeep();
        }
    }

    public class SUVFactory : AbstractFactory
    {
        public override Audi createAudi()
        {
            return new AudiSUV();
        }
        public override BMW createBMW()
        {
            return new BMWSUV();
        }
    }
}
