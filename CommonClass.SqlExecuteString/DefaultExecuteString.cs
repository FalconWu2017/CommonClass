using System.Linq;
using System.Reflection;
using System.Text;
using CommonClass.Factory;

namespace CommonClass.SqlExecuteString
{
    /// <summary>
    /// 默认的执行字符串生成类
    /// </summary>
    public class DefaultExecuteString:IExecuteString, IRegisterBaseInterface
    {
        #region IExceString 成员

        public string GetString<T>(T model) {
            return GetString<T>(getProcuderName<T>(), model);
        }

        public string GetString<T>(string ProcederStoreName, T model) {
            if(ProcederStoreName == null) throw new ProcuderNameNullException();
            var sql=new StringBuilder();
            sql.AppendFormat("exec {0} ", ProcederStoreName);

            if(model == null) return sql.ToString();
            foreach(var pro in typeof(T).GetProperties()) {
                if(pro.CanRead && !ProcuderPaIgnore(pro)) {
                    string paName=getProcuderParmName(pro);
                    object paValue=pro.GetValue(model, null);
                    if(paValue == null) continue;
                    if(pro.PropertyType.IsValueType) {
                        sql.AppendFormat("@{0}={1},", paName, paValue.ToString());
                    }
                    else {
                        sql.AppendFormat("@{0}='{1}',", paName, paValue.ToString());
                    }
                }
            }
            return sql.ToString().TrimEnd(',');
        }

        #endregion

        /// <summary>
        /// 获取属性的存储过程参数名称
        /// </summary>
        protected virtual string getProcuderParmName(PropertyInfo pi) {
            var attr=pi.GetCustomAttributes(typeof(ProcuderParmNameAttribute), true);
            if(attr.Any()) {
                var a=attr.Last() as ProcuderParmNameAttribute;
                if(a != null) {
                    return a.Name;
                }
            }
            return pi.Name;
        }

        /// <summary>
        /// 确定属性是否不生成对应的存储过程参数
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        protected virtual bool ProcuderPaIgnore(PropertyInfo pi) {
            var attr=pi.GetCustomAttributes(typeof(ProcuderParmIgnoreAttribute), true);
            return attr.Any();
        }

        /// <summary>
        /// 获取存储过程名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual string getProcuderName<T>() {
            var type=typeof(T);
            var attr=type.GetCustomAttributes(typeof(ProcuderNameAttribute), true);
            return attr.Any() ? ((ProcuderNameAttribute)attr.Last()).Name : type.Name;
        }

    }
}
