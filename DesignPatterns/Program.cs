using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Publiser pub = new Publiser();
            Subscriber A = new Subscriber("A");
            Subscriber B = new Subscriber("B");
            Subscriber C = new Subscriber("C");
            List<IObserver> subs = new List<IObserver>() { A, B, C };
            pub.Attach(subs);
            pub.Notify();
            Console.ReadLine();
        }
    }
}
