using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Study.Entity
{
    public class SportKindDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }

}
