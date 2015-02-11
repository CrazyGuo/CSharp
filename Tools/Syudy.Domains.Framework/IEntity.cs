namespace Study.Domains.Framework
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IEntity 
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
        /// <summary>
        /// 验证
        /// </summary>
        void Validate();
    }

    /// <summary>
    /// 实体
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IEntity<out TKey> : IEntity 
    {
        /// <summary>
        /// 标识
        /// </summary>
        TKey Id { get; }
    }
}
