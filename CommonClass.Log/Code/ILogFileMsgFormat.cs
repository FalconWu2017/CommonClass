using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonClass.Log
{
    /// <summary>
    /// 文件日志的存储格式
    /// </summary>
    public interface ILogFileMsgFormat
    {
        /// <summary>
        /// 格式化文件日志记录
        /// </summary>
        /// <param name="context">日志记录上下文</param>
        /// <returns>格式化后的字符串</returns>
        string Format(LogContext context);
    }
}
