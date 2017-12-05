using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommonClass.ModelSql
{
    /// <summary>
    /// 数据库上下文方法扩展
    /// </summary>
    public static class DataExtend
    {
        /// <summary>
        /// 通过模型方式调用存储过程
        /// </summary>
        /// <typeparam name="TP">模型的类型</typeparam>
        /// <param name="db">数据库上下文</param>
        /// <param name="data">传参</param>
        public static int RunSql<TP>(this DbContext db,TP data) {
            var parms = getParams(data);
            var sql = getSql(getProcuderName<TP>(),parms);
            return db.Database.ExecuteSqlCommand(sql,parms.ToArray());
        }

        /// <summary>
        /// 通过模型方式调用存储过程
        /// </summary>
        /// <typeparam name="TP">参数模型类型</typeparam>
        /// <typeparam name="TR">返回的枚举单元类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="data">参数数据</param>
        /// <returns>存储过程响应枚举</returns>
        public static IEnumerable<TR> RunSql<TP, TR>(this DbContext db,TP data) {
            var parms = getParams(data);
            var sql = getSql(getProcuderName<TP>(),parms);
            return db.Database.SqlQuery<TR>(sql,parms.ToArray()).ToList();
        }

        /// <summary>
        /// 生成执行字符串部分。
        /// </summary>
        /// <param name="pName">存储过程名称</param>
        /// <param name="parms">存储过程参数</param>
        private static string getSql(string pName,IEnumerable<SqlParameter> parms) {
            var sb = new StringBuilder();
            sb.Append(pName + " ");
            if(parms != null || parms.Count() > 0) {
                foreach(var p in parms) {
                    sb.Append($"{p.ParameterName},");
                }
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 获取存储过程参数枚举
        /// </summary>
        /// <typeparam name="T">参数模型类型</typeparam>
        /// <param name="data">参数实例</param>
        private static IEnumerable<SqlParameter> getParams<T>(T data) {
            if(data == null) yield break;
            foreach(var p in typeof(T).GetProperties()) {
                if(!p.CanRead) continue;
                if(ignoreProp(p)) continue;
                yield return new SqlParameter("@" + getName(p),p.GetValue(data));
            }
        }

        /// <summary>
        /// 是否忽略属性
        /// </summary>
        /// <param name="p">要检查的属性</param>
        private static bool ignoreProp(PropertyInfo p) {
            return p.GetCustomAttribute<IgnoreAttribute>(true) != null;
        }

        /// <summary>
        /// 获取存储过程参数名称
        /// </summary>
        /// <param name="p">对应的属性</param>
        private static string getName(PropertyInfo p) {
            var np = p.GetCustomAttribute<PrarmNameAttribute>(true);
            if(np != null && np is PrarmNameAttribute na) {
                return na.Name;
            }
            return p.Name;
        }
        /// <summary>
        /// 获取存储过程名
        /// </summary>
        /// <typeparam name="T">参数模型</typeparam>
        private static string getProcuderName<T>() {
            var attr = typeof(T).GetCustomAttribute<ProcuderNameAttribute>(true);
            if(attr != null && attr is ProcuderNameAttribute pna) {
                return pna.ProcuderName;
            }
            return typeof(T).Name;
        }
    }
}
