using System;
using System.Threading;

namespace CommonClass.BackgroundTask.Tests
{
    class TaskTest:ITask
    {
        public static int Count { get; set; } = 0;

        void ITask.Run() {
            Count += 1;
            Console.WriteLine($"TaskTest Run(Count:{Count}),TheadId:{Thread.CurrentThread.ManagedThreadId}");
        }
    }

}