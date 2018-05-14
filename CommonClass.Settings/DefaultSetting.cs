using System.Collections.Generic;

namespace CommonClass.Settings
{
    public class DefaultSetting:ISettings
    {
        public static List<IProviderFactory> Factorys { get; set; } = new List<IProviderFactory>();

        public void AddDefaultProviders() {
            AddProvider(new ConfigurationAppSettingsProvider());
        }

        public void AddProvider(params IProviderFactory[] pf) {
            if(Factorys != null) Factorys.AddRange(pf);
        }

        public string GetValue(string key) {
            if(Factorys == null || Factorys.Count == 0) {
                return null;
            }
            foreach(var f in Factorys) {
                var s = f.Instantiates();
                var se = s as ISettingEvent;
                if(se != null) se.BeforeGet(key);
                var result = s.GetValue(key);
                if(se != null) se.AfterGet(key,result);
                if(result != null) {
                    return result;
                }
            }
            return null;
        }
    }
}