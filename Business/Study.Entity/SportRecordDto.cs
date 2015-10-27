using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;

namespace Study.Entity
{
    public class SportRecordDto
    {
        public int Id { get; set; }
        public int ActivityKind { get; set; }
        public string SportName { get; set; }
        public int Numbers { get; set; }
        public DateTime ActivityTime { get; set; }   
        [StringLength(10)]
        public string Remark { get; set; }        
        //用于用户显示的字段
    }

}