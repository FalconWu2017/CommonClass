using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonClass.DataCache.Tests
{
    [TestClass()]
    public class DefaultDataCacheTests
    {
        protected DefaultDataCache Cache {
            get {
                return new DefaultDataCache();
            }
        }

        [TestMethod()]
        public void AddDataTest() {
            var c = this.Cache;
            var data = new CacheItem { Key = "a",Data = "adata" };
            c.AddData(data.Key,data.Data);
            Assert.IsTrue(c.List.ContainsKey(data.Key));
            var cd = c.Get(data.Key) as string;
            Assert.IsNotNull(cd);
            Assert.AreEqual(data.Data,cd);
        }

        [TestMethod()]
        public void ExistsTest() {
            var c = Cache;
            var data = new CacheItem { Key = "a",Data = "adata" };
            c.AddData(data.Key,data.Data);
            Assert.IsTrue(c.Exists(data.Key));
            Assert.IsFalse(c.Exists("abc"));
        }

        [TestMethod()]
        public void GetTest() {
            var c = Cache;
            var data = new CacheItem { Key = "a",Data = "adata" };
            c.AddData(data.Key,data.Data);
            var cd = c.Get(data.Key) as string;
            Assert.IsNotNull(cd);
            Assert.AreEqual(data.Data,cd);

            var ncd = c.Get("abc");
            Assert.IsNull(ncd);
        }

        [TestMethod()]
        public void MergeDataTest() {
            var c = Cache;
            var data = new CacheItem { Key = "a",Data = "adata" };
            //数据不存在
            Assert.IsNull(c.Get(data.Key));
            c.MergeData(data.Key,data.Data);
            var d = c.Get(data.Key);
            Assert.IsNotNull(d);
            Assert.AreEqual(data.Data,d);

            //数据存在
            c.MergeData(data.Key,"mergeD");
            d = c.Get(data.Key);
            Assert.IsNotNull(d);
            Assert.AreEqual("mergeD",d);
        }

        [TestMethod()]
        public void RemoveDataTest() {
            var c = Cache;
            var data = new CacheItem { Key = "a",Data = "adata" };
            c.AddData(data.Key,data.Data);
            Assert.IsNotNull(c.Get(data.Key));
            c.RemoveData(data.Key);
            Assert.IsNull(c.Get(data.Key));
        }

        [TestMethod()]
        public void UpdateDataTest() {
            var c = Cache;
            var data = new CacheItem { Key = "a",Data = "adata" };
            //数据不存在
            Assert.IsNull(c.Get(data.Key));
            c.AddData(data.Key,data.Data);

            //数据存在
            c.UpdateData(data.Key,"mergeD");
            var d = c.Get(data.Key);
            Assert.IsNotNull(d);
            Assert.AreEqual("mergeD",d);
        }
    }
}