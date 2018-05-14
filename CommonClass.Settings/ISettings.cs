namespace CommonClass.Settings
{
    /// <summary>
    /// 配置方法接口
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// 从配置中获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);
    }
}
