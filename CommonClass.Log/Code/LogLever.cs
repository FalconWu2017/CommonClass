
namespace CommonClass.Log
{
    /// <summary>
    /// 记录级别。主要影响写入设备的策略
    /// </summary>
    public enum LogLever
    {
        /// <summary>
        /// 重要记录
        /// </summary>
        Important,
        /// <summary>
        /// 一般记录
        /// </summary>
        Common,
        /// <summary>
        /// 根据消息类型自动确定
        /// </summary>
        Auto
    }
}