using CommonClass.Factory;
using System;

namespace CommonClass.FactoryTests
{
    interface ITestClass { }
    class TestClass:ITestClass, IRegisterBaseInterface, IDisposable
    {
        public int Count { get; set; } = 0;

        public TestClass() {
            Count += 1;
        }
        public int c { get; set; } = 0;

        public void Dispose() {
            Count -= 1;
        }
    }

    interface ITest2 { }
    public class TestClass2:ITest2 { }
}
