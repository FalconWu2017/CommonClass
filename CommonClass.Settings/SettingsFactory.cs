using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonClass.Settings
{
    /// <summary>
    /// 配置提供器工厂。
    /// </summary>
    public static class SettingsFactory
    {
        public static DefaultSetting StaticObj { get; set; } = null;

        /// <summary>
        /// 创建配置提供器
        /// </summary>
        /// <returns></returns>
        public static DefaultSetting Instantiates() {
            if(StaticObj == null) {
                StaticObj = new DefaultSetting();
            }
            return StaticObj;
        }
    }
}
