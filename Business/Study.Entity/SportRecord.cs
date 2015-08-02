using System;
using Study.Domains.Framework;
namespace Study.Entity
{
    public class SportRecord : AggregateRootIntId
    {
        public SportRecord()
            : this(int.MaxValue)
        {
        }
        public SportRecord(int id)
            : base(id)
        {
        }
       // public int Id { get; set; }
        public int Numbers { get; set; }
        public DateTime ActivityTime { get; set; }
        public int ActivityKind { get; set; }
        public string Remark { get; set; }
    }
}