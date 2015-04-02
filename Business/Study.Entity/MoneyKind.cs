using System;
using Study.Domains.Framework;
namespace Study.Entity
{
    public class MoneyKind : AggregateRootIntId
    {
        public MoneyKind()
            : this(int.MaxValue)
        {
        }
        public MoneyKind(int id)
            : base(id)
        {
        }
        //public int Id { get; set; }
        public int KindType { get; set; }
        public string Name { get; set; }
        public int? InOrOut { get; set; }
    }
}
