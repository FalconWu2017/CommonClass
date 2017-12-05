using CommonClass.Serializer;

namespace CommonClass.JsonSettings
{
    /// <summary>
    /// 配置工厂
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// 通过提供配置类型、配置文件名创建配置提供器
        /// </summary>
        /// <typeparam name="T">配置文件类型</typeparam>
        /// <param name="file">配置文件名称</param>
        public static ISettings<T> Create<T>(string file) where T : class {
            return Create<T>(file,new DefaultJsonSerializer());
        }
        /// <summary>
        /// 通过提供配置类型、配置文件名和Json序列化器创建配置提供器
        /// </summary>
        /// <typeparam name="T">配置文件类型</typeparam>
        /// <param name="file">配置文件名称</param>
        /// <param name="serializer">Json序列化器</param>
        public static ISettings<T> Create<T>(string file,IJsonSerializer serializer) where T : class {
            return new DefaultJsonFileSettingsProvider<T>(file,serializer);
        }

    }
}
