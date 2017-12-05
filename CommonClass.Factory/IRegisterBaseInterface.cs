/*
 * IOC工厂类：
 * 程序可以通过本工厂的Container属性生成对象。
 * 要通过工厂生成的对象必须先注册。
 * 注册对象可以为对象添加IocRegisterAttribute特性，该特性也提供一个RegisterOption选项，可以控制对象以何种方式生成。
 * 另外还可以为对象实现IRegisterBaseInterface，IRegisterSingleBase或IRegisterSelf接口实现注册。
 * 以上方案均为自动注册方案，也可以实现IRegister来进行手动注册。
 * 注册后通过Container.BeginLifetimeScope()方法生成工厂生命周期对象，并且在生命周期结束后调用IDisposable释放资源，
 * 如果不释放资源，工厂会一直监视对象，这样GC将不会释放资源，并最终导致系统资源耗尽而崩溃。
 * 如果对象需要释放非托管资源可以实现IDisposable接口，工厂生命周期对象释放的时候会调用所有监视对象调用IDisposable方法释放资源。
 * */


namespace CommonClass.Factory
{
    /// <summary>
    /// 实现该接口表示类型要注册到其实现的基础接口。
    /// </summary>
    public interface IRegisterBaseInterface { }
}