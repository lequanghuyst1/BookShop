using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{   
    /// <summary>
    /// Class BaseEntity
    /// </summary>
    /// CreatedBy: LQHUY(16/03/2024)
    public class BaseEntity
    {
        #region Properties
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: LQHUY(16/03/2024)
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy: LQHUY(16/03/2024)
        [DisplayName("Người tạo")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        /// CreatedBy: LQHUY(16/03/2024)
        /// 
        /// Created By: LQHUY(25/12/2023)
        [DisplayName("Ngày chỉnh sửa")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        /// CreatedBy: LQHUY(16/03/2024)
        [DisplayName("Người chỉnh sửa")]
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
