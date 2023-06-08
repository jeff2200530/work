using Design_Pattern.Simple_Factor;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Design_Pattern.Abstract_Factory;
using Design_Pattern.Observer;
using Design_Pattern.Singleton;
using Design_Pattern.Prototype;
using Design_Pattern.Strategy;
using System.Collections.Generic;

namespace Design_Pattern
{

    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        public Form1()
        {
            AllocConsole();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Car audi = CarFactory.OpCar("AD");
            audi.Make();
            audi.Sale();

            Car benz = CarFactory.OpCar("BC");
            benz.Make();
            benz.Sale();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbstractFactory factorySUV = new SUVFactory();
            Audi suvAudi = factorySUV.createAudi();
            suvAudi.make();
            suvAudi.sale();

            AbstractFactory factoryJeep = new JeepFactory();
            BMW jeepBMW = factoryJeep.createBMW();
            jeepBMW.make();
            jeepBMW.sale();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mishu mishu = new Mishu();
            StockObserver Jeff = new StockObserver("巴菲特Jeff", mishu);
            NBAObserver Sam = new NBAObserver("霍華德Sam", mishu);
            mishu.Update += Jeff.CloseStockMarket;
            mishu.Update += Sam.CloseNBA;
            mishu.SubjectState = "老闆回來了！";
            mishu.Notify();

            mishu.Remove(Jeff);
            mishu.SubjectState = "老闆回來但是少了sam！";
            mishu.Notify();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SingletonLazy s = SingletonLazy.GetInstnace();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MonkeyKingPrototype prototypeMonkeyKing = new ConcretePrototype("MonkeyKingPrototype");
            MonkeyKingPrototype cloneMonkeyKing = prototypeMonkeyKing.Clone() as ConcretePrototype;
            Console.WriteLine("Cloned1:\t" + cloneMonkeyKing.Id);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Player player = new Player();

            Decorator_A A = new Decorator_A();
            A.SetPlayer(player);

            Decorator_B B = new Decorator_B();
            B.SetPlayer(A);

            Decorator_C  C= new Decorator_C();
            C.SetPlayer(B);

            C.Operation();



        }

        private void button7_Click(object sender, EventArgs e)
        {
            Caculator strategy = new Caculator();
            strategy.setStrategy("add");
            strategy.execute(6, 2);

            strategy.setStrategy("minus");
            strategy.execute(6, 2);

            strategy.setStrategy("multiply");
            strategy.execute(6, 2);

            strategy.setStrategy("divide");
            strategy.execute(6, 2);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            var strs = GetStrings();
            foreach (var str in strs)
                Console.WriteLine(str);
        }
        static IEnumerable<string> GetStrings()
        {
            yield return "Mio";
            yield return "Miffy";
            yield return "Lulu";
            yield break;
            yield return "NekoSan";
        }
    }
}
