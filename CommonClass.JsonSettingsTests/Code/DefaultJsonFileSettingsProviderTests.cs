using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace CommonClass.JsonSettings.Tests
{
    [TestClass()]
    public class DefaultJsonFileSettingsProviderTests
    {
        [TestMethod()]
        public void DefaultJsonFileSettingsProviderTest() {
            //定义测试文件
            var path = System.Environment.CurrentDirectory;
            var file = Path.Combine(path,"JosnSettingsTestFile.json");
            //一般写入读取测试
            saveLoadTest(file);
            //多次读取配置测试
            loadMoreTimesTest(file);
            //测试文件监视
            this.fileWatchTest(file);
            //数组测试
            this.dataArrayTest(file);
            //列表测试
            this.dataListTest(file);
            //删除临时设置文件
            if(File.Exists(file)) {
                File.Delete(file);
            }
        }
        /// <summary>
        /// 多次读取测试
        /// </summary>
        private static void loadMoreTimesTest(string file) {
            var provider = Factory.Create<TestSetting>(file);
            provider.SaveSettings(new TestSetting());
            var set1 = provider.GetSettingsObject();
            (provider as DefaultJsonFileSettingsProvider<TestSetting>).ReadSettingsFile += (s,a) => {
                Assert.Fail();
            };
            for(var i = 0;i < 5;i++) {
                var set2 = provider.GetSettingsObject();
                Assert.ReferenceEquals(set1,set2);
            }
            if(provider is IDisposable dis) {
                dis.Dispose();
            }
        }
        /// <summary>
        /// 一般写入读取测试
        /// </summary>
        private static void saveLoadTest(string file) {
            //测试写入
            ISaveSettings<TestSetting> setting = Factory.Create<TestSetting>(file);
            setting.SaveSettings(new TestSetting());
            if(setting is IDisposable dis) {
                dis.Dispose();
            }
            //测试读取
            IGetSettins<TestSetting> set = Factory.Create<TestSetting>(file);
            var obj = set.GetSettingsObject();
            var objT = new TestSetting();
            foreach(var p in typeof(TestSetting).GetProperties()) {
                var pv = p.GetValue(obj);
                var pvt = p.GetValue(objT);
                Assert.AreEqual(pv,pvt);
            }
            if(set is IDisposable disG) {
                disG.Dispose();
            }
        }

        /// <summary>
        /// 配置文件监视测试
        /// </summary>
        private void fileWatchTest(string file) {
            //生成设置提供器
            var settingProvider = Factory.Create<TestSetting>(file);
            //获取第一次数据
            var settingS = settingProvider.GetSettingsObject();
            var ib = settingS.IntB;
            //生成更改数据
            var setT = new TestSetting {
                IntB = settingS.IntB + 1,StrA = settingS.StrA,
            };
            Assert.IsNotNull((settingProvider as DefaultJsonFileSettingsProvider<TestSetting>).Settings);
            //更改配置文件
            settingProvider.SaveSettings(setT);
            Thread.Sleep(1);
            //更改后，第一次读取前，缓冲为空
            Assert.IsNull((settingProvider as DefaultJsonFileSettingsProvider<TestSetting>).Settings);
            //第二次读取配置
            var set2 = settingProvider.GetSettingsObject();
            //测试读取数据是否正确
            Assert.AreEqual(set2.IntB,settingS.IntB + 1);
            //释放资源
            if(settingProvider is IDisposable dis) {
                dis.Dispose();
            }
        }

        /// <summary>
        /// 测试数组写入与读取
        /// </summary>
        /// <param name="file"></param>
        private void dataArrayTest(string file) {
            var provider = Factory.Create<TestSetting>(file);
            var set = new TestSetting {
                Array = new string[] { "abc","123" },
            };
            provider.SaveSettings(set);
            var set2 = provider.GetSettingsObject();
            Assert.AreEqual("abc",set2.Array[0]);
            Assert.AreEqual("123",set2.Array[1]);
            //释放资源
            if(provider is IDisposable dis) {
                dis.Dispose();
            }
        }

        /// <summary>
        /// 测试列表写入与读取
        /// </summary>
        /// <param name="file"></param>
        private void dataListTest(string file) {
            var provider = Factory.Create<TestSetting>(file);
            var set = new TestSetting {
                List = new System.Collections.Generic.List<string> { "abc","123" },
            };
            provider.SaveSettings(set);
            var set2 = provider.GetSettingsObject();
            Assert.AreEqual("abc",set2.List.ToArray()[0]);
            Assert.AreEqual("123",set2.List.ToArray()[1]);
            //释放资源
            if(provider is IDisposable dis) {
                dis.Dispose();
            }
        }
    }
}