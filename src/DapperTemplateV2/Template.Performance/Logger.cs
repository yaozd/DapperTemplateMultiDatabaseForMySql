using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Performance
{
    /// <summary>
    /// 简单记录日志
    /// 主要用于性能测试的日志记录
    /// </summary>
    class Logger
    {
        private static StreamWriter _mWriter;
        //日志文件控制在5M以内
        private static long size = 5120000;
        public static void Info(string pattern, params object[] args)
        {
            Log(pattern, args);
        }
        public static void Close()
        {
            _mWriter.Close();
        }

        private static void Log(string pattern, params object[] args)
        {
            //日志文件控制在5M以内
            if (_mWriter != null && _mWriter.BaseStream.Length < size)
            {
                string line = string.Format(pattern, args);
                _mWriter.WriteLine(line);
                _mWriter.Flush();
            }
        }
        /// <summary>
        ///  初始化
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public static void Initialize(string filePath, string fileName)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                if (fileName.IndexOf("/", StringComparison.Ordinal) != -1)
                {
                    fileName = fileName.Replace("/", "-");
                }
                var logFile = Path.Combine(filePath, fileName);
                if (!File.Exists(logFile))
                {
                    var directoryInfo = new FileInfo(logFile).Directory;
                    if (directoryInfo != null) directoryInfo.Create();
                }

                _mWriter = new StreamWriter(logFile, true);
                if (_mWriter == null)
                {
                    throw new Exception("文件不存在：" + logFile);
                }
                if (_mWriter.BaseStream.Length < size) return;
                Display("文件已经在5M以上，必须删除！：" + logFile);
            }
            catch (Exception e)
            {
                Display("Logger初始化错误： " + e.Message + " " + e.StackTrace + ".");
            }
        }

        static void Display(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}
