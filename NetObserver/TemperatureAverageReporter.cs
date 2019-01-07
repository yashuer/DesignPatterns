using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetObserver
{
    public class TemperatureAverageReporter : IObserver<Temperature>
    {
        private IDisposable unsubscriber;
        private List<Temperature> history;
        public string Name { get; set; }
        public TemperatureAverageReporter(string name)
        {
            Name = name;
            history = new List<Temperature>();
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
            decimal average = 0;
            history.Add(value);
            average = history.Sum(p => p.Degrees) / history.Count;
            Console.WriteLine("{3}:The Average Temperature: {0}° between {1} and {2}", average, history[0].Date, value.Date,Name);
        }
    }
}
