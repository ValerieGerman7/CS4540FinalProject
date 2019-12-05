using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CS4540PS2.Models {
    public class Notification {
        private static Notification self;
        private Timer time;
        // Singletone Notification object
        public static Notification Self => self ?? (self = new Notification());

        public Notification() {
            Debug.WriteLine("Notification initialized");
            DateTime now = DateTime.Now;
            DateTime next = DateTime.Now.AddSeconds(1); //Next check is in 1 day
            TimeSpan timeRemaining = next - now;
            if(timeRemaining <= TimeSpan.Zero) {
                timeRemaining = TimeSpan.Zero;
            }
            Action not = delegate() { Notify(); };
            time = new Timer(t => { not.Invoke(); }, null, timeRemaining, TimeSpan.FromSeconds(1));
        }

        private static void Notify() {
            Debug.WriteLine("Notify called: " + DateTime.Now.ToString());
        }
    }

    /// <summary>
    /// Task scheduler class: referenced https://stackoverflow.com/questions/3243348/how-to-call-a-method-daily-at-specific-time-in-c to
    /// create this class.
    /// </summary>
    public class TaskScheduler {
        private static TaskScheduler instance;
        private List<Timer> timers = new List<Timer>();
        private TaskScheduler() { }

        public static TaskScheduler Instance => instance ?? (instance = new TaskScheduler());

        public void ScheduleTask(int hour, int min, double intervalInHour, Action task) {
            DateTime now = DateTime.Now;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hour, min, 0, 0);
            if (now > firstRun) {
                firstRun = firstRun.AddDays(1);
            }

            TimeSpan timeToGo = firstRun - now;
            if (timeToGo <= TimeSpan.Zero) {
                timeToGo = TimeSpan.Zero;
            }

            var timer = new Timer(x =>
            {
                task.Invoke();
            }, null, timeToGo, TimeSpan.FromHours(intervalInHour));

            timers.Add(timer);
        }
    }
}
