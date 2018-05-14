using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonClass.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CommonClass.Settings.Tests
{
    [TestClass()]
    public class DefaultSettingTests
    {
        [TestMethod()]
        public void GetValueTest() {
            var setting = SettingsFactory.Instantiates();
            Assert.IsNotNull(setting,"配置器对象没有创建成功");

            setting.AddDefaultProviders();
            var asm = Assembly.GetAssembly(typeof(ISettingProvider));
            var providers = asm.GetTypes().Where(m => m.GetInterface(nameof(ISettingProvider)) != null);
            Assert.AreEqual(providers.Count(),DefaultSetting.Factorys.Count(),"添加的默认提供器数量不正确");

            var val = setting.GetValue("abc");
            Assert.AreEqual("123",val,"从配置文件中获取的值不正确");

            var setting1 = SettingsFactory.Instantiates();
            var setting2 = SettingsFactory.Instantiates();
            Assert.IsTrue(setting1.Equals(setting2),"多次创建配置器返回了不同的实例");
        }
    }
}