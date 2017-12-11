using System;

namespace CommonClass.DataCache
{
    public class CacheItemNotExistsException:Exception
    {
        public CacheItemNotExistsException() : base("数据项不存在，请先添加数据项") { }
    }
}
