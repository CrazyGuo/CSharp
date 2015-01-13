using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    interface IEntQueryByProject
    {
        StringBuilder getConnectionInfo();
        StringBuilder getSchemaQuery();
        StringBuilder getTableQuery(string schemaName);
        StringBuilder getColumnQuery(string tableName, bool upperYn);
    }
}
