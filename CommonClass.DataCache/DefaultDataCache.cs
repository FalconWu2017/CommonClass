using System;
using System.Collections.Generic;

namespace CommonClass.DataCache
{
    /// <summary>
    /// 默认数据缓冲区对象，调用方必须维护缓冲区对象生命期，此对象不会自行维护。
    /// </summary>
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
        /// <summary>
        /// 委托。判断数据项是否过期，如果过期将在下次观察时删除。
        /// 默认检测是否超过RemoveTimeSpan规定的过期时间
        /// </summary>
        public Func<CacheItem,bool> RemoveDataFunc { get; set; } = (item) => {
            if(item is CacheItemEx cie && DateTime.Now > cie.MargeDateTime + cie.RemoveTimeSpan) {
                return true;
            }
            return false;
        };
        /// <summary>
        /// 添加数据委托。传入添加的数据key，观察数据时候调用，如果返回不为null，就将数据添加进缓冲区，默认返回null，
        /// 如果返回不为null，则调用AddData方法添加数据。
        /// 实现应根据传入的key书写数据获取方法。
        /// <para>也可以调用AddData方法添加数据</para>
        /// </summary>
        public Func<string,CacheItem> AddDataFunc { get; set; } = (key) => {
            return null;
        };
        /// <summary>
        /// 将数据添加到缓冲区。
        /// </summary>
        /// <param name="key">数据的键</param>
        /// <param name="obj">数据对象，如果数据为null则不进行任何操作</param>
        /// <returns>数据缓冲对象</returns>
        public IDataCache AddData(string key,object obj) {
            if(obj == null) return this;
            if(this.Exists(key)) {
                throw new CacheItemExistsException();
            }
            List.Add(key,new CacheItemEx {
                Key = key,Data = obj,MargeDateTime = DateTime.Now,RemoveTimeSpan = this.DefaultRemovetimeSpan,
            });
            return this;
        }
        /// <summary>
        /// 观察对象是否存在。方法首先会检查数据是否过期。如果过去则会删除。然后调用AddDataFunc委托。如果返回不为null，则调用AddData方法添加数据。
        /// </summary>
        /// <param name="key">数据的键</param>
        /// <returns>如果存在返回True，否则False</returns>
        public bool Exists(string key) {
            //获取数据
            var obj = this.Get(key);
            //如果存在数据，则判断是否过期，过期则删除
            if(obj != null && this.RemoveDataFunc(obj as CacheItem)) {
                this.RemoveData(key);
                obj = null;
            }
            //如果无数据则尝试通过添加过去添加数据
            if(obj == null) {
                obj = this.AddDataFunc(key);
                if(obj != null) {
                    this.AddData(key,obj);
                }
            }
            //返回数据是否仍然存在
            return obj != null;
        }
        /// <summary>
        /// 从缓冲器过去数据并且判断数据是否过期，如果过期调用RemoveData方法删除数据。并且根据UpdateMergeTimeWhenWatch设置更新观察时间。
        /// </summary>
        /// <param name="key">要获取的数据键</param>
        /// <returns>数据对象或null</returns>
        public object Get(string key) {
            if(List.TryGetValue(key,out CacheItem obj)) {
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
        /// <summary>
        /// 合并数据。如果数据存在则调用UpdateData方法升级数据，否则调用AddData添加数据。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IDataCache MergeData(string key,object obj) {
            if(this.Exists(key)) {
                this.UpdateData(key,obj);
            }
            else {
                this.AddData(key,obj);
            }
            return this;
        }
        /// <summary>
        /// 从缓冲区删除数据
        /// </summary>
        /// <param name="key">数据的键</param>
        /// <returns></returns>
        public IDataCache RemoveData(string key) {
            this.List.Remove(key);
            return this;
        }
        /// <summary>
        /// 升级数据。如果数据不存在则抛出异常。除此之外还会更新观察时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IDataCache UpdateData(string key,object obj) {
            if(this.List.TryGetValue(key,out CacheItem o)) {
                o.Data = obj;
                if(o is CacheItemEx oe) {
                    oe.MargeDateTime = DateTime.Now;
                }
                return this;
            }
            throw new CacheItemNotExistsException();
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
