namespace CommonClass.SqlExecuteString
{
    /// <summary>
    /// 生成数据库存储过程执行的sql字符串
    /// </summary>
    public interface IExecuteString
    {
        /// <summary>
        /// 根据模型生成执行存储过程字符串
        /// </summary>
        /// <typeparam name="T">参数模型类型</typeparam>
        /// <param name="model">模型</param>
        /// <returns>执行字符串</returns>
        string GetString<T>(T model);
        /// <summary>
        /// 根据模型生成执行存储过程字符串
        /// </summary>
        /// <typeparam name="T">参数模型类型</typeparam>
        /// <param name="ProcederStoreName">存储过程名称</param>
        /// <param name="model">模型</param>
        /// <returns>执行字符串</returns>
        string GetString<T>(string ProcederStoreName, T model);
    }
}
