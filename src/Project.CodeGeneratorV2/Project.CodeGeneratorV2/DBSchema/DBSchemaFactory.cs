using System;

namespace Project.CodeGeneratorV2.DBSchema
{
    public class DBSchemaFactory
    {
        static readonly string DatabaseType = "MySql";
        public static IDBSchema GetDBSchema()
        {
            IDBSchema dbSchema;
            switch (DatabaseType)
            {
                case "SqlServer":
                    {
                        dbSchema = new SqlServerSchema();
                        break;
                    }
                case "MySql":
                    {
                        dbSchema = new MySqlSchema();
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("The input argument of DatabaseType is invalid!");
                    }
            }
            return dbSchema;
        }
    }
}