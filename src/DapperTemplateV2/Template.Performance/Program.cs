using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Template.Dao;
using Template.Model;

namespace Template.Performance
{
    class Program
    {
        //原子计数
        public static int Count;
        //错误原子计数
        public static int ErrorCount;
        //测试数据
        public static Byte[] TestData;
        public static int TestDataSize = 1 * 1024;
        public static string LogPath = @"D:\temp-test";
        public static string LogName = "log.txt";

        static void Main(string[] args)
        {
            Init();
            //
            while (true)
            {
                PerformanceTest.Time("Test_Set", 40, 1000, (Test_Set));  
            }
          
            //
            End();
        }

        static void Test_Set()
        {
            //
            var model = new UrlInfo
            {
                Url = "http:www.baidu.com",
                UrlMd5 = "atSDsdfsfsfsdsffw",
                ShortVal = "adEwF",
                Comment = "百度",
                State = 1,
                CreateTime = DateTime.Now
            };
            var urlInfoDao = new UrlInfoDao();
            var id = urlInfoDao.Add(model);
        }

        static void Test_Get()
        {
            //
        }
        static void Example()
        {
            var current = Interlocked.Increment(ref Count);
            Logger.Info(current.ToString());
        }

        static void Example_Loop()
        {
            while (true)
            {
                try
                {
                    //
                    PerformanceTest.Time("Test_Set", 40, 5000, (Test_Set));
                    PerformanceTest.Time("Test_Get", 40, 5000, (Test_Get));
                    //
                }
                catch (Exception ex)
                {
                    //记录报错信息
                    var current = Interlocked.Increment(ref ErrorCount);
                    Logger.Info(string.Format("2-Error-Count-{0}-{1}", current, DateTime.Now));
                    Logger.Info(string.Format("2-Error-Count-{0}-{1}", current, ex.Message));
                }
            }
        }
        static void Init()
        {
            Logger.Initialize(LogPath, LogName);
            PerformanceTest.Initialize();
            BuildTestData();
        }
        private static void End()
        {
            Console.WriteLine("Test End");
            Logger.Close();
            Console.Read();
        }
        static void BuildTestData()
        {
            TestData = new byte[TestDataSize];
            for (int i = 0; i < TestDataSize; i++)
            {
                TestData[i] = 0x30;
            }
        }
    }
}
