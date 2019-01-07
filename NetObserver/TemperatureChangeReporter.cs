using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetObserver
{
    public class TemperatureChangeReporter : IObserver<Temperature>
    {
        private IDisposable unsubscriber;
        private bool first = true;
        private Temperature last;
        public string Name { get; set; }
        public TemperatureChangeReporter(string name)
        {
            Name = name;
        }
        public virtual void Subscribe(IObservable<Temperature> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine(" 不会再发布新数据了，不用再等待了。");
        }

        public virtual void OnError(Exception error)
        {
            // Do nothing.
        }

        public virtual void OnNext(Temperature value)
        {            
            if (first)
            {
                last = value;
                first = false;
            }
            else
            {
                Console.WriteLine("{2}:Change: {0}° in {1:g}", value.Degrees - last.Degrees, value.Date.ToUniversalTime() - last.Date.ToUniversalTime(),Name);
            }
        }
    }
}
