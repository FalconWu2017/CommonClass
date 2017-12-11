namespace CommonClass.DataCache
{
    /// <summary>
    /// 缓冲器创建工厂
    /// </summary>
    public static  class DataCacheFactory
    {
        /// <summary>
        /// 创建默认的缓冲器
        /// </summary>
        /// <returns></returns>
        public static IDataCache CreateCache() {
            return new DefaultDataCache();
        }
    }
}
