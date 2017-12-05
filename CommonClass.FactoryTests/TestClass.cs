using CommonClass.Factory;

namespace CommonClass.FactoryTests
{
    interface ITestClass { }

    class TestClass:ITestClass, IRegisterBaseInterface
    {
        public int c { get; set; } = 0;
    }
}
