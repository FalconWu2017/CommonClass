using System;
using System.Collections.Generic;
using Autofac;

namespace CommonClass.Log
{
    /// <summary>
    /// 日志管理器工厂类
    /// </summary>
    public static class LogFactory
    {
        private static ILog _log=null;

        /// <summary>
        /// LOG日志记录器
        /// </summary>
        public static ILog Log {
            get
            {
                if (_log == null)
                {
                    ContainerBuilder cb = new ContainerBuilder();
                    //注册文件名生成器
                    cb.Register(c => new DefaultLogFileNameMaker()).As<ILogFileNameMaker>();
                    cb.Register(c => new DefaultAllInOneFileNameMaker()).As<ILogFileNameMaker>();
                    //注册文件格式生成器
                    cb.Register(c => new DefaultLogFilemsgFormat()).As<ILogFileMsgFormat>();
                    //注册文件日志设备
                    cb.Register(c => new DefaultFileLogWriter(c.Resolve<IEnumerable<ILogFileNameMaker>>(), c.Resolve<ILogFileMsgFormat>())).As<ILogWriter>();
                    //注册缓冲满后回调
                    cb.Register(c => new DefaultBufFullCallback()).As<IBufFullCallback>();
                    //注册写入缓存
                    cb.Register(c => new DefaultWirteBuffer(c.Resolve<IEnumerable<IBufFullCallback>>())).As<IWriteBuffer>();
                    //注册日志管理
                    cb.Register(c => new DefaultLog()).As<ILog>();
                    using (var f = cb.Build())
                    {
                        //生成配置
                        LogContext.CurLogconfig = LogConfig.Default;
                        LogContext.CurLogconfig.BufferSize = 0;
                        LogContext.CurLogconfig.FileConfig = FileLogConfig.Default;
                        LogContext.CurLogconfig.FileConfig.BasePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + @"Logs\";
                        LogContext.CurLogWriters = f.Resolve<IEnumerable<ILogWriter>>();
                        LogContext.CurWriteBuffer = f.Resolve<IWriteBuffer>();
                        _log = f.Resolve<ILog>();
                    }
                }
                return _log;
            }
        }
    }
}