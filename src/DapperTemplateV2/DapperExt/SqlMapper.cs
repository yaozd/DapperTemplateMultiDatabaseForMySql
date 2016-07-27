using System;
using System.Collections;

namespace DapperExt
{
    public class SqlMapper
    {
        private static readonly Lazy<SqlMapper> lazy = new Lazy<SqlMapper>(() => new SqlMapper());
        public static SqlMapper Instance
        {
            get { return lazy.Value; }
        }
        private static readonly object LockObject = new object();
        private readonly Hashtable _ht = new Hashtable();

        public DbHelperSql GetDbHelperSql(EnumDbName dbName)
        {
            var val = _ht[dbName];
            if (val == null)
            {
                lock (LockObject)
                {
                    val = _ht[dbName];
                    if (val == null)
                    {
                        var name = dbName.ToString("G");
                        var readerConn = ConncetionReader(name);
                        var writerConn = GetConncetionWriter(name);
                        val = new DbHelperSql(readerConn,writerConn);
                        _ht[dbName] = val;
                    }
                }
            }
            return (DbHelperSql)val;
        }

        private string GetConncetionWriter(string name)
        {
            //TODO MYSQL版本
            //TODO 数据库读取连接--可以是配置文件也可以是配置中心->根据实际情况添加 
            const string connStr = "Server=10.48.251.78;Port=22336;User ID=root;Password=1q2w3e4r5t;Database=ShortUrl_DB;CharSet=utf8;";
            //const string connStr = "Server=0.0.0.0;Port=22336;User ID=root;Password=xxx;Database=Test3;CharSet=utf8;";
            return connStr;
        }

        private string ConncetionReader(string name)
        {
            //TODO MYSQL版本
            //TODO 数据库读取连接--可以是配置文件也可以是配置中心->根据实际情况添加 
            const string connStr = "Server=10.48.251.78;Port=22336;User ID=root;Password=1q2w3e4r5t;Database=ShortUrl_DB;CharSet=utf8;";
            //const string connStr = "Server=0.0.0.0;Port=22336;User ID=root;Password=xxx;Database=Test3;CharSet=utf8;";
            return connStr;
        }
    }
}