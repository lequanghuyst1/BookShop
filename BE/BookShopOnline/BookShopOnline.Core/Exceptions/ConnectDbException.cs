using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Exceptions
{
    public class ConnectDbException : Exception
    {
        #region Properties
   
        /// <summary>
        /// Danh sách lỗi 
        /// </summary>
        /// Created By: LQHUY(25/12/2023)
        public Dictionary<string, string[]>? errors { get; set; }
        #endregion

        #region Constructors
        public ConnectDbException() { }
        public ConnectDbException(Dictionary<string, string[]> errors)
        {
            this.errors = errors;
        }

        public ConnectDbException(string message) : base(message)
        {
        }

        public ConnectDbException(string message, Dictionary<string, string[]> errors) : base(message)
        {
            this.errors = errors;
        }
        #endregion
    }
}
