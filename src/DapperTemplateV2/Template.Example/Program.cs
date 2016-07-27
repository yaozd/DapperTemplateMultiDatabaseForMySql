using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Template.Dao;
using Template.Model;
using Template.Model.ModelExt;

namespace Template.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_UrlInfo();
        }

        private static void Test_UrlInfo()
        {
            var model = new UrlInfo
            {
                Url = "http:www.baidu.com",
                UrlMd5 = "atSDsdfsfsfsdsffw",
                ShortVal = "adEwF",
                Comment = "百度",
                State = 1,
                CreateTime = DateTime.Now
            };
            var urlInfoDao=new UrlInfoDao();
            var isOkDelete = urlInfoDao.DeleteById(1);
            var id = urlInfoDao.Add(model);
            var isOkUpdate = urlInfoDao.UpdateById(new UrlInfo() {Comment = "百度update"}, id);
            var model01 = urlInfoDao.FindById(id);
            var whereModel = new UrlInfo { Id = id };
            var model02 = urlInfoDao.FindList(whereModel, "id=@id", 1);
            //---
            //---

            //todo where--比较推荐的写法--可以提高程序的可读性

            var where = new WhereEntity<UrlInfo>()
            {
                Model = new UrlInfo() { Id = id },
                Sql = "id=@id",
                OrderBy = "id"
            };
            //
            var model03 = urlInfoDao.FindListByPage(where.Model, where.Sql, where.OrderBy, 0, 10);
            var count = urlInfoDao.Count(where.Model, where.Sql);

        }

      
    }
}
