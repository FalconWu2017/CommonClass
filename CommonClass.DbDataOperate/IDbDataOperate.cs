using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommonClass.DbDataOperate
{
    /// <summary>
    /// 数据库表操作
    /// </summary>
    public interface IDbDataOperate
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="model">要插入的数据</param>
        void Insert<T>(T model) where T : class;

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="model">数据对象</param>
        void Remove<T>(T model) where T : class;

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="model">数据对象</param>
        void Updata<T>(T model) where T : class;

        /// <summary>
        /// 合并数据，找到匹配则更新，否则插入新行
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="model">数据对象</param>
        /// <param name="filter">查找匹配数据的条件，一般为主键匹配</param>
        void Merge<T>(T model,Func<T,bool> filter) where T : class;

        /// <summary>
        /// 判断是否存在数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="filter">查找的匹配条件</param>
        bool Exists<T>(Func<T,bool> filter) where T : class;

        /// <summary>
        /// 查找并返回数据。按键的顺序给定值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        T Find<T>(params object[] keys) where T : class;

        IEnumerable<TR> Select<T, TR>(Expression<Func<IQueryable<T>,IQueryable<TR>>> filter) where T : class;

        IQueryable<T> Query<T>() where T : class;
    }
}
