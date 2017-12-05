using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Compress
{
    /// <summary>
    /// 压缩返回值
    /// </summary>
    public class ConnmpressResult<T>
    {
        public T Result { get; set; }
        public string CompressType { get; set; }
    }
}
