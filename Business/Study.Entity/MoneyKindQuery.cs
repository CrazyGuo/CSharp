using Study.Domains.Framework.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Study.Entity
{
    public class MoneyKindQuery : Pager
    {
        public int Id { get; set; }
        public int KindType { get; set; }
        public string Name { get; set; }
        public int? InOrOut { get; set; }
    }

}
