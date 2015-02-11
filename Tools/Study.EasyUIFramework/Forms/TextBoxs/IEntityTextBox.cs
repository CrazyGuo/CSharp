namespace Study.EasyUIFramework.Forms.TextBoxs
{
    /// <summary>
    /// 实体文本框
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IEntityTextBox<TEntity, TProperty> : ITextBox<IEntityTextBox<TEntity, TProperty>> {
    }
}
