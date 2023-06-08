using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Simple_Factor
{
    /// <summary>
    /// 車 抽象類(Product)
    /// </summary>
    public abstract class Car
    {
        /// <summary>
        /// 製造車--抽象方法
        /// </summary>
        public abstract void Make();

        /// <summary>
        /// 賣車--抽象方法
        /// </summary>
        public abstract void Sale();
    }

    /// <summary>
    /// 奧迪車
    /// </summary>
    public class ADCar : Car
    {
        public override void Make()
        {
            Console.WriteLine("製造了一輛奧迪車");
        }

        public override void Sale()
        {
            Console.WriteLine("銷售了一輛奧迪車");
        }

    }

    /// <summary>
    /// 賓士車
    /// </summary>
    public class BCCar : Car {
        public override void Make()
        {
            Console.WriteLine("製造了一輛賓士車");
        }

        public override void Sale()
        {
            Console.WriteLine("銷售了一輛賓士車");
        }
    }

}
