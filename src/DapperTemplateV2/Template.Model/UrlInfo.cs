
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    [Serializable]
    public partial class UrlInfo
    {
        #region Attributes
        /// <summary>
        /// Id(PK)
        /// IsNullable=False
        ///</summary>
        private Int64 id;
        public Int64 Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Url
        /// IsNullable=True
        ///</summary>
        private String url;
        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        /// <summary>
        /// UrlMd5
        /// IsNullable=True
        ///</summary>
        private String urlMd5;
        public String UrlMd5
        {
            get { return urlMd5; }
            set { urlMd5 = value; }
        }
        /// <summary>
        /// ShortVal
        /// IsNullable=True
        ///</summary>
        private String shortVal;
        public String ShortVal
        {
            get { return shortVal; }
            set { shortVal = value; }
        }
        /// <summary>
        /// Comment
        /// IsNullable=True
        ///</summary>
        private String comment;
        public String Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        /// <summary>
        /// State
        /// IsNullable=True
        ///</summary>
        private Int32? state;
        public Int32? State
        {
            get { return state; }
            set { state = value; }
        }
        /// <summary>
        /// CreateTime
        /// IsNullable=True
        ///</summary>
        private DateTime? createTime;
        public DateTime? CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        /// <summary>
        /// IsDel
        /// IsNullable=True
        ///</summary>
        private Int32? isDel;
        public Int32? IsDel
        {
            get { return isDel; }
            set { isDel = value; }
        }
        #endregion
    }
}


