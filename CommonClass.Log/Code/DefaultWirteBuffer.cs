using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace CommonClass.Log
{
    /// <summary>
    /// 默认记录写入缓冲器
    /// </summary>
    public class DefaultWirteBuffer : IWriteBuffer
    {
        /// <summary>
        /// 存储日志的缓冲区
        /// </summary>
        public Queue<LogContext> Buffer { get; set; }

        /// <summary>
        /// 共享锁
        /// </summary>
        private static object ObjLock =new object();

        public IEnumerable<IBufFullCallback> Callback { get; set; }

        //构造
        public DefaultWirteBuffer(IEnumerable<IBufFullCallback> bfc) {
            this.Buffer = new Queue<LogContext>();
            this.Callback = bfc;
        }

        #region IWriteBuffer 成员

        public LogContext AddToBuffer(LogContext context) {
            lock(ObjLock) {
                this.Buffer.Enqueue(context);
            }
            if(context.Lever == LogLever.Important || Buffer.Count >= context.Config.BufferSize) {
                OnBuffFull();
            }
            return context;
        }

        #endregion

        public void OnBuffFull() {
            List<LogContext> r=new List<LogContext>();
            //从缓冲区取出数据
            lock(ObjLock) {
                for(int i = 0; i < Buffer.Count; i++) {
                    r.Add(Buffer.Dequeue());
                }
            }
            //调用事件处理
            foreach(var cb in this.Callback) {
                cb.OnFullCallback(r);
            }
        }

        #region IWriteBuffer 成员



        #endregion
    }
}