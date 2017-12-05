using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass.Log
{
    /// <summary>
    /// 日志记录上下文
    /// </summary>
    public class LogContext
    {
        /// <summary>
        /// Log写入设备
        /// </summary>
        public static IEnumerable<ILogWriter> CurLogWriters { get; set; }
        /// <summary>
        /// 记录缓冲器
        /// </summary>
        public static IWriteBuffer CurWriteBuffer { get; set; }
        /// <summary>
        /// 当前日志记录配置
        /// </summary>
        public static LogConfig CurLogconfig { get; set; }

        /// <summary>
        /// 记录来源
        /// </summary>
        public object LogSource { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public LogType Type { get; set; }
        /// <summary>
        /// 记录级别
        /// </summary>
        public LogLever Lever { get; set; }
        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateDatetime { get; set; }
        /// <summary>
        /// 记录信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 记录配置
        /// </summary>
        public LogConfig Config { get { return CurLogconfig; } }
        /// <summary>
        /// 可用的日志记录器集合
        /// </summary>
        public IEnumerable<ILogWriter> LogWriters { get { return CurLogWriters; } }
        /// <summary>
        /// 当前使用的写入缓冲器
        /// </summary>
        public IWriteBuffer WriteBuffer { get { return CurWriteBuffer; } }
    }
}