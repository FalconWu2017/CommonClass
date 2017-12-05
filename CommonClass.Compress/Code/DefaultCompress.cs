using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Compress
{
    public class DefaultCompress:ICompress
    {
        public List<string> CompressStorList { get; set; } = new List<string> { "zip","deflate" };

        ConnmpressResult<T> ICompress.Compress<T>(T inPut,string compressType) {
            throw new NotImplementedException();
        }

        ConnmpressResult<T> ICompress.UnCompress<T>(T inPut) {

            ICompress aa = new DefaultCompress();
            aa.Compress(new object(),"gzip");
            throw new NotImplementedException();
        }
    }
}
