using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBatisQueryGenerator
{
    public  class DbColumn
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 数据库类型对应的C#类型
        /// </summary>
        public string CSharpType
        {
            get
            {
                return SqlServerDbTypeMap.MapCsharpType(ColumnType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type CommonType
        {
            get
            {
                return SqlServerDbTypeMap.MapCommonType(ColumnType);
            }
        }

        /// <summary>
        /// 字节长度
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// 字符长度
        /// </summary>
        public int CharLength { get; set; }

        /// <summary>
        /// 小数位
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 是否自增列
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
