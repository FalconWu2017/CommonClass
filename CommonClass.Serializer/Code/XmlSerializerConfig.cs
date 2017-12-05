using System.Text;
using System.Xml.Serialization;

namespace CommonClass.Serializer {
    /// <summary>
    /// 提供xml序列化时配置
    /// </summary>
    public class XmlSerializerConfig {
        /// <summary>
        /// 构造默认配置
        /// </summary>
        public XmlSerializerConfig() {
            this.WithHeader = false;
            var defNamespaces=new XmlSerializerNamespaces();
            defNamespaces.Add("", "");
            this.Namespaces = defNamespaces;
            this.EncodingType = Encoding.UTF8;
            this.DefaultXmlHeader = @"<?xml version=""1.0"" encoding=""GB2312""?>";
        }
        /// <summary>
        /// 系列化后是否添加xml头，默认无
        /// </summary>
        public bool WithHeader { get; set; }
        /// <summary>
        /// 默认xml头
        /// </summary>
        public string DefaultXmlHeader { get; set; }
        /// <summary>
        /// xml使用的名字空间，默认无
        /// </summary>
        public XmlSerializerNamespaces Namespaces { get; set; }
        /// <summary>
        /// 序列化和反序列化时使用的编码类型.默认UTF8
        /// </summary>
        public Encoding EncodingType { get; set; }
    }
}