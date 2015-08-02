using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace IBatisQueryGenerator
{
    public class GenerateEntity
    {
        private EntConnection connect;
        private string prefix = "   ";
        public string ClassNameSpace = string.Empty;
        public GenerateEntity(EntConnection con)
        {
            this.connect = con;
        }
        private List<DbColumn> GetTableColums(string db,string table)
        {
            #region SQL
            string sql = string.Format(@"
                                    WITH indexCTE AS
                                    (
                                        SELECT 
                                        ic.column_id,
                                        ic.index_column_id,
                                        ic.object_id    
                                        FROM {0}.sys.indexes idx
                                        INNER JOIN {0}.sys.index_columns ic ON idx.index_id = ic.index_id AND idx.object_id = ic.object_id
                                        WHERE  idx.object_id =OBJECT_ID(@tableName) AND idx.is_primary_key=1
                                    )
                                    select
                                    colm.column_id ColumnID,
                                    CAST(CASE WHEN indexCTE.column_id IS NULL THEN 0 ELSE 1 END AS BIT) IsPrimaryKey,
                                    colm.name ColumnName,
                                    systype.name ColumnType,
                                    colm.is_identity IsIdentity,
                                    colm.is_nullable IsNullable,
                                    cast(colm.max_length as int) ByteLength,
                                    (
                                        case 
                                            when systype.name='nvarchar' and colm.max_length>0 then colm.max_length/2 
                                            when systype.name='nchar' and colm.max_length>0 then colm.max_length/2
                                            when systype.name='ntext' and colm.max_length>0 then colm.max_length/2 
                                            else colm.max_length
                                        end
                                    ) CharLength,
                                    cast(colm.precision as int) Precision,
                                    cast(colm.scale as int) Scale,
                                    prop.value Remark
                                    from {0}.sys.columns colm
                                    inner join {0}.sys.types systype on colm.system_type_id=systype.system_type_id and colm.user_type_id=systype.user_type_id
                                    left join {0}.sys.extended_properties prop on colm.object_id=prop.major_id and colm.column_id=prop.minor_id
                                    LEFT JOIN indexCTE ON colm.column_id=indexCTE.column_id AND colm.object_id=indexCTE.object_id                                        
                                    where colm.object_id=OBJECT_ID(@tableName)
                                    order by colm.column_id", db);
            #endregion
            return connect.GetDbColumns(sql, db, table);
        }

        public string BeginGenerateEntity(string db,string table,string className,bool isEntityClass =false)
        {
            StringBuilder builder=new StringBuilder ();
            
            if (isEntityClass)
            {
                builder.Append(prefix + "public class " + className + ":" + "AggregateRootIntId" + "\r\n");
                builder.Append(prefix + "{\r\n");
                builder.Append(prefix + prefix + "public " + className + "()");
                builder.Append("\r\n");
                builder.Append(prefix + prefix + ": this(int.MaxValue)");
                builder.Append("\r\n");
                builder.Append(prefix + prefix + "{");
                builder.Append("\r\n");
                builder.Append(prefix + prefix + "}");
                builder.Append("\r\n");

                builder.Append(prefix + prefix + "public " + className + "(int id)");
                builder.Append("\r\n");
                builder.Append(prefix + prefix + ": base(id)");
                builder.Append("\r\n");
                builder.Append(prefix + prefix + "{");
                builder.Append("\r\n");
                builder.Append(prefix + prefix + "}");
                builder.Append("\r\n");
            }
            else
            {
                builder.Append(prefix + "public class " + className  + "\r\n");
                builder.Append(prefix + "{\r\n");
            }
            foreach (DbColumn column in GetTableColums(db, table))
            {
                builder.Append(prefix + "    public ");
                builder.Append(column.CSharpType);
                if(column.CommonType.IsValueType && column.IsNullable)
                {
                    builder.Append("?");
                }
                builder.Append("  "+column.ColumnName);
                builder.Append(" { get; set; } ");
                builder.Append("\r\n");
            }
            builder.Append(prefix + "}\r\n");

            return builder.ToString();
        }

        public string BeginGenerateEntityClass(string db, string table, string className)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("using System;");
            builder.Append("\r\n");
            builder.Append("using Study.Domains.Framework;");
            builder.Append("\r\n");
            builder.Append("namespace " + ClassNameSpace);
            builder.Append("\r\n");
            builder.Append("{");
            builder.Append("\r\n");
            builder.Append(BeginGenerateEntity(db, table, className,true));
            builder.Append("}");
            return builder.ToString();
        }

        public string BeginGenerateEntityQuery(string db, string table)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("using System;");
            builder.Append("\r\n");
            builder.Append("using Study.Domains.Framework.Repositories;");
            builder.Append("\r\n");
            builder.Append("using System.ComponentModel.DataAnnotations;");
            builder.Append("\r\n");
            builder.Append("\r\n");
            builder.Append("namespace " + ClassNameSpace);
            builder.Append("\r\n");
            builder.Append("{");
            builder.Append("\r\n");
            string className = table+"Query" + "  :   Pager";
            string result = BeginGenerateEntity(db, table, className);
            builder.Append( result);
            builder.Append("\r\n");
            builder.Append("}");
            return builder.ToString();
        }

        public string BeginGenerateEntityDto(string db, string table)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("using System;");
            builder.Append("\r\n");
            builder.Append("using System.ComponentModel.DataAnnotations;");
            builder.Append("\r\n");
            builder.Append("using System.Runtime.Serialization;");
            builder.Append("\r\n");
            //builder.Append("using Study.ApplicationServices;");
            //builder.Append("\r\n");
            builder.Append("\r\n");
            builder.Append("namespace " + ClassNameSpace);
            builder.Append("\r\n");
            builder.Append("{");
            builder.Append("\r\n");
            string className = table + "Dto";// +"  :   DtoBase";
            string result = BeginGenerateEntity(db, table, className);
            builder.Append(result);
            builder.Append("\r\n");
            builder.Append("}");
            return builder.ToString();
        }
    }
}
