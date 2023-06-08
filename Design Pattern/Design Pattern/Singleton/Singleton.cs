using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Singleton
{
    public class SingletonEager
    {
        private static readonly SingletonEager instance = new SingletonEager();
        private SingletonEager() { }
        public static SingletonEager GetInstance()
        {
            return instance;
        }
    }

    public class SingletonLazy {
        private static readonly object thisLock = new object();
        private static SingletonLazy instance;
        private SingletonLazy(){}
        public static SingletonLazy GetInstnace() {
            if (null == instance)
            {
                lock (thisLock)
                {
                    if (null == instance)
                    {
                        instance = new SingletonLazy();
                    }                
                }
            
            }
            return instance;
        }
    }

}
