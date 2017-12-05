using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass.Log
{
    /// <summary>
    /// 默认日志记录满后回调处理
    /// </summary>
    public class DefaultBufFullCallback : IBufFullCallback
    {
        #region IBufFullCallback 成员

        public void OnFullCallback(IEnumerable<LogContext> logs) {
            if(logs == null || logs.Count() == 0) return;
            var first=logs.First();
            foreach(var w in first.LogWriters) {
                w.Write(logs.ToArray());
            }
        }

        #endregion
    }
}