using Autofac;
using CommonClass.Factory;

namespace CommonClass.DataCache
{
    /// <summary>
    /// CacheData IOC注册器
    /// </summary>
    public class CacheRegister:IRegister
    {
        /// <summary>
        /// 注册IDataCache 及其实现
        /// </summary>
        /// <param name="builder"></param>
        public void Register(ContainerBuilder builder) {
            builder.Register(c => DataCacheFactory.CreateCache()).As<IDataCache>();
        }
    }
}
