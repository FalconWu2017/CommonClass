using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass.Log
{
    /// <summary>
    /// 记录类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 错误。由于一些输入错误生成的日志。比如输入数据错误、类型错误、配置错误等。
        /// </summary>
        Error,
        /// <summary>
        /// 异常。程序运行发生的异常生成的日志。
        /// </summary>
        Exception,
        /// <summary>
        /// 警告，虽然程序可以继续正常运行，但是可能存在一些错误。
        /// </summary>
        Warn,
        /// <summary>
        /// 调试。调试时期产生的记录
        /// </summary>
        Debug,
        /// <summary>
        /// 跟踪。正常程序运行跟踪产生的记录
        /// </summary>
        Trace
    }
}