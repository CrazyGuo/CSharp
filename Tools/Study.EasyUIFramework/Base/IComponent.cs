namespace Study.EasyUIFramework.Base 
{
    /// <summary>
    /// 组件
    /// </summary>
    public interface IComponent<out T> : IOption<T> where T : IComponent<T> 
    {
        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        T Id( string id );
        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        T Width( int? width );
        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        T Height( int height );
    }
}
