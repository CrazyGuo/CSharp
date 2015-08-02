using Study.Domains.Framework.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace Study.Entity
{
    public class SportRecordQuery : Pager
    {
        public int Id { get; set; }
        public int Numbers { get; set; }
        public DateTime ActivityTime { get; set; }
        public int ActivityKind { get; set; }
        public string Remark { get; set; }
        //业务需要增加
        public string SportName { get; set; }
        public DateTime SportFromTime { get; set; }
        public DateTime SportEndTime { get; set; }
    }

}