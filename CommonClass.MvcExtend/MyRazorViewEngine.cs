using System.Linq;
using System.Web.Mvc;
using System;

namespace CommonClass.MvcExtend
{
    /// <summary>
    /// 重写MVC查找视图的方法。
    /// </summary>
    [Obsolete("各MVC项目应该自己重写视图引擎，这个类只是例子",true)]
    public class MyRazorViewEngine:RazorViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext,string partialViewName,bool useCache) {
            var ns = controllerContext.GetType().Namespace;
            this.PartialViewLocationFormats.ToList().Add($"~/Views/{namespacePathStr(controllerContext)}/{1}/{0}.cshtml");
            return base.FindPartialView(controllerContext,partialViewName,useCache);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext,string viewName,string masterName,bool useCache) {
            this.ViewLocationFormats.ToList().Add($"~/Views/{namespacePathStr(controllerContext)}/{1}/{0}.cshtml");
            return base.FindView(controllerContext,viewName,masterName,useCache);
        }

        private string namespacePathStr(ControllerContext controllerContext) {
            //获取控制器名字空间
            var ns = controllerContext.GetType().Namespace;
            //去掉Controllers以及前面的部分。并把名字空间分隔符.转换为/
            var nss = ns.Substring(ns.IndexOf("Controllers") + 11).Replace('.','/');
            return nss;
        }
    }
}
