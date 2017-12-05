using System;

namespace CommonClass.SqlExecuteString
{
    /// <summary>
    /// 忽略此属性为存储过程参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = true)]
    public class ProcuderParmIgnoreAttribute:Attribute
    {
    }
}
