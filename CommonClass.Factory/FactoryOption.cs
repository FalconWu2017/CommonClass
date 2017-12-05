using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Factory
{
    /// <summary>
    /// IOC工厂操作选项
    /// </summary>
    public class FactoryOption
    {
        /// <summary>
        /// 初始化选项
        /// </summary>
        public InitOptionEnum InitOption { get; set; } = InitOptionEnum.BuildContainer;
        /// <summary>
        /// 默认使用注册调用方程序集
        /// </summary>
        public bool DefaultUseCallingAssembly { get; set; } = true;
    }
    /// <summary>
    /// IOC工厂初始化选项
    /// </summary>
    [Flags]
    public enum InitOptionEnum
    {
        /// <summary>
        /// 只创建容器生成器
        /// </summary>
        BuildContainerBuilder = 1,
        /// <summary>
        /// 创建容器
        /// </summary>
        BuildContainer = 2,
        /// <summary>
        /// 创建容器生成器和容器
        /// </summary>
        All = BuildContainerBuilder | BuildContainer
    }
}
