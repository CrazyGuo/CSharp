using System;
using System.ComponentModel.DataAnnotations;
using Study.Util.Validations;

namespace Study.Domains.Framework 
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public abstract class AggregateRoot : AggregateRoot<Guid> 
    {
        /// <summary>
        /// 初始化聚合根
        /// </summary>
        /// <param name="id">标识</param>
        protected AggregateRoot( Guid id ) 
            : base( id ){
        }

        /// <summary>
        /// 验证
        /// </summary>
        protected override void Validate( ValidationResultCollection results ) 
        {
            if ( Id == Guid.Empty )
                results.Add( new ValidationResult( "Id不能为空" ) );
        }
    }
}
