using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Study.Entity
{
    /// <summary>
    /// 数据传输对象
    /// </summary>
    [DataContract]
    public abstract class DtoBase
    {
        /// <summary>
        /// 标识
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
    }

}
