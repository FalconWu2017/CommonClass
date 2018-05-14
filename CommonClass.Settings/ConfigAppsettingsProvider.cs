using System.Configuration;

namespace CommonClass.Settings
{
    /// <summary>
    /// 可以从web程序的Web.config文件或窗体应用程序的Appsettings节中获取值。实现配置提供器工厂接口和配置提供器接口。
    /// </summary>
    public class ConfigurationAppSettingsProvider:IProviderFactory, ISettingProvider
    {
        public static ConfigurationAppSettingsProvider StaticObj { get; set; }

        public string GetValue(string key) {
            var value = ConfigurationManager.AppSettings[key];
            return value;
        }

        public ISettingProvider Instantiates() {
            if(StaticObj == null) {
                StaticObj = new ConfigurationAppSettingsProvider();
            }
            return StaticObj;
        }
    }
}
