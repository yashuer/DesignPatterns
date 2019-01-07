using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetObserver
{
    public class TemperatureCurrentReporter : IObserver<Temperature>
    {
        private IDisposable unsubscriber;
        public string Name { get; set; }
        public TemperatureCurrentReporter(string name)
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
        public void OnCompleted()
        {
            Console.WriteLine(" 不会再发布新数据了，不用再等待了。 ");
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Temperature value)
        {
            Console.WriteLine("{2}:The current temperature is {0}°C at {1:g}", value.Degrees, value.Date,Name);
        }
    }
}
