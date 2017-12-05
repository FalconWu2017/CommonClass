using Autofac;
using CommonClass.FactoryTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonClass.Factory.Tests
{
    [TestClass()]
    public class IOCFactoryTests
    {
        [TestMethod()]
        public void IOCFactoryTest() {
            //无参构造函数测试
            var f = new IOCFactory();
            Assert.IsNotNull(f.Assemblies);
            Assert.AreEqual(0,f.Assemblies.Length);
            Assert.IsNull(f.Container);
            Assert.IsNull(f.Builder);
            f.Dispose();
            //操作选项传递测试
            var op = new FactoryOption();
            f = new IOCFactory(op,this.GetType().Assembly);
            Assert.IsNotNull(f.Assemblies);
            Assert.AreEqual(op,f.Option);
            Assert.IsNull(f.Container);
            Assert.IsNull(f.Builder);
            f.Dispose();

            //测试无任何操作
            op = new FactoryOption { InitOption = InitOptionEnum.None };
            f = new IOCFactory(op,this.GetType().Assembly);
            Assert.IsNotNull(f.Assemblies);
            Assert.AreEqual(op,f.Option);
            Assert.IsNull(f.Container);
            Assert.IsNull(f.Builder);
            f.Dispose();
        }

        [TestMethod()]
        public void InitTest() {
            var f = new IOCFactory();
            Assert.AreEqual(InitOptionEnum.BuildContainer,f.Option.InitOption);
            f.Init();
            Assert.IsNull(f.Builder);
            Assert.IsNotNull(f.Container);
            var c = f.Container.Resolve<ITestClass>();
            Assert.IsNotNull(c);
            f.Dispose();
        }

        [TestMethod()]
        public void DisposeTest() {
            var f = new IOCFactory();
            f.Init();
            Assert.IsNotNull(f.Container);
            f.Dispose();
            Assert.IsNull(f.Container);
        }
    }
}