using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Study.ApplicationServices;

namespace Study.Entity
{
    public class MoneyKindDto 
    {
        public int Id { get; set; }
        public int KindType { get; set; }
        public string Name { get; set; }
        public int? InOrOut { get; set; }
    }

}
