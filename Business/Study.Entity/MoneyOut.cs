using System;
using Study.Domains.Framework;

namespace Study.Entity
{
    public class MoneyOut : AggregateRootIntId
    {
        //public int Id { get; set; }
        public MoneyOut()
            : this(int.MaxValue)
        {
        }

        public MoneyOut(int id)
            : base(id)
        {
        }

        public decimal Quality { get; set; }
        public int KindType { get; set; }
        public DateTime OutTime { get; set; }
        public string Remark { get; set; }
    }
}
