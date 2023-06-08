using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Abstract_Factory
{
    public abstract class Audi
    {
        private String brand;
        private String type;

        public Audi()
        {
            this.brand = "Audi";
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
        public class AudiSUV : Audi {
            public AudiSUV():base() {
                setType("SUV");
            }
            public override void make()
            {
                Console.WriteLine("製造了一輛奧迪SUV車");
            }
            public override void sale()
            {
                Console.WriteLine("銷售了一輛奧迪SUV車");
            }
        }
        public class AudiJeep : Audi {
            public AudiJeep() : base() {
                setType("Jeep");
            }
            public override void make()
            {
                Console.WriteLine("製造了一輛奧迪Jeep車");
            }
            public override void sale()
            {
                Console.WriteLine("銷售了一輛奧迪Jeep車");
            }
        }
    }
}
