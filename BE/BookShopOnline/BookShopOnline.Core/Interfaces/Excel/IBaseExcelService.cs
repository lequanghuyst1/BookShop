using BookShopOnline.Core.Model.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Excel
{
    public interface IBaseExcelService<TEntity>
    {
        /// <summary>
        /// Export tất cả các bản ghi vào file excel
        /// </summary>
        /// <param name="data">Danh sách các bản ghi lấy từ database</param>
        /// <returns>Mảng byte của file excel</returns>
        /// Created By: LQHUY(25/04/2024)
        Task<byte[]> ExportAllAsync(ExcelRequest<TEntity> excelRequest);

        /// <summary>
        /// Export các bản ghi theo danh dách id vào file excel
        /// </summary>
        /// <param name="data">danh sách các bản ghi lấy từ database theo danh sách id</param>
        /// <returns>Mảng byte của file excel</returns>
        /// Created By: LQHUY(25/04/2022)
        Task<byte[]> ExportListAsync(ExcelRequest<TEntity> excelRequest);
    }
}
