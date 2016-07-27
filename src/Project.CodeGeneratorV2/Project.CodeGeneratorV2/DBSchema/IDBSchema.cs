using System;
using System.Collections.Generic;

namespace Project.CodeGeneratorV2.DBSchema
{
    public interface IDBSchema : IDisposable
    {
        List<string> GetTablesList();

        Table GetTableMetadata(string tableName);
    }
}