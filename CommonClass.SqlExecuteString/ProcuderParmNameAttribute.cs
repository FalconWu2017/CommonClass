using System;

namespace CommonClass.SqlExecuteString
{
    /// <summary>
    /// 存储过程参数名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = true)]
    public class ProcuderParmNameAttribute:Attribute
    {
        /// <summary>
        /// 配置存储过程参数名称
        /// </summary>
        /// <param name="name">参数名称</param>
        public ProcuderParmNameAttribute(string name) { this.Name = name; }

        /// <summary>
        /// 对应存储过程参数名称
        /// </summary>
        public string Name { get; set; }
    }
}
