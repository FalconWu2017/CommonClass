using System.Collections.Specialized;

namespace CommonClass.WebService.WebHttpRequest
{
    /// <summary>
    /// 网络Http通信组件
    /// </summary>
    public interface IWebHttpRequest
    {
        /// <summary>
        /// 向服务器发送get请求，并获取数据
        /// </summary>
        /// <param name="url">服务器url</param>
        /// <param name="data">要发送的数据</param>
        /// <returns></returns>
        string HttpGet(string url, NameValueCollection data = null);

        /// <summary>
        /// 向服务器发送post请求，并获取数据
        /// </summary>
        /// <param name="url">服务器url</param>
        /// <param name="data">要发送的数据</param>
        /// <returns></returns>
        string HttpPost(string url, NameValueCollection data = null);
    }
}
