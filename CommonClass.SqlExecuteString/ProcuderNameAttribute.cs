using System;

namespace CommonClass.SqlExecuteString
{
    /// <summary>
    /// 配置存储过程名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = true)]
    public class ProcuderNameAttribute:Attribute
    {
        public ProcuderNameAttribute(string name) { this.Name = name; }
        public string Name { get; set; }
    }
}
