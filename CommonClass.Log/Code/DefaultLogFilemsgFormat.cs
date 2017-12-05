using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CommonClass.Log
{
    /// <summary>
    /// 默认的日志文件格式实现
    /// </summary>
    public class DefaultLogFilemsgFormat : ILogFileMsgFormat
    {
        #region ILogFileMsgFormat 成员

        public string Format(LogContext context) {
            StringBuilder sb=new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("调用堆栈：");
            sb.AppendLine(context.LogSource.ToString());
            sb.AppendFormat("生成时间：{0}，记录时间：{1}，消息类型：{2}，消息级别：{3}{4}",
                context.CreateDatetime.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                context.Type.ToString(),
                context.Lever.ToString(),
                "\r\n");
            sb.AppendLine("消息内容：");
            sb.AppendLine(context.Msg);
            sb.AppendLine("----------------------------------------");
            return sb.ToString();
        }

        #endregion
    }
}