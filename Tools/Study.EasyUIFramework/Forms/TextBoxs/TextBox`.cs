using System.Collections.Generic;
using System.Text;
using Study.Util;
using Study.UtilWebs;
using Study.EasyUIFramework.Base;

namespace Study.EasyUIFramework.Forms.TextBoxs
{
    /// <summary>
    /// 文本框
    /// </summary>
    /// <typeparam name="T">文本框类型</typeparam>
    public abstract class TextBox<T> : ComponentBase<T>, ITextBox<T> where T : ITextBox<T> {
        /// <summary>
        /// 初始化文本框
        /// </summary>
        protected TextBox() {
            AddClass( "easyui-textbox" );
            _validTypes = new List<string>();
        }

        /// <summary>
        /// 验证类型
        /// </summary>
        private readonly List<string> _validTypes;

        /// <summary>
        /// 设置name属性
        /// </summary>
        /// <param name="name">名称</param>
        public T Name( string name ) {
            AddAttribute( "name", name );
            return This();
        }

        /// <summary>
        /// 设置提示消息，该消息显示在文本框中
        /// </summary>
        /// <param name="text">提示消息文本</param>
        public T Prompt( string text ) {
            AddDataOption( "prompt", GetValue( text ) );
            return This();
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        public T Value( string value ) {
            AddDataOption( "value", GetValue( value ) );
            return This();
        }

        /// <summary>
        /// 设置为密码框
        /// </summary>
        public T Password() {
            AddDataOption( "type", GetValue( "password" ) );
            return This();
        }

        /// <summary>
        /// 设置为多行文本框
        /// </summary>
        /// <param name="width">文本框宽度</param>
        /// <param name="height">文本框高度</param>
        public T MultiLine( int width, int height ) {
            AddDataOption( "multiline", true ).Width( width ).Height( height );
            return This();
        }

        /// <summary>
        /// 禁用文本框
        /// </summary>
        /// <param name="disabled">true为禁用</param>
        public T Disable( bool disabled = true ) {
            AddDataOption( "disabled", disabled );
            return This();
        }

        /// <summary>
        /// 设置文本框为只读
        /// </summary>
        /// <param name="readOnly">true为只读</param>
        public T ReadOnly( bool readOnly = true ) {
            AddDataOption( "readonly", readOnly );
            return This();
        }

        /// <summary>
        /// 设置文本框为可编辑
        /// </summary>
        /// <param name="editable">true为可编辑</param>
        public T Editable( bool editable = true ) {
            AddDataOption( "editable", editable );
            return This();
        }

        /// <summary>
        /// 设置文本框图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        /// <param name="width">图标宽度，默认18</param>
        /// <param name="align">图标对齐方式，默认右对齐</param>
        public T Icon( string iconClass, int width = 18, AlignLeftRigth align = AlignLeftRigth.Right ) {
            AddDataOption( "iconCls", GetValue( iconClass ) )
                .AddDataOption( "iconWidth", width.ToString() )
                .AddDataOption( "iconAlign", GetValue( align.Description() ) );
            return This();
        }

        /// <summary>
        /// 设置文本框按钮
        /// </summary>
        /// <param name="iconClass">文本框按钮图标class</param>
        /// <param name="clickCallback">单击回调函数</param>
        /// <param name="text">按钮文本</param>
        /// <param name="align">按钮对齐方式,默认右对齐</param>
        public T Button( string iconClass, string clickCallback, string text = "", AlignLeftRigth align = AlignLeftRigth.Right ) {
            AddDataOption( "buttonIcon", GetValue( iconClass ) )
                .AddDataOption( "onClickButton", clickCallback )
                .AddDataOption( "buttonText", GetValue( text ) )
                .AddDataOption( "buttonAlign", GetValue( align.Description() ) );
            return This();
        }

        /// <summary>
        /// 设置文本框文本改变事件处理
        /// </summary>
        /// <param name="callback">文本改变回调函数，只设置函数名，范例：func</param>
        public T OnChange( string callback ) {
            AddDataOption( "onChange", callback );
            return This();
        }

        /// <summary>
        /// 设置延迟验证的时间
        /// </summary>
        /// <param name="time">延迟验证的时间，单位：毫秒</param>
        public T Delay( int time ) {
            AddDataOption( "delay", time.ToString() );
            return This();
        }

        /// <summary>
        /// 设置提示位置
        /// </summary>
        /// <param name="align">提示位置</param>
        public T TipPosition( AlignLeftRigth align ) {
            AddDataOption( "tipPosition", GetValue( align.Description() ) );
            return This();
        }

        /// <summary>
        /// 设置关闭验证
        /// </summary>
        public T NoValidate() {
            AddDataOption( "novalidate", true );
            return This();
        }

        /// <summary>
        /// 设置文本框为必填项
        /// </summary>
        /// <param name="isRequired">true为必填项</param>
        public T Required( bool isRequired = true ) {
            AddDataOption( "required", isRequired );
            return This();
        }

        /// <summary>
        /// 设置文本框为必填项
        /// </summary>
        /// <param name="message">验证失败消息</param>
        public T Required( string message ) {
            Required().AddDataOption( "missingMessage", GetValue( message ) );
            return This();
        }

        /// <summary>
        /// 设置Email验证
        /// </summary>
        public T Email() {
            AddValidation( "email" );
            return This();
        }

        /// <summary>
        /// 添加验证条件
        /// </summary>
        private void AddValidation( string validation, params object[] args ) {
            if ( validation.IsEmpty() )
                return;
            _validTypes.Add( GetValue( string.Format( validation, args ) ) );
        }

        /// <summary>
        /// 设置Url验证
        /// </summary>
        public T Url() {
            AddValidation( "url" );
            return This();
        }

        /// <summary>
        /// 设置长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public T Length( int minLength, int maxLength ) {
            AddValidation( "length[{0},{1}]", minLength, maxLength );
            return This();
        }

        /// <summary>
        /// 设置最小长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        public T MinLength( int minLength ) {
            AddValidation( "minLength[{0}]", minLength );
            return This();
        }

        /// <summary>
        /// 设置最大长度验证
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public T MaxLength( int maxLength ) {
            AddValidation( "maxLength[{0}]", maxLength );
            return This();
        }

        /// <summary>
        /// 设置远程验证
        /// </summary>
        /// <param name="url">远程url</param>
        /// <param name="parameterName">参数名</param>
        public T Remote( string url, string parameterName ) {
            AddValidation( "remote[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, url, parameterName );
            return This();
        }

        /// <summary>
        /// 设置相等验证
        /// </summary>
        /// <param name="targetId">目标元素Id</param>
        /// <param name="message">消息</param>
        public T EqualTo( string targetId, string message = "" ) {
            AddValidation( "equalTo[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, targetId, message );
            return This();
        }

        /// <summary>
        /// 设置最大值验证
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="message">消息</param>
        public T Max( double maxValue, string message = "" ) {
            AddValidation( "max[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, maxValue, message );
            return This();
        }

        /// <summary>
        /// 设置最小值验证
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="message">消息</param>
        public T Min( double minValue, string message = "" ) {
            AddValidation( "min[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, minValue, message );
            return This();
        }

        /// <summary>
        /// 设置数值范围验证
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="message">消息</param>
        public T Range( double min, double max, string message = "" ) {
            AddValidation( "range[{0}{1}{0},{0}{2}{0},{0}{3}{0}]", HtmlEscape.Quote, min, max, message );
            return This();
        }

        /// <summary>
        /// 显示日期框
        /// </summary>
        public T Date() {
            UpdateClass( "easyui-datebox" ).Editable( false );
            return This();
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            CreateValidations();
            var result = new StringBuilder();
            result.AppendFormat( "<input {0}/>", GetOptions() );
            return result.ToString();
        }

        /// <summary>
        /// 创建验证
        /// </summary>
        private void CreateValidations() {
            if ( _validTypes.Count == 0 )
                return;
            AddDataOption( "validType", GetValidations() );
        }

        /// <summary>
        /// 获取验证规则
        /// </summary>
        private string GetValidations() {
            if ( _validTypes.Count == 1 )
                return _validTypes[0];
            return string.Format( "[{0}]", _validTypes.Splice() );
        }
    }
}
