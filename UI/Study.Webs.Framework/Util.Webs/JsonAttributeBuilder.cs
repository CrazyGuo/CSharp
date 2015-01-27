namespace Util.Webs 
{
    /// <summary>
    /// Json属性生成器
    /// </summary>
    public class JsonAttributeBuilder 
    {
        /// <summary>
        /// Json属性生成器
        /// </summary>
        public JsonAttributeBuilder()
        {
            _builder = new AttributeBuilder( ":","," );
        }

        /// <summary>
        /// 属性生成器
        /// </summary>
        private readonly AttributeBuilder _builder;

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        /// <param name="quotes">属性值引号</param>
        public void Add( string name, string value,string quotes = "" ) 
        {
            if ( value.IsEmpty() )
                return;
            _builder.Update( name, value, ",", quotes );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() 
        {
            return _builder.GetResult();
        }
    }
}
