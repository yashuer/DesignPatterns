using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetObserver
{
    public class TemperatureMonitor : IObservable<Temperature>
    {
        List<IObserver<Temperature>> observers;
        public TemperatureMonitor()
        {
            observers = new List<IObserver<Temperature>>();
        }
        public IDisposable Subscribe(IObserver<Temperature> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new UnSubscriber(observers, observer);
        }
        public class UnSubscriber : IDisposable
        {
            List<IObserver<Temperature>> _observers;
            IObserver<Temperature> _observer;
            public UnSubscriber(List<IObserver<Temperature>> observers, IObserver<Temperature> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            public void Dispose()
            {
                _observers.Remove(_observer);
            }
        }
        public void UpdateTemperture(Temperature? tempData)
        {
            foreach (var observer in observers)
            {
                if (tempData.HasValue)
                {
                    observer.OnNext(tempData.Value);
                }
                else
                {
                    observer.OnError(new Exception("没有数据！"));
                }
            }
        }
        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }
    public struct Temperature
    {
        private decimal temp;
        private DateTime tempDate;

        public Temperature(decimal temperature, DateTime dateAndTime)
        {
            this.temp = temperature;
            this.tempDate = dateAndTime;
        }

        public decimal Degrees
        { get { return this.temp; } }

        public DateTime Date
        { get { return this.tempDate; } }
    }
}
