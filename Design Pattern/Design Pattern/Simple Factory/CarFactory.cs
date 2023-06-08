using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Simple_Factor
{
    /// <summary>
    /// 車的工廠類
    /// </summary>
    public class CarFactory
    {
        public static Car OpCar(string carName)
        {
            Car car = null;
            switch (carName)
            {
                case ("AD"):
                    car = new ADCar();
                    break;
                case ("BC"):
                    car = new BCCar();
                    break;
            }
            return car;
        }
    }
}
