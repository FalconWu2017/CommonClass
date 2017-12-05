using System;
using System.IO;
using System.Text;
using CommonClass.Serializer;

namespace CommonClass.JsonSettings
{
    /// <summary>
    /// 默认Json配置文件提供器
    /// </summary>
    public class DefaultJsonFileSettingsProvider<T>:ISettings<T>, IDisposable
        where T : class
    {
        #region 属性
        /// <summary>
        /// 文件修改监视器
        /// </summary>
        public FileSystemWatcher FileWatcher { get; set; }

        private string _fileFullPath = null;
        /// <summary>
        /// 配置文件完整路径
        /// </summary>
        public string FileFullPath {
            get {
                return this._fileFullPath;
            }

            set {
                if(!File.Exists(value)) {
                    File.Create(value).Close();
                }
                this._fileFullPath = value;
                #region 监视配置文件修改
                if(this.FileWatcher != null) {
                    this.FileWatcher.Changed -= this.fsw_Changed;
                }
                if(this.EnableWatchFileChange) {
                    if(this.FileWatcher == null) {
                        this.FileWatcher = new FileSystemWatcher(Path.GetDirectoryName(this._fileFullPath),Path.GetFileName(this._fileFullPath)) {
                            EnableRaisingEvents = true,
                            IncludeSubdirectories = false,
                            NotifyFilter = NotifyFilters.LastWrite,
                        };
                    }
                    this.FileWatcher.Changed += this.fsw_Changed;
                }
                #endregion
            }
        }

        private void fsw_Changed(object sender,FileSystemEventArgs e) {
            if(e.FullPath == this._fileFullPath) {
                this.ClearSettingsWhenFileChange?.Invoke(this,EventArgs.Empty);
                this.Settings = null;
            }
        }

        /// <summary>
        /// json格式反序列化器
        /// </summary>
        public IJsonSerializer Ser { get; set; }
        /// <summary>
        /// 是否监视配置文件的更改
        /// </summary>
        public bool EnableWatchFileChange { get; set; } = true;
        /// <summary>
        /// 配置模型对象
        /// </summary>
        public T Settings { get; set; } = null;

        #endregion

        #region 事件
        /// <summary>
        /// 当因配置文件改变清空本地缓存设置。提供被清空的本地设置和改变的文件。
        /// </summary>
        public event EventHandler ClearSettingsWhenFileChange;
        /// <summary>
        /// 当发生读取配置文件时触发该事件
        /// </summary>
        public event EventHandler ReadSettingsFile;
        #endregion

        #region 构造
        /// <summary>
        /// 通过提供配置文件路径和序列化器实例化配置提供器
        /// </summary>
        /// <param name="fileFullName">完整的文件路径</param>
        /// <param name="s">Json序列化器</param>
        public DefaultJsonFileSettingsProvider(string fileFullName,IJsonSerializer s) {
            this.Ser = s ?? throw new ArgumentNullException(nameof(s));
            this.FileFullPath = fileFullName;
        }

        #endregion

        T IGetSettins<T>.GetSettingsObject() {
            if(this.Settings != null) return this.Settings;

            byte[] Unicode = new byte[] { 0xFF,0xFE,0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE,0xFF,0x00 };
            byte[] UTF8 = new byte[] { 0xEF,0xBB,0xBF };

            if(!File.Exists(this.FileFullPath)) {
                this.Settings = typeof(T).Assembly.CreateInstance(typeof(T).FullName) as T;
                return this.Settings;
            }
            using(var fs = new FileStream(this.FileFullPath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite)) {
                var br = new BinaryReader(fs);
                var b1 = br.ReadBytes(3);
                Encoding en = null;
                //编码判断
                if(en == null && b1[0] == UTF8[0] && b1[1] == UTF8[1] && b1[2] == UTF8[2]) {
                    en = Encoding.UTF8;
                }
                if(en == null && b1[0] == UnicodeBIG[0] && b1[1] == UnicodeBIG[1] && b1[2] == UnicodeBIG[2]) {
                    en = Encoding.Unicode;
                }
                if(en == null && b1[0] == Unicode[0] && b1[1] == Unicode[1] && b1[2] == Unicode[2]) {
                    en = Encoding.BigEndianUnicode;
                }
                if(en == null) {
                    throw new Exception("配置文件编码格式错误，仅支持UTF8 Unicode编码，并且需要编码头。");
                }
                var buf = new byte[fs.Length];
                fs.Read(buf,0,buf.Length);
                fs.Close();
                var str = en.GetString(buf,0,buf.Length);
                this.Settings = this.Ser.Deserialize<T>(str);
                this.ReadSettingsFile?.Invoke(this,EventArgs.Empty);
                return this.Settings;
            }
        }

        T ISaveSettings<T>.SaveSettings(T data) {
            byte[] UTF8 = new byte[] { 0xEF,0xBB,0xBF };
            if(this.Ser is CommonClass.Serializer.DefaultJsonSerializer djs) {
                djs.Config.Formatting = Newtonsoft.Json.Formatting.Indented;
            }
            var str = this.Ser.Serialize(data);
            var buf = Encoding.UTF8.GetBytes(str);

            //if(File.Exists(this.FileFullPath)) {
            //    File.Delete(this.FileFullPath);
            //}
            using(var fs = new FileStream(this.FileFullPath,FileMode.Create,FileAccess.Write,FileShare.None)) {
                fs.Write(UTF8,0,UTF8.Length);
                fs.Write(buf,0,buf.Length);
                fs.Flush();
                fs.Close();
            }
            return data;
        }

        public void Dispose() {
            if(this.FileWatcher != null) {
                this.FileWatcher.Changed -= this.fsw_Changed;
            }
        }
    }
}