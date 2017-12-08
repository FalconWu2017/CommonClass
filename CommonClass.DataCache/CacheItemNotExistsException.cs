using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.DataCache
{
    public class CacheItemNotExistsException:Exception
    {
        public CacheItemNotExistsException() : base("数据项不存在，请先添加数据项") { }
    }
}
