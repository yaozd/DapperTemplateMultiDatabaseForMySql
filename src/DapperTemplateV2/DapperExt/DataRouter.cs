using System;
using System.Text.RegularExpressions;

namespace DapperExt
{
    /// <summary>
    /// 数据路由
    /// --
    /// TODO 大项目下数据路由器 
    /// </summary>
    public class DataRouter
    {
        // 提取SQL语句中的表名
        static readonly Regex RegexTable = new Regex(@"(\[[^]]+)", RegexOptions.Compiled);

        /// <summary>
        /// 将SQL语名中表名，按照数据路由算法进行转换
        /// 例：select * from [tablename] 进行解析
        /// [tablename]转为[tablename_01]
        /// select * from [tablename_01]
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Table(string sql, object obj)
        {
            if (!RegexTable.IsMatch(sql)) throw new Exception("错误：请检查SQL语句中表名的写法，表名必须在[]中-例：[tablename]");
            //TODO 实际表的数据路由算法
            var tableNum = TableNum(obj);
            return RegexTable.Replace(sql, "$1_" + tableNum);
        }

        /// <summary>
        /// 表的序列号
        /// </summary>
        /// <param name="obj">实际数据拆分--唯度指标参数</param>
        /// <returns></returns>
        private static string TableNum(object obj)
        {
            var num = 1;
            return num.ToString().PadLeft(2, '0');
        }

        public static string Database(string conn, object obj)
        {
            //TODO 实际数据库的数据路由算法
            //需要考虑的两个方面：
            //1.数据库的地址：Server=
            //2.数据库的名称：Database=
            return "database-name";
        }
    }
}