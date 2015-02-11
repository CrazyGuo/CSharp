using Study.UtilWebs;

namespace Study.EasyUIFramework 
{
    /// <summary>
    /// EasyUi属性生成器
    /// </summary>
    public class EasyUiAttributeBuilder : AttributeBuilder 
    {
        /// <summary>
        /// 初始化EasyUi属性生成器
        /// </summary>
        public EasyUiAttributeBuilder() 
        {
            _dataOptionBuilder = new AttributeBuilder( ":", "," );
        }

        /// <summary>
        /// data-options属性生成器
        /// </summary>
        private readonly AttributeBuilder _dataOptionBuilder;

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public void AddDataOption( string name, string value ) 
        {
            _dataOptionBuilder.Update( name,value,"","" );
            Update( "data-options", _dataOptionBuilder.GetResult() );
        }
    }
}
