using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Study.ApplicationServices;

namespace Study.Entity
{
    public class MoneyOutDto 
    {
        public int MoneyOutId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quality { get; set; }
        public DateTime OutTime { get; set; }
        public string Remark { get; set; }
    }

}
