using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern.Observer
{
    public interface ISubject
    {
        void Add(Observer observer);
        void Remove(Observer observer);
        
        string SubjectState { get; set; }

        void Notify();

    }
    public delegate void EvenHandler();

    public class Mishu:ISubject
    {
        public event EvenHandler Update;
        public IList<Observer> observers = new List<Observer>();
        public string SubjectState { get; set; }
        public void Add(Observer observer)
        {
            observers.Add(observer);
        }
        public void Remove(Observer observer)
        {
            observers.Remove(observer);
        }
        public void Notify() {
            Update();
        }

    }
}
