using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonClass.Settings
{
    public interface ISettingEvent
    {
        void BeforeGet(string key);
        void AfterGet(string key,string Value);
    }
}
