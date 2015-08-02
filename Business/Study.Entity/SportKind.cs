using System;
using Study.Domains.Framework;
namespace Study.Entity
{
    public class SportKind : AggregateRootIntId
    {
        public SportKind()
            : this(int.MaxValue)
        {
        }
        public SportKind(int id)
            : base(id)
        {
        }
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}