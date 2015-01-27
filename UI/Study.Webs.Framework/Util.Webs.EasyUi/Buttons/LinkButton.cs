namespace Util.Webs.EasyUi.Buttons 
{
    /// <summary>
    /// 链接按钮
    /// </summary>
    public class LinkButton : LinkButton<ILinkButton>, ILinkButton 
    {
        /// <summary>
        /// 初始化链接按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        public LinkButton( string text )
            : base( text ) 
        {
        }
    }
}
