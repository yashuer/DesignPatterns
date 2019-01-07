using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// 1.问题：定义对象间一对多的依赖关系 ,当一个对象的状态发生改变时, 所有依赖于它的对象 都需要得到通知并被自动更新。
    /// 2.别名：发布-订阅( Publish - Subscribe ) 
    /// 3.动机：避免依赖对象之间相互更新对方的状态而导致的高耦合（低重用）
    /// 
    /// </summary>
    public interface IObserver
    {
        void Update();
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public class Subscriber : IObserver
    {
        public String Name { get; set; }
        public Subscriber(string name)
        {
            this.Name = name;
        }
        public void Update()
        {
            Console.WriteLine("{0}，您订阅的主题有更新了",Name);
            //TODO:Send email、message to the user
        }
    }
    public class Publiser : ISubject
    {
        private List<IObserver> subscibers = new List<IObserver>();//观察者

        public void Attach(IObserver subscriber)
        {
            subscibers.Add(subscriber);
        }
        public void Attach(List<IObserver> subscribers)
        {
            foreach (var item in subscribers)
            {
                Attach(item);
            }
        }
        public void Detach(IObserver subscriber)
        {
            subscibers.Remove(subscriber);
        }

        public void Notify()
        {
            foreach (var observer in subscibers)
            {
                observer.Update();
            }
        }
    }
}
