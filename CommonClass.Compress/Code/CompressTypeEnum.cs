using System;

namespace CommonClass.Compress
{
    /// <summary>
    /// 可用压缩方式枚举
    /// </summary>
    [Flags]
    public enum CompressTypeEnum
    {
        zip,
        deflate
    }
}
