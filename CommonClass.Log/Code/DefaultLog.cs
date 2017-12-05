using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace CommonClass.Log
{
    /// <summary>
    /// 默认日志记录器
    /// </summary>
    public class DefaultLog:ILog
    {

        #region ILog 成员

        public LogContext AddMsg(string msg) {
            return AddMsg(msg, LogType.Trace, LogLever.Common);
        }

        public LogContext AddMsg(string msg, LogType type) {
            switch(type) {
                case LogType.Error: return AddMsg(msg, type, LogLever.Common);
                case LogType.Exception: return AddMsg(msg, type, LogLever.Important);
                case LogType.Warn: return AddMsg(msg, type, LogLever.Common);
                case LogType.Debug: return AddMsg(msg, type, LogLever.Important);
                case LogType.Trace: return AddMsg(msg, type, LogLever.Common);
                default: return AddMsg(msg, type, LogLever.Common);
            }
        }

        public LogContext AddMsg(string msg, LogType type, LogLever lever) {
            LogContext context=new LogContext {
                CreateDatetime = DateTime.Now,
                Lever = lever, Type = type,
                Msg = msg,
            };
            var st = new StackTrace();
            context.LogSource = st.GetFrame(2).GetMethod().DeclaringType.ToString();
            return context.WriteBuffer.AddToBuffer(context);
        }

        public LogContext AddMsg(LogContext context) {
            if(context.Lever == LogLever.Auto) {
                context.Lever =
                    context.Type == LogType.Debug ? LogLever.Important :
                    context.Type == LogType.Error ? LogLever.Common :
                    context.Type == LogType.Warn ? LogLever.Common :
                    context.Type == LogType.Trace ? LogLever.Common :
                    context.Type == LogType.Exception ? LogLever.Important :
                    LogLever.Common;
            }
            return context.WriteBuffer.AddToBuffer(context);
        }
        #endregion
    }
}