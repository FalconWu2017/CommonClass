namespace CommonClass.Compress
{
    /// <summary>
    /// 数据压缩接口
    /// </summary>
    public interface ICompress
    {
        /// <summary>
        /// 压缩传入数据
        /// </summary>
        /// <param name="inPut">传入数据</param>
        /// <param name="compressOption">压缩选项</param>
        /// <returns></returns>
        ConnmpressResult<T> Compress<T>(T inPut,string compressType);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inPut"></param>
        /// <exception cref="System.Exception" >ceshi </exception>
        /// <returns></returns>
        ConnmpressResult<T> UnCompress<T>(T inPut);
    }
}
