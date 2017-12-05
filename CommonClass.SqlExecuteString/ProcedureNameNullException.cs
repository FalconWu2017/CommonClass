using System;

namespace CommonClass.SqlExecuteString
{
    /// <summary>
    /// 无法确定存储过程名称引发的错误
    /// </summary>
    public class ProcuderNameNullException : Exception
    {
        /// <summary>
        /// 无法确定存储过程名称引发的错误
        /// </summary>
        public ProcuderNameNullException() : base("无法确定存储过程名称！") { }
    }
}
