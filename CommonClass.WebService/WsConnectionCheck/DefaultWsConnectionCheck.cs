using System;
using System.ServiceModel;
using CommonClass.Factory;
using CommonClass.WebService.WebHttpRequest;

namespace CommonClass.WebService.WsConnectionCheck
{
    /// <summary>
    /// webservice可连接性检测,通过发送HHTP GET请求测试，webservice服务器返回结果确定是否处于可连接状态
    /// </summary>
    public class DefaultWsConnectionCheck:IWsConnectionCheck, IRegisterBaseInterface
    {
        public IWebHttpRequest Web { get; set; }

        /// <summary>
        /// 通过提供WebHttp请求方法构造webservice检查
        /// </summary>
        /// <param name="whr"></param>
        public DefaultWsConnectionCheck(IWebHttpRequest whr) {
            this.Web = whr;
        }
        /// <summary>
        /// 通过发送HHTP GET请求测试webservice服务器返回结果确定是否处于可连接状态
        /// </summary>
        /// <typeparam name="T">用于连接的WebService代理类型</typeparam>
        /// <param name="ws">Webservice连接代理</param>
        /// <returns>是否检测到正常的服务器返回。</returns>
        public bool Connection<T>(ClientBase<T> ws) where T : class {
            var address = ws.Endpoint.Address.ToString();
            address = address.ToLower().EndsWith("?wsdl") ? address : address + "?wsdl";
            try {
                return this.ResponseStrCheck(this.Web.HttpGet(address));
            }
            catch(Exception) {
                return false;
            }
        }

        /// <summary>
        /// 检测Webservice返回值是否代表可用
        /// </summary>
        /// <param name="str">webservice服务器Get请求响应</param>
        /// <returns></returns>
        protected virtual bool ResponseStrCheck(string str) {
            return str.ToLower().Contains("wsdl:definitions");
        }

    }
}