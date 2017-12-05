using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CommonClass.Serializer
{
    /// <summary>
    /// xml序列化和反序列化类
    /// </summary>
    public class DefaultXmlSerializer:IXmlSerializer
    {
        /// <summary>
        /// 构造xml序列化器
        /// </summary>
        /// <param name="config">序列化使用的配置</param>
        public DefaultXmlSerializer() : this(new XmlSerializerConfig()) { }

        /// <summary>
        /// 构造xml序列化器
        /// </summary>
        /// <param name="config">序列化使用的配置</param>
        public DefaultXmlSerializer(XmlSerializerConfig config) {
            this.Config = config;
        }


        /// <summary>
        /// 添加xml头.默认头为<?xml version=""1.0"" encoding=""GB2312""?>
        /// </summary>
        /// <param name="xml">要转换的xml.</param>
        /// <returns>带有头的xml字符串</returns>
        public static string AddXmlHead(string xml,string xmlHead = @"<?xml version=""1.0"" encoding=""GB2312""?>") {
            return xml.StartsWith(xmlHead) ? xml : xmlHead + xml;
        }

        #region ISerializer 成员

        public XmlSerializerConfig Config { get; set; }

        public string Serialize<T>(T obj) where T : class {
            return Serialize(typeof(T),obj);
        }

        public T Deserialize<T>(string str) where T : class {
            return Deserialize(typeof(T),str) as T;
        }

        public string Serialize(Type type,object obj) {
            var xmlSerializer = new XmlSerializer(type);
            var sb = new StringBuilder();
            var xmlWriterSettings = new XmlWriterSettings() {
                OmitXmlDeclaration = !this.Config.WithHeader,
                Encoding = this.Config.EncodingType
            };
            using(var xmlWriter = XmlWriter.Create(sb,xmlWriterSettings)) {
                xmlSerializer.Serialize(xmlWriter,obj,Config.Namespaces);
            }
            return sb.ToString();
        }

        public object Deserialize(Type type,string str) {
            if(string.IsNullOrEmpty(str)) return null;
            var e = new XmlSerializer(type);
            using(StringReader sr = new StringReader(str)) {
                return e.Deserialize(sr);
            }
        }

        #endregion

    }
}