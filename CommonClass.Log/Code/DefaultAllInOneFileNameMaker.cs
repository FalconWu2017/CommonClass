using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass.Log
{
    public class DefaultAllInOneFileNameMaker : ILogFileNameMaker
    {
        #region ILogFileNameMaker 成员

        public string MakeFileName(LogContext context) {
            return "AIO" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        }

        #endregion
    }
}