
namespace Util.Webs.EasyUi.Base 
{
    /// <summary>
    /// 基选项
    /// </summary>
    public abstract class OptionBase<T> : IOption<T> where T : IOption<T> 
    {
        /// <summary>
        /// 初始化基选项
        /// </summary>
        protected OptionBase() 
        {
            _builder = new EasyUiAttributeBuilder();
        }

        /// <summary>
        /// 属性生成器
        /// </summary>
        private readonly EasyUiAttributeBuilder _builder;

        /// <summary>
        /// 输出
        /// </summary>
        public override string ToString() 
        {
            return GetOptions();
        }

        /// <summary>
        /// 输出
        /// </summary>
        public string ToHtmlString() 
        {
            return ToString();
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public T AddAttribute( string name, string value ) 
        {
            _builder.Add( name, value );
            return This();
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name">样式名称</param>
        /// <param name="value">样式值</param>
        public T AddStyle( string name, string value ) 
        {
            _builder.AddStyle( name, value );
            return This();
        }

        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性</param>
        public T AddClass( string @class ) 
        {
            _builder.AddClass( @class );
            return This();
        }

        /// <summary>
        /// 更新class属性
        /// </summary>
        /// <param name="class">class属性</param>
        protected T UpdateClass( string @class ) 
        {
            _builder.UpdateClass( @class );
            return This();
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public T AddDataOption( string name, string value ) 
        {
            if ( value.IsEmpty() )
                return This();
            _builder.AddDataOption( name, value );
            return This();
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public T AddDataOption( string name, bool value ) 
        {
            _builder.AddDataOption( name, GetValue( value ) );
            return This();
        }

        /// <summary>
        /// 返回组件
        /// </summary>
        protected T This() 
        {
            return (T)( (object)this );
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        protected string GetOptions() 
        {
            return _builder.GetResult();
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="value">字符串值</param>
        protected string GetValue( string value ) 
        {
            if ( value.IsEmpty() )
                return string.Empty;
            return string.Format( "'{0}'", value );
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="value">布尔值</param>
        protected string GetValue( bool value ) 
        {
            return value.ToString().ToLower();
        }
    }
}
