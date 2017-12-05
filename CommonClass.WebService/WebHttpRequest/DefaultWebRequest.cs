using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using CommonClass.Factory;

namespace CommonClass.WebService.WebHttpRequest
{
    public class DefaultWebRequest:IRegisterBaseInterface, IWebHttpRequest
    {
        #region IWebHttpRequest 成员

        public string HttpGet(string url,NameValueCollection data = null) {
            //参数
            string parms = formatData(data);
            //请求的地址
            var requestUrl = url.StartsWith("http://") ? url : "http://" + url;
            requestUrl = string.IsNullOrEmpty(parms) ? requestUrl : requestUrl + "?" + parms;
            //请求
            var request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            request.Headers.Add("Accept-Encoding","gzip,deflate");
            request.Method = "get";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //响应
            WebResponse response = request.GetResponse();
            //解压
            Stream compression = this.decompress(response.GetResponseStream(),response.Headers["Content-encoding"]);
            //获取编码方式
            //application/json; charset=utf-8
            var ct = response.ContentType;
            //获取返回结果
            var sr = new StreamReader(compression,Encoding.UTF8);
            var result = sr.ReadToEnd();
            sr.Close();
            compression.Close();
            response.Close();
            request.Abort();
            return result;
        }

        public string HttpPost(string url,NameValueCollection data = null) {
            //参数
            var parms = formatData(data);
            //请求的地址
            string requestUrl = url.StartsWith("http://") ? url : "http://" + url;
            //请求
            var request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            request.Headers.Add("Accept-Encoding","gzip,deflate");
            request.Method = "post";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            //传送数据
            if(!string.IsNullOrEmpty(parms)) {
                var buf = Encoding.UTF8.GetBytes(parms);
                using(var qs = request.GetRequestStream()) {
                    qs.Write(buf,0,buf.Length);
                    qs.Close();
                }
            }
            //响应
            WebResponse response = request.GetResponse();
            //解压
            Stream compression = this.decompress(response.GetResponseStream(),response.Headers["Content-encoding"]);
            //获取编码方式
            //application/json; charset=utf-8
            var ct = response.ContentType;
            //获取返回结果
            StreamReader sr = new StreamReader(compression,Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            compression.Close();
            response.Close();
            request.Abort();
            return result;
        }

        #endregion

        /// <summary>
        /// 格式化请求参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string formatData(NameValueCollection data) {
            if(data == null || !data.HasKeys()) {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            foreach(var k in data.AllKeys) {
                if(!string.IsNullOrEmpty(k)) {
                    if(data.GetValues(k).Length == 0) {
                        sb.AppendFormat("{0}=&",k);
                    }
                    else {
                        foreach(string v in data.GetValues(k)) {
                            sb.AppendFormat("{0}={1}&",k,v);
                        }
                    }
                }
            }
            return sb.ToString().TrimEnd('&');
        }

        /// <summary>
        /// 解压响应流
        /// </summary>
        private Stream decompress(Stream input,string compressType) {
            if(string.IsNullOrEmpty(compressType)) return input;
            if(compressType.ToLower() == "gzip") {
                return new GZipStream(input,CompressionMode.Decompress);
            }
            if(compressType.ToLower() == "deflate") {
                return new DeflateStream(input,CompressionMode.Decompress);
            }
            return input;
        }
    }
}