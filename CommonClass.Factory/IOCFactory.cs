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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace CommonClass.Factory
{
    /// <summary>
    /// IOC工厂
    /// </summary>
    public class IOCFactory:IDisposable
    {
        #region 事件
        /// <summary>
        /// 在容器创建之前，类型注册之前发送的事件
        /// </summary>
        public event Action<IOCFactory,ContainerBuilder> BeforeRegister;
        /// <summary>
        /// 在容器创建之前，所有类型注册之后发送的事件
        /// </summary>
        public event Action<IOCFactory,ContainerBuilder> BeforeBuild;
        /// <summary>
        /// 在容器生成之后发生
        /// </summary>
        public event Action<IOCFactory,IContainer> OnBuild;
        /// <summary>
        /// 销毁容器之前发送的事件
        /// </summary>
        public event Action<IOCFactory,IContainer> BeforeDisposeContainer;
        /// <summary>
        /// 容器销毁完成
        /// </summary>
        public event Action<IOCFactory> OnDisposeContainer;

        #endregion

        #region 属性
        /// <summary>
        /// 容器创建器
        /// </summary>
        public ContainerBuilder Builder { get; set; } = null;
        /// <summary>
        /// 注册容器
        /// </summary>
        public IContainer Container { get; set; } = null;
        /// <summary>
        /// 注册的程序集范围
        /// </summary>
        public Assembly[] Assemblies { get; private set; } = new Assembly[] { };
        /// <summary>
        /// IOC工厂选项
        /// </summary>
        public FactoryOption Option { get; set; } = new FactoryOption();

        #endregion

        #region 构造
        /// <summary>
        /// 只在调用方程序集中寻找注册类型
        /// </summary>
        public IOCFactory() { }
        /// <summary>
        /// 在给定的程序集中寻找注册
        /// </summary>
        /// <param name="assemblies">指定要寻找的程序集</param>
        public IOCFactory(params Assembly[] assemblies) {
            this.Assemblies = assemblies;
        }
        /// <summary>
        /// 通过提供程序集注册其中类型
        /// </summary>
        public IOCFactory(params string[] assemblyNames) {
            this.Assemblies = ToAssenbly(assemblyNames);
        }
        /// <summary>
        /// 提供初始化操作选项和，程序集构造IOC工厂
        /// </summary>
        /// <param name="option">工厂选项</param>
        /// <param name="assemblyNames">程序集名称</param>
        public IOCFactory(FactoryOption option,params string[] assemblyNames) {
            this.Option = option;
            this.Assemblies = ToAssenbly(assemblyNames);
        }
        /// <summary>
        /// 通过提供工厂选项和程序集构造IOC工厂
        /// </summary>
        /// <param name="option">工厂选项</param>
        /// <param name="assemblies">程序集</param>
        public IOCFactory(FactoryOption option,params Assembly[] assemblies) {
            this.Option = option;
            this.Assemblies = assemblies;
        }

        #endregion

        /// <summary>
        /// 初始化类型容器
        /// </summary>
        public virtual void Init() {
            if(this.Option == null) {
                throw new System.ArgumentNullException("IOCFactory.Option");
            }
            //初始化默认调用方程序集
            if((this.Assemblies == null || this.Assemblies.Length == 0) && this.Option.DefaultUseCallingAssembly) {
                this.Assemblies = new Assembly[] { Assembly.GetCallingAssembly() };
            }
            //初始化容器创建器
            var bui = new ContainerBuilder();
            this.BeforeRegister?.Invoke(this,bui);

            foreach(var ass in this.Assemblies) {
                RegisterByInterface(bui,ass);
                RegisterByAttribute(bui,ass);
                RegisterByIRegister(bui,ass);
            }

            //注册配置
            //bui.RegisterModule(new ConfigurationSettingsReader("autofac"));

            if(this.Option.InitOption.HasFlag(InitOptionEnum.BuildContainerBuilder)) {
                this.Builder = bui;
            }
            if(this.Option.InitOption.HasFlag(InitOptionEnum.BuildContainer)) {
                DisposeContainer();
                //生成容器
                this.BeforeBuild?.Invoke(this,bui);
                this.Container = bui.Build();
                //发送生成事件
                this.OnBuild?.Invoke(this,this.Container);
            }
        }
        /// <summary>
        /// 销毁容器
        /// </summary>
        public virtual void DisposeContainer() {
            if(this.Container != null) {
                this.BeforeDisposeContainer?.Invoke(this,this.Container);
                this.Container.Dispose();
                this.Container = null;
                this.OnDisposeContainer?.Invoke(this);
            }
        }

        #region 静态帮助方法
        /// <summary>
        /// 将程序集名称集合转换为程序集集合
        /// </summary>
        /// <param name="assemblyNames">程序集名称集合</param>
        /// <returns>程序集集合</returns>
        public static Assembly[] ToAssenbly(params string[] assemblyNames) {
            var list = new List<Assembly>();
            for(var i = 0;i < assemblyNames.Length;i++) {
                list.Add(Assembly.Load(assemblyNames[i]));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 注册实现IRegister接口的类型
        /// </summary>
        public static void RegisterByIRegister(ContainerBuilder bui,Assembly ass) {
            foreach(var t in getTypes<IRegister>(ass)) {
                if(t.Assembly.CreateInstance(t.FullName) is IRegister obj) {
                    obj.Register(bui);
                }
            }
        }

        /// <summary>
        /// 通过接口实现注册
        /// </summary>
        public static void RegisterByInterface(ContainerBuilder bui,Assembly ass) {
            Type[] types;
            //注册实现IRegisterBaseInterface的类型。
            types = getTypes<IRegisterBaseInterface>(ass);
            bui.RegisterTypes(types).AsImplementedInterfaces();

            //注册实现ISelfRegister的类型。
            types = getTypes<IRegisterSelf>(ass);
            bui.RegisterTypes(types).AsSelf();

            //注册实现IRegisterSingleBase的类型。
            types = getTypes<IRegisterSingleBase>(ass);
            bui.RegisterTypes(types).AsImplementedInterfaces().SingleInstance();
        }

        /// <summary>
        /// 通过IocRegisterAttribute实现注册
        /// </summary>
        public static void RegisterByAttribute(ContainerBuilder bui,Assembly ass) {
            foreach(Type t in ass.GetTypes()) {
                //获取类型自定义属性IocRegisterAttribute
                var attr = t.GetCustomAttributes(typeof(IocRegisterAttribute),true);
                foreach(IocRegisterAttribute a in attr) {
                    //注册类型
                    var rb = bui.RegisterTypes(t);
                    //注册到基础接口
                    if(a.Option.HasFlag(RegisterOption.BaseInterface)) {
                        rb.AsImplementedInterfaces();
                    }
                    //注册到自身
                    if(a.Option.HasFlag(RegisterOption.Self)) {
                        rb.AsSelf();
                    }
                    //注册为单例模式
                    if(a.Option.HasFlag(RegisterOption.Single)) {
                        rb.SingleInstance();
                    }
                    //支持属性注入
                    if(a.Option.HasFlag(RegisterOption.PropertyDI)) {
                        rb.PropertiesAutowired();
                    }
                }
            }
        }

        /// <summary>
        /// 获取程序集实现了T类型的所有对象类型
        /// </summary>
        private static Type[] getTypes<T>(Assembly ass) {
            return ass.GetTypes().Where(m => typeof(T).IsAssignableFrom(m) && m.IsClass && !m.IsAbstract).ToArray();
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() {
            foreach(var p in this.GetType().GetProperties()) {
                var obj = p.GetValue(this);
                if(obj != null && obj is IDisposable o) {
                    o.Dispose();
                }
            }
        }

        #endregion
    }
}