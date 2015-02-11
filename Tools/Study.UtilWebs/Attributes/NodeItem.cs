namespace Study.UtilWebs.Attributes 
{
    /// <summary>
    /// 属性节点项目
    /// </summary>
    internal sealed class NodeItem 
    {
        /// <summary>
        /// 初始化属性节点项目
        /// </summary>
        /// <param name="value">值</param>
        public NodeItem( string value ) 
        {
            Value = value;
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; private set; }
    }
}
