using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClass.SqlExecuteString;

namespace CommonClass.SqlExecuteStringTests
{
    [ProcuderNameAttribute("testP")]
    class PName
    {
        public int id { get; set; }
        public string Name { get; set; }

        [ProcuderParmIgnore]
        public string p1 { get; set; }

        [ProcuderParmName("p2")]
        public string paaa { get; set; }
    }
}
