using System;

namespace CommonClass.DataCache
{
    /// <summary>
    /// 数据项已经存在异常
    /// </summary>
    public class CacheItemExistsException:Exception
    {
        public CacheItemExistsException() : base("数据项已经存在，不可以重复添加") { }
    }
}
