using Newtonsoft.Json;

namespace CommonClass.Serializer
{
    /// <summary>
    /// Json序列化配置
    /// </summary>
    public class JsonSerializerConfig
    {
        /// <summary>
        /// 序列化格式。默认None不带缩进
        /// </summary>
        public Formatting Formatting { get; set; } = Formatting.None;
        /// <summary>
        /// 序列化设置
        /// </summary>
        public JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings();
    }
}
