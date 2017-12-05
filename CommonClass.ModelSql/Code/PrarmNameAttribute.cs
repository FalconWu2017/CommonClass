using System;

namespace CommonClass.ModelSql
{
    /// <summary>
    /// 定义名称
    /// </summary>
    public class PrarmNameAttribute:Attribute
    {
        public PrarmNameAttribute(string name) { this.Name = name; }
        public string Name { get; set; }
    }
}
