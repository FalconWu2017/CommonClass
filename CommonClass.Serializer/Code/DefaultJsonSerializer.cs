using System;
using Newtonsoft.Json;

namespace CommonClass.Serializer
{
    /// <summary>
    /// json默认序列化和反序列化类
    /// </summary>
    public class DefaultJsonSerializer:IJsonSerializer
    {
        /// <summary>
        /// 序列化配置
        /// </summary>
        public JsonSerializerConfig Config { get; set; }

        /// <summary>
        /// 用默认的配置创建序列化器
        /// </summary>
        /// <example>
        /// IJsonSerializer ser =new DefaultJsonSerializer();
        /// </example>
        public DefaultJsonSerializer() {
            this.Config = new JsonSerializerConfig();
        }

        #region IJsonSerializer 成员

        public string Serialize<T>(T obj) where T : class {
            return JsonConvert.SerializeObject(obj,this.Config.Formatting,this.Config.Settings);
        }

        public T Deserialize<T>(string str) where T : class {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public string Serialize(Type type,object obj) {
            return JsonConvert.SerializeObject(obj,type,this.Config.Formatting,this.Config.Settings);
        }

        public object Deserialize(Type type,string str) {
            return JsonConvert.DeserializeObject(str,type);
        }

        #endregion
    }
}