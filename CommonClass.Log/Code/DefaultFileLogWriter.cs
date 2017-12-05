using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CommonClass.Log
{
    /// <summary>
    /// 默认文件日志写入设备
    /// </summary>
    public class DefaultFileLogWriter : ILogWriter
    {
        /// <summary>
        ///  文件名生成器集合
        /// </summary>
        public IEnumerable<ILogFileNameMaker> FileNameMakers { get; set; }
        /// <summary>
        /// 消息格式
        /// </summary>
        public ILogFileMsgFormat LogFileMsgFormat { get; set; }


        public DefaultFileLogWriter(IEnumerable<ILogFileNameMaker> lfnm, ILogFileMsgFormat lfmf) {
            this.FileNameMakers = lfnm;
            this.LogFileMsgFormat = lfmf;
        }

        #region ILogWriter 成员

        public void Write(params LogContext[] context) {
            if(context == null || context.Length == 0) return;
            string basePath=context.First().Config.FileConfig.BasePath;
            //整理写入字典
            //key为完整的要写入的文件路径名。Value为要写入的字符串
            Dictionary<string,StringBuilder> buf=new Dictionary<string, StringBuilder>();
            foreach(var fm in FileNameMakers) {
                StringBuilder sb;
                foreach(var c in context) {
                    var fileName= fm.MakeFileName(c);
                    if(!string.IsNullOrEmpty(fileName)) {
                        var fullPath=Path.Combine(basePath, fileName);
                        if(buf.ContainsKey(fullPath)) {
                            if(buf.TryGetValue(fullPath, out sb)) {
                                sb.Append(LogFormat(c));
                            }
                        }
                        else {
                            sb = new StringBuilder();
                            sb.Append(LogFormat(c));
                            buf.Add(fullPath, sb);
                        }
                    }
                }
            }
            //写入文件
            Encoding en=context.First().Config.FileConfig.FileEncoding;
            foreach(var fn in buf.Keys) {
                BaseWriteToFile(buf, en, fn);
            }
        }
        private static object fileLock=new object();

        private void BaseWriteToFile(Dictionary<string, StringBuilder> buf, Encoding en, string fn) {
            lock(fileLock) {
                CreateDir(fn); CreateFile(fn);
                using(FileStream fs=File.Open(fn, FileMode.Append, FileAccess.Write, FileShare.Read)) {
                    StringBuilder sb=null;
                    if(buf.TryGetValue(fn, out sb)) {
                        var data=en.GetBytes(sb.ToString());
                        fs.Write(data, 0, data.Length);
                    }
                    fs.Flush(); fs.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// 格式化log记录存储格式
        /// </summary>
        private string LogFormat(LogContext context) {
            return this.LogFileMsgFormat.Format(context);
        }

        private void CreateDir(string filePath) {
            var dir=Path.GetDirectoryName(filePath);
            if(!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
        }

        private void CreateFile(string filePath) {
            if(!File.Exists(filePath)) {
                var fs = File.Create(filePath);
                fs.Close();
            }
        }
    }
}