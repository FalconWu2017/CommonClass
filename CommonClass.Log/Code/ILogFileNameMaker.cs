
namespace CommonClass.Log
{
    /// <summary>
    /// 获取存储日志文件的文件名
    /// </summary>
    public interface ILogFileNameMaker
    {
        /// <summary>
        /// 创建保存记录的文件名，不含路径
        /// </summary>
        /// <param name="context">日志记录上下文</param>
        /// <returns>完整的文件名,不包含路径</returns>
        string MakeFileName(LogContext context);
    }
}
