using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonClass.Log
{
    /// <summary>
    /// 缓冲区满时回调
    /// </summary>
    public interface IBufFullCallback
    {
        /// <summary>
        /// 当缓冲区满或重要消息触发回调
        /// </summary>
        /// <param name="logs"></param>
        void OnFullCallback(IEnumerable<LogContext> logs);
    }
}
