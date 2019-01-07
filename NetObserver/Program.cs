using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetObserver
{
    public class Program
    {
        static void Main(string[] args)
        {
            TemperatureMonitor monitor = new TemperatureMonitor();
            TemperatureCurrentReporter current = new TemperatureCurrentReporter("Current");
            current.Subscribe(monitor);//初始化一个当前温度 订阅者
            TemperatureChangeReporter change = new TemperatureChangeReporter("Change");
            change.Subscribe(monitor);//初始化一个温度改变值 订阅者
            TemperatureAverageReporter average = new TemperatureAverageReporter("Average");
            Thread t = new Thread(new ThreadStart(()=> { average.Subscribe(monitor); current.Unsubscribe(); }));
            
            //模拟从温度检测设备收集到数据
            Nullable<Decimal>[] temps = { 14.6m, 14.65m, 14.7m, 14.9m, 14.9m, 15.2m, 15.25m, 15.2m, 15.4m, 15.45m, null };
            foreach (var temp in temps)
            {
                Thread.Sleep(1500);
                if (temp.HasValue)
                {
                    Temperature tempData = new Temperature(temp.Value, DateTime.Now);
                    monitor.UpdateTemperture(tempData);
                }
                else
                {
                    monitor.EndTransmission();
                    break;
                }
                if (t.ThreadState == ThreadState.Unstarted)
                {
                    t.Start();
                }                
            }
            Console.ReadLine();
        }
    }
}
