using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlarmClockDemo
{

    //1,定义事件委托，约束事件处理方法的签名
    //Tick事件委托：参数为闹钟实例，当前时间
    public delegate void TickEventHandler(object sender, DateTime currentTime);
    //Alarm事件委托
    public delegate void AlarmEventHandler(object sender, DateTime alarmTime);

    //2.闹钟核心类
    public class AlarmClock
    {

        //私有字段：闹钟设定时间、是否运行标识
        private DateTime _alarmTime;
        private bool _isRunning;

        //3.声明事件
        public event TickEventHandler Tick;
        public event AlarmEventHandler Alarm;

        //公共属性：设置/获取闹钟时间
        public DateTime AlarmTime
        {
            get => _alarmTime;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("闹钟时间不能早于当前时间!");

                }
                _alarmTime = value;
            }
        }

        //4.启动闹钟
        public void Start()
        {
            if (_isRunning)
            {
                Console.WriteLine("闹钟已在运行中！");
                return;

            }
            _isRunning = true;
            Console.WriteLine($"闹钟已启动，当前时间：{DateTime.Now:HH:mm:ss},闹钟设定时间：{_alarmTime:HH:mm:ss}");

            //循环计时，每秒触发一次Tick事件
            while(_isRunning)
            {
                //获取当前时间（精确到秒）
                DateTime currentTime=DateTime.Now;
                currentTime = new DateTime(currentTime.Year,currentTime.Month,currentTime.Day,
                    currentTime.Hour,currentTime.Minute,currentTime.Second);
                //触发Tick事件
                Tick?.Invoke(this, currentTime);

                // 检查是否到达闹钟时间（精确到秒）
                if (currentTime >= _alarmTime && currentTime <= _alarmTime.AddSeconds(1))
                {
                    // 触发Alarm事件
                    Alarm?.Invoke(this, _alarmTime);
                    Console.WriteLine("🔔 闹钟响铃后将自动停止！");
                    Stop(); // 响铃后停止闹钟
                    break;
                }

                // 暂停1秒（模拟秒针走时）
                Thread.Sleep(1000);
            }
        }


        // 停止闹钟
        public void Stop()
        {
            _isRunning = false;
            Console.WriteLine("⏹️ 闹钟已停止！");
        }
    }


    // 主程序
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 实例化闹钟
                AlarmClock clock = new AlarmClock();

                // 5. 订阅Tick事件（处理滴答提示）
                clock.Tick += (sender, currentTime) =>
                {
                    Console.WriteLine($"⏱️  滴答... 当前时间：{currentTime:HH:mm:ss}");
                };

                // 6. 订阅Alarm事件（处理响铃提示）
                clock.Alarm += (sender, alarmTime) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red; // 红色高亮提示
                    Console.WriteLine($"🎉 闹钟响铃啦！设定时间：{alarmTime:HH:mm:ss}");
                    Console.ResetColor(); // 恢复默认颜色
                };

                // 设置闹钟时间（当前时间+10秒，方便测试）
                clock.AlarmTime = DateTime.Now.AddSeconds(10);

                // 启动闹钟
                clock.Start();

                // 等待用户输入后退出
                Console.WriteLine("\n按任意键退出...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 出错了：{ex.Message}");
            }
        }
    }

}
