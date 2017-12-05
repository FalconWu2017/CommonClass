using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass.Log
{
    /// <summary>
    /// 默认文件名格式。LogTypeYYYYMMdd
    /// </summary>
    public class DefaultLogFileNameMaker : ILogFileNameMaker
    {
        #region ILogFileNameMaker 成员

        public string MakeFileName(LogContext context) {
            string pathEX=
                context.Type == LogType.Debug ? "dbg" :
                context.Type == LogType.Error ? "Error" :
                context.Type == LogType.Exception ? "Exception" :
                context.Type == LogType.Trace ? "Trace" :
                context.Type == LogType.Warn ? "Warn" : "";
            string  path = pathEX + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            return path;
        }

        #endregion
    }
}