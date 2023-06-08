using Microsoft.VisualStudio.TestTools.UnitTesting;
using Design_Pattern.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Strategy.Tests
{
    [TestClass()]
    public class Tests
    {
        [TestMethod()]
        public void addcaculateTest()
        {
            int actual;
            int expect = 17;
            Caculator caculator = new Caculator();
            caculator.setStrategy("add");
            actual = caculator.execute(8, 9);
            Assert.AreEqual(actual, expect);

        }
        [TestMethod()]
        public void minuscaculateTest()
        {
            int actual;
            int expect = -1;
            Caculator caculator = new Caculator();
            caculator.setStrategy("minus");
            actual = caculator.execute(8, 9);
            Assert.AreEqual(actual, expect);
        }
        [TestMethod()]
        public void mutiplycaculateTest()
        {
            int actual;
            int expect = 72;
            Caculator caculator = new Caculator();
            caculator.setStrategy("multiply");
            actual = caculator.execute(8, 9);
            Assert.AreEqual(actual, expect);
           
        }
        [TestMethod()]
        public void dividecaculateTest()
        {
            int actual;
            int expect = 8;
            Caculator caculator = new Caculator();
            caculator.setStrategy("divide");
            actual = caculator.execute(72, 9);
            Assert.AreEqual(actual, expect);
        }
    }
}