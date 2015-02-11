using System;
using System.Runtime.Serialization;

namespace Util.ApplicationServices 
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
