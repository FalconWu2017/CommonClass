using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Compress
{
    /// <summary>
    /// 压缩参数
    /// </summary>
    public class CompressOption
    {
        public List<string> CompressStorList { get; set; } = new List<string> { "zip","deflate" };
    }
}
