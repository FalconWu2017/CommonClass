using System.Collections.Generic;

namespace CommonClass.JsonSettings.Tests
{
    public class TestSetting
    {
        public string StrA { get; set; } = "stra";
        public int IntB { get; set; } = 1;
        public string[] Array { get; set; }
        public List<string> List { get; set; }
    }
}