using System;
using System.Collections.Generic;

namespace CommonClass.Log
{
    /// <summary>
    /// 记录写入缓冲区
    /// </summary>
    public interface IWriteBuffer
    {
        /// <summary>
        /// 将记录添加到缓冲区
        /// </summary>
        /// <param name="context">记录上下文</param>
        /// <returns>添加的记录上下文</returns>
        LogContext AddToBuffer(LogContext context);
    }
}