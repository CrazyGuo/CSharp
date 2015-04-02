using System;
using System.ComponentModel.DataAnnotations;
using Study.Util.Validations;

namespace Study.Domains.Framework
{
    public abstract class AggregateRootIntId : AggregateRoot<int>
    {
        protected AggregateRootIntId(int id) 
            : base( id )
        {
        }
        protected override void Validate(ValidationResultCollection results)
        {
            if (Id == null)
                results.Add(new ValidationResult("Id不能为空"));
        }
    }
}
