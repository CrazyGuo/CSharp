using System;
using Study.Domains.Framework.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Study.Entity
{
    public class SportKindQuery : Pager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }

}