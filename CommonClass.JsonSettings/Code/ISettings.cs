namespace CommonClass.JsonSettings
{
    /// <summary>
    /// 配置提供接口，可以实现保存和读取配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISettings<T>:IGetSettins<T>, ISaveSettings<T> where T : class
    {
    }
}
