using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using CommonClass.Factory;

namespace CommonClass.BackgroundTask
{
    /// <summary>
    /// 背景任务管理器
    /// </summary>
    public class TaskManager:ITaskManager, IRegisterBaseInterface
    {
        /// <summary>
        /// 任务心跳触发计时器
        /// </summary>
        public static Timer CTimer { get; private set; }

        /// <summary>
        /// 要执行的背景线程
        /// </summary>
        public IEnumerable<ITask> BackTasks { get; set; }

        public double BackgroundTaskHeartbeat { get; set; } = 1000;

        /// <summary>
        /// 创建一个背景任务管理器
        /// </summary>
        public TaskManager() { }

        /// <summary>
        /// 通过提供任务和背景心跳间隔创建背景任务管理器
        /// </summary>
        /// <param name="bt">背景任务枚举</param>
        /// <param name="bh">心跳</param>
        public TaskManager(IEnumerable<ITask> bt) {
            this.BackTasks = bt;
        }

        /// <summary>
        /// 通过提供任务和背景心跳间隔创建背景任务管理器
        /// </summary>
        /// <param name="bt">背景任务枚举</param>
        /// <param name="bh">心跳</param>
        public TaskManager(IEnumerable<ITask> bt,double bh) {
            this.BackTasks = bt; this.BackgroundTaskHeartbeat = bh;
        }

        /// <summary>
        /// 开始执行背景任务
        /// </summary>
        public void RunTasks() {
            if(this.BackTasks == null || this.BackTasks.Count() == 0) return;
            CTimer = CTimer ?? new Timer(this.BackgroundTaskHeartbeat);
            CTimer.Elapsed += this.run;
            CTimer.AutoReset = false;
            CTimer.Start();
        }

        /// <summary>
        /// 任务处理事件
        /// </summary>
        private void run(object sender,ElapsedEventArgs e) {
            //启动具体任务
            foreach(var t in this.BackTasks) {
                Task.Factory.StartNew(m => {
                    if(m is ITask task) {
                        task.Run();
                    }
                },t as object);
            }
            //任务启动后重启心跳计时器
            if(sender is Timer timer) {
                timer.Start();
            }
        }

        public void StopTasks() {
            if(CTimer == null) return;
            CTimer.Stop();
            CTimer.Elapsed -= run;
            CTimer = null;
        }
    }
}
