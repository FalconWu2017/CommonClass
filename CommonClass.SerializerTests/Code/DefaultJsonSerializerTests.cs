using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonClass.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Serializer.Tests
{
    [TestClass()]
    public class DefaultJsonSerializerTests
    {

        [TestMethod()]
        public void SerializeTest() {
            datetimeTest();
        }

        private void datetimeTest() {
            ISerializer Ser = new DefaultJsonSerializer();
            string str;
            //默认时间格式测试
            str = Ser.Serialize(new SerObj());
            //Assert.AreEqual("{\"Time\":\"2000-10-10T12:00:00\"}",str);
            StringAssert.Contains(str,"{\"Time\":\"2000-10-10T17:00:00\"}");
            if(Ser is DefaultJsonSerializer js) {
                //微软日期格式测试
                js.Config.Settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                str = Ser.Serialize(new SerObj());
                StringAssert.Contains(str,"{\"Time\":\"\\/Date(971168400000+0800)\\/\"}");
                //自定义日期时间格式测试
                js.Config.Settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                str = Ser.Serialize(new SerObj());
                StringAssert.Contains(str,"{\"Time\":\"2000-10-10 17:00:00\"}");
            }
        }
    }

    /// <summary>
    /// 测试用例
    /// </summary>
    class SerObj
    {
        public DateTime Time { get; set; } = new DateTime(2000,10,10,17,0,0);
    }
}