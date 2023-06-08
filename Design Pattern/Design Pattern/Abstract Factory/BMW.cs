using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Abstract_Factory
{
    public abstract class BMW
    {
        private String brand;
        private String type;

        public BMW()
        {
            this.brand = "BMW";
        }
        public void setType(String type)
        {
            this.type = type;
        }
        #region get
        public String getBrand()
        {
            return brand;
        }
        public String getType()
        {
            return type;
        }
        #endregion

        public abstract void make();
        public abstract void sale();
    }

    public class BMWSUV : BMW {
        public BMWSUV() : base() {
            setType("SUV");
        }
        public override void make()
        {
            Console.WriteLine("製造了一輛賓士SUV車");
        }
        public override void sale()
        {
            Console.WriteLine("銷售了一輛賓士SUV車");
        }
    }
    public class BMWJeep : BMW {
        public BMWJeep():base(){
            setType("Jeep");
        }
        public override void make()
        {
            Console.WriteLine("製造了一輛賓士Jeep車");
        }
        public override void sale()
        {
            Console.WriteLine("銷售了一輛賓士Jeep車");
        }
    }
}
