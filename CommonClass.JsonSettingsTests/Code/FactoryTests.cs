using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonClass.JsonSettings.Tests
{
    [TestClass()]
    public class FactoryTests
    {
        [TestMethod()]
        public void CreateTest() {
            var path = System.Environment.CurrentDirectory;
            var file = Path.Combine(path,"JosnSettingsTestFile.json");
            var obj = Factory.Create<TestSetting>(file);
            Assert.IsNotNull(obj);
            if(obj is IDisposable dis) {
                dis.Dispose();
            }
        }
    }
}