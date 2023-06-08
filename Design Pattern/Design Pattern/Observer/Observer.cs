using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Observer
{
    public abstract class Observer
    {
        protected ISubject sub;
        protected string name;
        protected Observer(string name, ISubject sub)
        {
            this.name = name;
            this.sub = sub;
        }
    }
    public class StockObserver : Observer
    {
        public StockObserver(string name, ISubject sub) : base(name,sub)
        {

        }
        public void CloseStockMarket()
        {
            Console.WriteLine($"通知內容：{sub.SubjectState},反應：{name}關閉股票行情，繼續工作！");
        }
    }

    public class NBAObserver : Observer {
        public NBAObserver(string name, ISubject sub) : base(name, sub)
        {

        }
        public void CloseNBA()
        {
            Console.WriteLine($"通知內容：{sub.SubjectState},反應：{name}關閉NBA直播，繼續工作！");
        }


    }
}
   
    

