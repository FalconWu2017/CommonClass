namespace CommonClass.Settings
{
    /// <summary>
    /// 配置值提供器
    /// </summary>
    public interface ISettingProvider
    {
        /// <summary>
        /// 获取值，如果没有配置该值，返回null。
        /// </summary>
        /// <param name="key">对应的键</param>
        /// <returns>值，如果没有返回null。</returns>
        string GetValue(string key);
    }
}