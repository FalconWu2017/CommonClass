using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.ModelSql
{
    /// <summary>
    /// 定义存储过程名称
    /// </summary>
    public class ProcuderNameAttribute:Attribute
    {
        public string ProcuderName { get; set; }

        public ProcuderNameAttribute(string m) => this.ProcuderName = m;
    }
}
