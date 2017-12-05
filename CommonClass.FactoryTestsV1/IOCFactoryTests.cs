using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonClass.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClass.FactoryTests;
using Autofac;

namespace CommonClass.Factory.Tests
{
    [TestClass()]
    public class IOCFactoryTestsV1
    {
        [TestMethod()]
        public void IOCFactoryTestV1() {
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
            var o = f.Container.Resolve<ITestClass>();
            if(o is TestClass oc) {
                Assert.AreEqual(1,oc.Count);
            }
            f.Dispose();
            var ooo = new TestClass();
            Assert.AreEqual(1,ooo.Count);
        }

        [TestMethod]
        public void EventTest() {
            //BeforeBuild测试
            using(var f = new IOCFactory()) {
                f.Init();
                Assert.IsFalse(f.Container.IsRegistered<ITest2>());
            }
            using(var f = new IOCFactory()) {
                f.BeforeBuild += (fa,c) => {
                    c.Register(m => new TestClass2()).As<ITest2>();
                };
                f.Init();
                Assert.IsTrue(f.Container.IsRegistered<ITest2>());
            }
        }

        [TestMethod]
        public void AssemblyRegisterTest() {
            using(var f=new IOCFactory("CommonClass.FactoryTestHelperAssembly")) {
                f.Init();
                Assert.IsTrue(f.Container.IsRegistered<FactoryTestHelperAssembly.II1>(),"检测IC1注册失败");
            }
        }
    }
}