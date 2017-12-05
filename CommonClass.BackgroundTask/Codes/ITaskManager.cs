namespace CommonClass.BackgroundTask
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// 心跳间隔。毫秒
        /// </summary>
        double BackgroundTaskHeartbeat { get; set; }

        /// <summary>
        /// 执行任务
        /// </summary>
        void RunTasks();
        /// <summary>
        /// 结束任务
        /// </summary>
        void StopTasks();
    }
}