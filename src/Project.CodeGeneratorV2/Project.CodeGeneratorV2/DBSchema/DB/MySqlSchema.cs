using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Project.CodeGeneratorV2.DBSchema
{
    public class MySqlSchema : IDBSchema
    {
        public string ConnectionString = "Server=10.48.251.78;Port=22336;User ID=root;Password=1q2w3e4r5t;Database=ShortUrl_DB;CharSet=utf8;";

        public MySqlConnection conn;

        public MySqlSchema()
        {
            conn = new MySqlConnection(ConnectionString);
            conn.Open();
        }

        public List<string> GetTablesList()
        {
            DataTable dt = conn.GetSchema("Tables");
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["TABLE_NAME"].ToString());
            }
            return list;
        }

        public Table GetTableMetadata(string tableName)
        {
            string selectCmdText = string.Format("SELECT * FROM {0}", tableName); ;
            MySqlCommand command = new MySqlCommand(selectCmdText, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(command);
            System.Data.DataSet ds = new DataSet();
            ad.FillSchema(ds, SchemaType.Mapped, tableName);
            Table table = new Table(ds.Tables[0]);
            return table;
        }

        public void Dispose()
        {
            if (conn != null)
                conn.Close();
        }
    }
}