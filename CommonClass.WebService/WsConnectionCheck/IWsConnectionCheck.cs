using System.ServiceModel;

namespace CommonClass.WebService.WsConnectionCheck
{
    /// <summary>
    /// WebService连接测试。测试是否可以连接
    /// </summary>
    public interface IWsConnectionCheck
    {
        /// <summary>
        /// 测试Webservice服务器是否正常
        /// </summary>
        /// <typeparam name="T">Webservice服务器代理类型</typeparam>
        /// <param name="ws">Webservice实例</param>
        /// <returns>True表示可连接，False不可连接。</returns>
        bool Connection<T>(ClientBase<T> ws) where T : class;
    }
}
