
namespace CommonClass.Log
{
    /// <summary>
    /// 将LOG写入设置
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// 将LOG信息写入设备
        /// </summary>
        /// <param name="context">Log信息上下文</param>
        void Write(params LogContext[] context);
    }
}
