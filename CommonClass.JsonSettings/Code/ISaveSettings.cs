namespace CommonClass.JsonSettings
{
    /// <summary>
    /// 保存配置内容
    /// </summary>
    /// <typeparam name="T">配置的类型</typeparam>
    public interface ISaveSettings<T>
    {
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="data">配置模型</param>
        /// <returns>配置对象本身</returns>
        T SaveSettings(T data);
    }
}
