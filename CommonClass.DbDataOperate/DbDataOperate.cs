using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CommonClass.Factory;

namespace CommonClass.DbDataOperate
{
    public class DbDataOperate:IDbDataOperate, IDisposable
    {
        public DbContext Db { get; set; }

        public DbDataOperate(DbContext db) {
            this.Db = db;
        }

        public void Dispose() {
            this.Db?.Dispose();
        }

        public virtual void Insert<T>(T model) where T : class {
            this.Db.Entry(model).State = EntityState.Added;
            this.Db.SaveChanges();
        }

        public virtual void Remove<T>(T model) where T : class {
            this.Db.Entry(model).State = EntityState.Deleted;
            this.Db.SaveChanges();
        }

        public virtual void Updata<T>(T model) where T : class {
            this.Db.Entry(model).State = EntityState.Modified;
            this.Db.SaveChanges();
        }

        public virtual void Merge<T>(T model,Func<T,bool> filter) where T : class {
            if(Exists(filter)) {
                this.Updata(model);
            }
            else {
                this.Insert(model);
            }
        }
        /// <summary>
        /// 数据库是否存在该实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public virtual bool Exists<T>(Func<T,bool> filter) where T : class {
            return this.Db.Set<T>().Where(filter).Any();
        }
        /// <summary>
        /// 查找并返回该实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual T Find<T>(params object[] keys) where T : class {
            return this.Db.Set<T>().Find(keys);
        }

        public virtual IQueryable<T> Query<T>() where T : class {
            var result = this.Db.Set<T>() as IQueryable<T>;
            return result;
        }

        public virtual IEnumerable<TR> Select<T, TR>(Expression<Func<IQueryable<T>,IQueryable<TR>>> filter) where T : class {
            return filter.Compile()(Query<T>()).ToList();
        }

        /// <summary>
        /// 获取主键属性列表。首先查询KeyAttribute标记属性，如没有查找Id属性，然后查找类名+id属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetKeys<T>() {
            var t = typeof(T);
            var pros = t.GetProperties();
            IEnumerable<PropertyInfo> ps = null;
            //检查是否定义Key特性。如果有返回按order特性定义键顺序
            ps = pros
               .Where(m => m.GetCustomAttribute<KeyAttribute>() != null)
               .OrderBy(m => m.GetCustomAttribute<ColumnAttribute>() == null ? 0 : m.GetCustomAttribute<ColumnAttribute>().Order);
            //查找是否有id属性
            if(ps == null || ps.Count() == 0) {
                ps = pros.Where(m => m.Name.ToLower() == "id");
            }
            //查找是否具有类名加id属性
            if(ps == null || ps.Count() == 0) {
                ps = pros.Where(m => m.Name.ToLower() == t.Name.ToLower() + "id");
            }
            return ps ?? new List<PropertyInfo>();
        }

        public static IEnumerable<object> GetKeyValues<T>(T model) {
            return GetKeys<T>().Select(m => m.GetValue(model,null));
        }
    }
}
