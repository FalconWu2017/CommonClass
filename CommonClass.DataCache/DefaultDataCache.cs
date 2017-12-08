using System;
using System.Collections.Generic;

namespace CommonClass.DataCache
{
    public class DefaultDataCache:IDataCache
    {
        /// <summary>
        /// 默认数据缓冲时间，如果时间到达将删除数据。
        /// </summary>
        public TimeSpan DefaultRemovetimeSpan { get; set; } = new TimeSpan(0,10,0);

        /// <summary>
        /// 用于保存数据的缓冲区
        /// </summary>
        public SortedList<string,CacheItem> List { get; set; } = new SortedList<string,CacheItem>();
        /// <summary>
        /// 当观察数据时候更新数据更新时间，这将导致缓冲期延长。
        /// </summary>
        public bool UpdateMergeTimeWhenWatch { get; set; } = true;

        #region IDataCache 实现

        public Func<CacheItem,bool> RemoveDataFunc { get; set; } = (item) => {
            if(item is CacheItemEx cie && DateTime.Now > cie.MargeDateTime + cie.RemoveTimeSpan) {
                return true;
            }
            return false;
        };

        public Func<string,CacheItem> AddDataFunc { get; set; } = (item) => {
            return null;
        };

        public IDataCache AddData(string key,object obj) {
            if(this.Exists(key)) {
                throw new CacheItemExistsException();
            }
            this.List.Add(key,new CacheItemEx {
                Key = key,Data = obj,MargeDateTime = DateTime.Now,RemoveTimeSpan = DefaultRemovetimeSpan,
            });
            return this;
        }

        public bool Exists(string key) {
            var obj = this.Get(key);
            if(obj == null) return false;
            if(this.RemoveDataFunc(obj as CacheItem)) {
                this.RemoveData(key);
                return false;
            }
            return true;
        }

        public object Get(string key) {
            if(this.List.TryGetValue(key,out CacheItem obj)) {
                if(this.RemoveDataFunc(obj)) {
                    this.RemoveData(obj.Key);
                    return null;
                }
                if(this.UpdateMergeTimeWhenWatch && obj is CacheItemEx cie) {
                    cie.MargeDateTime = DateTime.Now;
                }
                return obj.Data;
            }
            return null;
        }

        public IDataCache MergeData(string key,object obj) {
            if(this.Exists(key)) {
                this.UpdateData(key,obj);
            }
            else {
                this.AddData(key,obj);
            }
            return this;
        }

        public IDataCache RemoveData(string key) {
            List.Remove(key);
            return this;
        }

        public IDataCache UpdateData(string key,object obj) {
            var data = this.Get(key) as CacheItemEx;
            if(data == null) {
                throw new CacheItemNotExistsException();
            }
            if(this.UpdateMergeTimeWhenWatch) {
                data.MargeDateTime = DateTime.Now;
            }
            data.Data = obj;
            return this;
        }

        #endregion
    }
    /// <summary>
    /// 默认的数据缓冲器数据项
    /// </summary>
    public class CacheItemEx:CacheItem
    {
        /// <summary>
        /// 添加或更新时间
        /// </summary>
        public DateTime MargeDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 数据生存时间
        /// </summary>
        public TimeSpan RemoveTimeSpan { get; set; }
    }
}
