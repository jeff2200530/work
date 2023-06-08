using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    public abstract class Component {
        public abstract void Operation();
    }
    public class Player : Component {

        public override void Operation()
        {
            Console.WriteLine("玩家的裝備");
        }
    }

    public abstract class Decorator : Component {
        protected Component component;
        public void SetPlayer(Component component) {
            this.component = component;
        }
        public override void Operation()
        {
            if (this.component != null)
            {
                this.component.Operation();
            }
        }
    }

    public class Decorator_A : Decorator {

        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("裝備了 保健，攻擊力+10");
        }
    }

    public class Decorator_B : Decorator 
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("裝備了 護頓，防禦力+5");
        }

    }

    public class Decorator_C : Decorator {

        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("裝備了 靴子，速度+2");
        }
    }

}
