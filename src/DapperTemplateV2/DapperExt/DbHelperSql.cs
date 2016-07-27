﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;

namespace DapperExt
{
    public class DbHelperSql
    {
        #region 增

        /// <summary>
        /// 插入数据-返回自增是否插入成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertSql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Insert<T>(string insertSql, T param, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            using (var conn = GetWriter())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(insertSql);
                var flag = conn.Execute(strSql.ToString(), param, transaction, commandTimeout);
                return flag == 1;
            }
        }

        /// <summary>
        /// 插入数据-返回自增id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertSql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int InsertReturnId<T>(string insertSql, T param, IDbTransaction transaction = null) where T : class
        {
            using (var conn = GetWriter())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(insertSql);
                //当前范围内最近插入行生成的标示值-自增长id字段
                strSql.Append(";select last_insert_id();");
                var keyId = conn.Query<int>(strSql.ToString(), param, transaction).FirstOrDefault();
                return keyId;
            }
        }

        /// <summary>
        /// 批量插入数据
        /// 原理：
        /// paramList进行循环插入生成多条insert插入语句
        /// 这种有问题隐患--出现部分记录插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertSql"></param>
        /// <param name="paramList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool InsertBatch<T>(string insertSql, IList<T> paramList, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            using (var conn = GetWriter())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(insertSql);
                var flag = conn.Execute(strSql.ToString(), paramList, transaction, commandTimeout);
                return flag == paramList.Count;
            }
        }
        #region 批量插入功能-删除
        ///// <summary>
        ///// 批量插入功能
        ///// --能过SqlBulkCopy实现
        ///// </summary>
        //public void InsertBatchBySqlBulkCopy<T>(IEnumerable<T> entityList, IDbTransaction transaction = null)
        //    where T : class
        //{
        //    InsertBatchBySqlBulkCopy(null, entityList, transaction);
        //}

        ///// <summary>
        ///// 批量插入功能
        ///// --能过SqlBulkCopy实现
        ///// --注意：如果你对速度十分敏感的话就不要用反射的方法写了。直接传datatable吧
        ///// 可能会好一些，但具体的还是要再测试的
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="tableName"></param>
        ///// <param name="entityList"></param>
        ///// <param name="transaction"></param>
        //public bool InsertBatchBySqlBulkCopy<T>(string tableName, IEnumerable<T> entityList,
        //    IDbTransaction transaction = null) where T : class
        //{
        //    Type classType = typeof(T);
        //    var tablename = tableName ?? classType.Name;
        //    PropertyInfo[] propertyInfos = classType.GetProperties();
        //    var tran = (SqlTransaction)transaction;
        //    using (var conn = GetWriter())
        //    {
        //        using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
        //        {
        //            var enumerable = entityList as T[] ?? entityList.ToArray();
        //            bulkCopy.BatchSize = enumerable.Count();
        //            bulkCopy.DestinationTableName = tablename;
        //            var propertyLegnth = propertyInfos.Count();
        //            var table = new DataTable();
        //            for (int i = 0; i < propertyLegnth; i++)
        //            {
        //                bulkCopy.ColumnMappings.Add(propertyInfos[i].Name, propertyInfos[i].Name);
        //                table.Columns.Add(propertyInfos[i].Name,
        //                    Nullable.GetUnderlyingType(propertyInfos[i].PropertyType) ?? propertyInfos[i].PropertyType);
        //            }
        //            //
        //            var dataRow = new object[propertyLegnth];
        //            foreach (var entity in enumerable)
        //            {
        //                for (int i = 0; i < propertyLegnth; i++)
        //                {
        //                    dataRow[i] = propertyInfos[i].GetValue(entity, null);
        //                }
        //                table.Rows.Add(dataRow);
        //            }
        //            conn.Open();
        //            bulkCopy.WriteToServer(table);
        //            conn.Close();
        //        }
        //    }
        //    return true;
        //}
        #endregion
        #endregion

        #region 删

        public bool Delete<T>(string deleteSql, T param, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            using (var conn = GetWriter())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(deleteSql);
                var flag = conn.Execute(strSql.ToString(), param, transaction, commandTimeout);
                return flag > 0;
            }
        }

        #endregion

        #region 改

        public bool Update<T>(string updateSql, T param, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            using (var conn = GetWriter())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(updateSql);
                var flag = conn.Execute(strSql.ToString(), param, transaction, commandTimeout);
                return flag > 0;
            }
        }

        #endregion

        #region 查

        public T Find<T>(string findSql, object param, IDbTransaction transaction = null) where T : class
        {
            using (var conn = GetReader())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(findSql);
                var result = conn.Query<T>(strSql.ToString(), param, transaction).FirstOrDefault();
                return result;
            }
        }

        public IList<T> FindList<T>(string findSql, object param, IDbTransaction transaction = null) where T : class
        {
            using (var conn = GetReader())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(findSql);
                var result = conn.Query<T>(strSql.ToString(), param, transaction);
                return result.ToList();
            }
        }

        public int Count(string findSql, object param, IDbTransaction transaction = null)
        {
            using (var conn = GetReader())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(findSql);
                var result = conn.Query<int>(strSql.ToString(), param, transaction).FirstOrDefault();
                return result;
            }
        }

        public T Find<T>(string findSql, T param, IDbTransaction transaction = null) where T : class
        {
            using (var conn = GetReader())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(findSql);
                var result = conn.Query<T>(strSql.ToString(), param, transaction).FirstOrDefault();
                return result;
            }
        }

        public IList<T> FindList<T>(string findSql, T param, IDbTransaction transaction = null) where T : class
        {
            using (var conn = GetReader())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(findSql);
                var result = conn.Query<T>(strSql.ToString(), param, transaction);
                return result.ToList();
            }
        }

        public int Count<T>(string findSql, T param, IDbTransaction transaction = null) where T : class
        {
            using (var conn = GetReader())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(findSql);
                var result = conn.Query<int>(strSql.ToString(), param, transaction).FirstOrDefault();
                return result;
            }
        }

        #endregion

        #region SqlConnection--这里主要是考虑到读写分离

        private readonly string _readerConn;
        private readonly string _writerConn;

        public DbHelperSql(string readerConn, string writerConn)
        {
            _readerConn = readerConn;
            _writerConn = writerConn;
        }
        /// <summary>
        /// 读
        /// </summary>
        /// <returns></returns>
        MySqlConnection GetReader()
        {
            return new MySqlConnection(_readerConn);
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <returns></returns>
        MySqlConnection GetWriter()
        {
            return new MySqlConnection(_writerConn);
        }

        #endregion
    }
}