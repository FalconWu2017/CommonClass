using CommonClass.Factory;

namespace CommonClass.Serializer
{
    /// <summary>
    /// XML序列化接口
    /// </summary>
    public interface IXmlSerializer:ISerializer
    {
        XmlSerializerConfig Config { get; set; }
    }
}
