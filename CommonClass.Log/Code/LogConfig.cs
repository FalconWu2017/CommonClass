using System.Text;

namespace CommonClass.Log
{
    /// <summary>
    /// 日志记录配置
    /// </summary>
    public class LogConfig
    {
        private static LogConfig _Default=null;

        /// <summary>
        /// 默认配置
        /// </summary>
        public static LogConfig Default {
            get {
                if(_Default == null) _Default = new LogConfig();
                return _Default;
            }
        }

        /// <summary>
        /// 生成默认的配置
        /// </summary>
        public LogConfig() {
            this.UseBuffer = true;
            this.CallWriterWhenImportant = true;
            this.BufferSize = 3;
            this.FileConfig = FileLogConfig.Default;
        }
        /// <summary>
        /// 是否使用数据缓冲.默认使用缓冲区
        /// </summary>
        public bool UseBuffer { get; set; }

        /// <summary>
        /// 缓冲区尺寸。默认3条记录
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// 遇到信重要记录立即将当前消息和缓冲的记录写入设备。默认立即保存
        /// </summary>
        public bool CallWriterWhenImportant { get; set; }

        /// <summary>
        /// 文件log配置
        /// </summary>
        public FileLogConfig FileConfig { get; set; }
    }

    /// <summary>
    /// 文件log配置
    /// </summary>
    public class FileLogConfig
    {
        private static FileLogConfig _Default=null;

        /// <summary>
        /// 默认文件log配置
        /// </summary>
        public static FileLogConfig Default {
            get {
                if(_Default == null) _Default = new FileLogConfig();
                return _Default;
            }
        }

        public FileLogConfig() {
            this.BasePath = null;
            this.FileEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// 基础日志记录目录
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 文件记录日志编码
        /// </summary>
        public Encoding FileEncoding { get; set; }

    }
}