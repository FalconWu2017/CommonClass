﻿/*
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

using System;

namespace CommonClass.Factory
{
    /// <summary>
    /// 注册选项
    /// </summary>
    [Flags]
    public enum RegisterOption
    {
        /// <summary>
        /// 注册到基础接口
        /// </summary>
        BaseInterface = 1,
        /// <summary>
        /// 注册到自身
        /// </summary>
        Self = 2,
        /// <summary>
        /// 注册为单例模式
        /// </summary>
        Single = 4,
        /// <summary>
        /// 需要支持属性注入
        /// </summary>
        PropertyDI = 8,

        /// <summary>
        /// 默认。注册到基础接口
        /// </summary>
        Default = BaseInterface,
        /// <summary>
        /// 注册到基础接口并实现单例
        /// </summary>
        BaseSingle = BaseInterface | Single,
        /// <summary>
        /// 标准注入。类将注册到其实现的接口，并且为单例模式和支持属性注入
        /// </summary>
        Standard = BaseInterface | Single | PropertyDI,
    }
}