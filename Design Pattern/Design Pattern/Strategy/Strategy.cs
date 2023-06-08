using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Strategy
{
    interface IStrategy
    {
        int caculate(int a, int b);
    }

    public class add : IStrategy {
        public int caculate(int a, int b)
        {
            Console.WriteLine($"加法!數字A={a},數字B={b} 結果為{a + b}");
            return a + b;
        }
    }

    public class minus : IStrategy {
        public int caculate(int a, int b)
        {
            Console.WriteLine($"減法!數字A={a},數字B={b} 結果為{a - b}");
            return a - b;
        }
    }

    public class multiply : IStrategy {
        public int caculate(int a, int b)
        {
            Console.WriteLine($"乘法!數字A={a},數字B={b} 結果為{a * b}");
            return a * b;
        }
    }

    public class divide : IStrategy {
        public int caculate(int a, int b)
        {
            Console.WriteLine($"除法!數字A={a},數字B={b} 結果為{a / b}");
            return a / b;
        }
    }

    public class Caculator {
        private int now = 0;

        private IStrategy strategy;

        public int execute(int a, int b) {
            return strategy.caculate(a,b);
        }
        public void reset() {
            this.now = 0;
        }

        public  void setStrategy(string s) {
            switch (s)
            {
                case ("add"):
                    strategy = new add();
                    break;
                case ("minus"):
                    strategy = new minus();
                    break;
                case ("multiply"):
                    strategy = new multiply();
                    break;
                case ("divide"):
                    strategy = new divide();
                    break;

            }
        }
    
    }
}
