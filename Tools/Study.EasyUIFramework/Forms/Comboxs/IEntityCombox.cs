namespace Study.EasyUIFramework.Forms.Comboxs 
{
    /// <summary>
    /// 实体组合框
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IEntityCombox<TEntity, TProperty> : ICombox<IEntityCombox<TEntity, TProperty>> 
    {
    }
}
