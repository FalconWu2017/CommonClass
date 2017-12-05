
using CommonClass.Factory;

namespace CommonClass.Log
{
    /// <summary>
    /// 日志管理对外接口。记录如何写入和何时写入设备取决于LogType、LogLever和相关实现
    /// </summary>
    public interface ILog:IRegisterBaseInterface
    {
        /// <summary>
        /// 将日志记录添加到管理器，类型为跟踪，级别一般
        /// </summary>
        /// <param name="msg">要添加的信息</param>
        /// <returns>记录上下文</returns>
        LogContext AddMsg(string msg);

        /// <summary>
        /// 将日志记录添加到管理器，并指定类型，级别根据类型确定
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="msg">要添加的信息</param>
        /// <returns>记录上下文</returns>
        LogContext AddMsg(string msg, LogType type);

        /// <summary>
        /// 将日志记录添加到管理器，并制定类型和级别
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="lever">消息级别</param>
        /// <param name="msg">要添加的信息</param>
        /// <returns>记录上下文</returns>
        LogContext AddMsg(string msg, LogType type, LogLever lever);

        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="context">日志记录上下文</param>
        /// <returns>日志记录上下文</returns>
        LogContext AddMsg(LogContext context);
    }
}
