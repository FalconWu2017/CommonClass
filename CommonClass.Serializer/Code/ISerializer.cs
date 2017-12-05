using System;
using CommonClass.Factory;

namespace CommonClass.Serializer
{
    /// <summary>
    /// 序列化接口
    /// </summary>
    public interface ISerializer:IRegisterBaseInterface
    {
        /// <summary>
        /// 序列化指定对象
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        string Serialize<T>(T obj) where T : class;
        /// <summary>
        /// 序列化指定对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>序列化后的字符串</returns>
        string Serialize(Type type,object obj);

        /// <summary>
        /// 反序列化指定对象
        /// </summary>
        /// <param name="str">对象序列化后的字符串</param>
        /// <returns></returns>
        T Deserialize<T>(string str) where T : class;

        /// <summary>
        /// 反序列化指定对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="str">反序列化的字符串</param>
        /// <returns>兑现</returns>
        object Deserialize(Type type,string str);
    }
}
