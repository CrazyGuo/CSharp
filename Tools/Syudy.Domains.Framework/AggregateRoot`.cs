namespace Study.Domains.Framework 
{
    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class AggregateRoot<TKey> : EntityBase<TKey>, IAggregateRoot<TKey> 
    {
        /// <summary>
        /// 初始化聚合根
        /// </summary>
        /// <param name="id">标识</param>
        protected AggregateRoot( TKey id )
            : base( id ) 
        {
        }

        /// <summary>
        /// 版本号(乐观锁)
        /// </summary>
        public byte[] Version { get; set; }
    }
}
