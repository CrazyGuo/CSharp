using Study.Domains.Framework.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace Study.Entity
{
    public class MoneyOutQuery : Pager
    {
        public int Id { get; set; }
        public decimal Quality { get; set; }
        public int KindType { get; set; }
        public DateTime OutTime { get; set; }
        public string Remark { get; set; }
    }

}
