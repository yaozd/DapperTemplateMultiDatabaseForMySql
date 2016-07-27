
using System;
using System.Collections.Generic;
using System.Text;
using DapperExt;
using Template.Model;

namespace Template.Dao
{
    public class UrlInfoDao
    {
        private readonly DbHelperSql _dbHelper;
        public UrlInfoDao()
        {
            _dbHelper = SqlMapper.Instance.GetDbHelperSql(EnumDbName.Test1);
        }
        //

        public int Add(UrlInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UrlInfo ( ");
            strSql.Append("url,urlMd5,shortVal,comment,state,createTime,isDel)");
            strSql.Append(" values (");
            strSql.Append("@url,@urlMd5,@shortVal,@comment,@state,@createTime,@isDel)");
            //
            var id = _dbHelper.InsertReturnId(strSql.ToString(), model);
            return id;
        }
        //

        public bool DeleteById(Int64 id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UrlInfo ");
            strSql.Append("where  id=@id  ");
            //
            var flag = _dbHelper.Delete(strSql.ToString(), new UrlInfo { Id = id });
            return flag;
        }
        //

        public bool UpdateById(UrlInfo model, Int64 id)
        {
            model.Id = id;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UrlInfo set ");
            if (model.@Url != null)
                strSql.Append("url=@Url,");
            if (model.@UrlMd5 != null)
                strSql.Append("urlMd5=@UrlMd5,");
            if (model.@ShortVal != null)
                strSql.Append("shortVal=@ShortVal,");
            if (model.@Comment != null)
                strSql.Append("comment=@Comment,");
            if (model.@State != null)
                strSql.Append("state=@State,");
            if (model.@CreateTime != null)
                strSql.Append("createTime=@CreateTime,");
            if (model.@IsDel != null)
                strSql.Append("isDel=@IsDel,");
            int n = strSql.ToString().LastIndexOf(",", StringComparison.Ordinal);
            if (n > 0) strSql.Remove(n, 1);
            strSql.Append(" where  id=@id  ");
            var flag = _dbHelper.Update(strSql.ToString(), model);
            return flag;
        }
        //

        public UrlInfo FindById(Int64 id)
        {
            //Ä¬ÈÏtop 1--1Ìõ¼ÇÂ¼
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append("id,url,urlMd5,shortVal,comment,state,createTime,isDel  ");
            strSql.Append("from UrlInfo ");
            strSql.Append("where  id=@id  ");
            strSql.Append("limit 1  ");
            //
            var result = _dbHelper.Find(strSql.ToString(), new UrlInfo { Id = id });
            return result;
        }
        //
        public IList<UrlInfo> FindList(UrlInfo whereModel, string where, int top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("id,url,urlMd5,shortVal,comment,state,createTime,isDel  ");
            strSql.Append("from UrlInfo ");
            strSql.AppendFormat("where 1=1 and {0} ", where);
            strSql.AppendFormat("limit {0}  ", top);
            var result = _dbHelper.FindList<UrlInfo>(strSql.ToString(), whereModel);
            return result;
        }
        //
        public IList<UrlInfo> FindListByPage(UrlInfo whereModel, string where, string orderBy, int pageIndex, int pageSize)
        {
            var startIndex = pageIndex * pageSize;
            var size = pageSize;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("id,url,urlMd5,shortVal,comment,state,createTime,isDel ");
            strSql.Append("from UrlInfo ");
            strSql.AppendFormat("where 1=1 and {0} ", where);
            strSql.AppendFormat("ORDER BY {0} LIMIT {1} , {2} ", orderBy, startIndex, size);
            var result = _dbHelper.FindList<UrlInfo>(strSql.ToString(), whereModel);
            return result;
        }
        //
        public int Count(UrlInfo whereModel, string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  count(*) from UrlInfo ");
            strSql.AppendFormat("where 1=1 and {0} ", where);
            var result = _dbHelper.Count(strSql.ToString(), whereModel);
            return result;
        }
        //
    }
}
