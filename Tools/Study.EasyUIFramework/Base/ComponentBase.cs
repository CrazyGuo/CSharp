using Study.Util;

namespace Study.EasyUIFramework.Base 
{
    /// <summary>
    /// 基组件
    /// </summary>
    public abstract class ComponentBase<T> : OptionBase<T>, IComponent<T> where T : IComponent<T> 
    {

        #region Id(设置标识)

        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        public T Id( string id ) 
        {
            AddAttribute( "id", id );
            return This();
        }

        #endregion

        #region Width(设置宽度)

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        public T Width( int? width ) 
        {
            AddDataOption( "width", width.ToStr() );
            return This();
        }

        #endregion

        #region Height(设置高度)

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        public T Height( int height ) 
        {
            AddDataOption( "height", height.ToString() );
            return This();
        }

        #endregion

        #region ToString(输出)

        /// <summary>
        /// 输出
        /// </summary>
        public override string ToString() 
        {
            return GetResult();
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected abstract string GetResult();

        #endregion
    }
}
