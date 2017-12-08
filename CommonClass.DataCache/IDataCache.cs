using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.DataCache
{
    /// <summary>
    /// 数据缓冲
    /// </summary>
    public interface IDataCache
    {
        /// <summary>
        /// 添加数据进入缓冲区，如果数据存在抛出异常
        /// </summary>
        /// <param name="key">数据键</param>
        /// <param name="obj">数据对象</param>
        /// <returns>缓冲器对象</returns>
        IDataCache AddData(string key,object obj);
        /// <summary>
        /// 更新缓冲区对象，如果对象不存在则抛出异常
        /// </summary>
        /// <param name="key">数据键</param>
        /// <param name="obj">数据对象</param>
        /// <returns>缓冲器对象</returns>
        IDataCache UpdateData(string key,object obj);

        /// <summary>
        /// 合并数据对象，如果存在则更新，如果不存在则添加。
        /// </summary>
        /// <param name="key">数据键</param>
        /// <param name="obj">数据对象</param>
        /// <returns>缓冲器对象</returns>
        IDataCache MergeData(string key,object obj);
        /// <summary>
        /// 删除缓冲区中的对象。不如存在则删除，不存在则不做任何操作
        /// </summary>
        /// <param name="key">数据键</param>
        /// <returns>缓冲器对象</returns>
        IDataCache RemoveData(string key);
        /// <summary>
        /// 数据键是否存在缓冲区中
        /// </summary>
        /// <param name="key"></param>
        /// <returns>存在返回True，否则返回False</returns>
        bool Exists(string key);
        /// <summary>
        /// 取出缓冲区中的项
        /// </summary>
        /// <param name="key">数据的键</param>
        /// <returns>取出的对象</returns>
        object Get(string key);
        /// <summary>
        /// 删除数据委托。缓冲器会按规则（具体规则取决于实现）调用委托，如果返回True则删除数据。
        /// </summary>
        Func<CacheItem,bool> RemoveDataFunc { get; set; }
        /// <summary>
        /// 添加数据委托。缓冲区会按规则（具体规则取决于实现）调用该委托，如果委托返回不为null则将数据项添加入缓冲区
        /// </summary>
        Func<string,CacheItem> AddDataFunc { get; set; }
    }

    /// <summary>
    /// 缓存的项
    /// </summary>
    public class CacheItem
    {
        /// <summary>
        /// 缓存项的键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 缓存的数据
        /// </summary>
        public object Data { get; set; }
    }
}
