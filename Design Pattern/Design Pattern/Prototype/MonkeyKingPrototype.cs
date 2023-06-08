using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Prototype
{
    /// <summary>
    /// 孫悟空原型
    /// </summary>
    public abstract class MonkeyKingPrototype {
        public string Id { get; set; }
        public MonkeyKingPrototype(string id) {
            this.Id = id;
        }
        //clone方法，即孫悟空說"變"
        public abstract MonkeyKingPrototype Clone();
    }

    /// <summary>
    /// 創建具體原型
    /// </summary>
    public class ConcretePrototype : MonkeyKingPrototype
    {
        public ConcretePrototype(string id) : base(id) { }

        /// <summary>
        /// 淺複製
        /// </summary>
        /// <returns></returns>
        public override MonkeyKingPrototype Clone()
        {
            return (MonkeyKingPrototype)this.MemberwiseClone();
        }
    }
}
