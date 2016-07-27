﻿using System.Data;

namespace Project.CodeGeneratorV2.DBSchema
{
    public class Column
    {
        DataColumn columnBase;

        public Column(DataColumn columnBase)
        {
            this.columnBase = columnBase;
        }

        public string ColumnName { get { return this.columnBase.ColumnName; } }

        public string MaxLength { get { return this.columnBase.MaxLength.ToString(); } }

        public string TypeName
        {
            get
            {
                string result = string.Empty;
                if (this.columnBase.DataType.Name == "Guid") //for mysql,因为对于MYSQL如果是CHAR(36),类型自动为Guid
                {
                    return "String";
                }
                //TODO 这里暂时无法确定这个方法是否行
                //for sqlservice 将数据库中timespan转换成Byte[]
                //timespan暂时无法识别--所以设定时间戳使用“_timestamp”的名称
                if (this.columnBase.DataType.Name == "Byte[]" && columnBase.ColumnName.ToLower().Contains("timestamp"))
                {
                    //标示出此字段为时间戳
                    //不能将显式值插入时间戳列。请对列列表使用 INSERT 来排除时间戳列，或将 DEFAULT 插入时间戳列。
                    IsTimestamp = true;
                }
                result = this.columnBase.DataType.Name;
                return result;
            }
        }

        public bool AllowDBNull { get { return this.columnBase.AllowDBNull; } }

        public string UpColumnName
        {
            get
            {
                return string.Format("{0}{1}", this.ColumnName[0].ToString().ToUpper(), this.ColumnName.Substring(1));
            }
        }

        public string LowerColumnName
        {
            get
            {
                return string.Format("{0}{1}", this.ColumnName[0].ToString().ToLower(), this.ColumnName.Substring(1));
            }
        }
        public bool IsPK { get; set; }
        public bool IsTimestamp { get; set; }
    }
}