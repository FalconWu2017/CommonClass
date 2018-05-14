namespace CommonClass.Settings
{
    /// <summary>
    /// 提供器工厂接口
    /// </summary>
    public interface IProviderFactory
    {
        /// <summary>
        /// 创建值提供器
        /// </summary>
        /// <returns>值提供器</returns>
        ISettingProvider Instantiates();
    }
}