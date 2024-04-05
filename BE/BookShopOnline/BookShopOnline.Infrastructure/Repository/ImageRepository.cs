using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Dapper.SqlMapper;
using Dapper;

namespace BookShopOnline.Infrastructure.Repository
{
    public class ImageRepository :  IImageRepository
    {
        IDbContext _dbContext;
        public ImageRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var procName = "Proc_Image_Delete";
            var res = _dbContext.Connection.ExecuteAsync(procName, new { TableId = id });
            return res;
        }

        public Task<int> DeleteManyAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {

            var res = await _dbContext.GetAllAsync<Image>();
            return res;
        }

        public async Task<Image?> GetByEnityIdAsync(Guid tableId)
        {
            var procName = "Proc_Image_GetByTableId";
            var param = new DynamicParameters();
            param.Add("TableId", tableId);
            var res = await _dbContext.Connection.QuerySingleAsync<Image>(procName, param);
            return res;
        }

        public async Task<int> InsertAsync(Image image, IFormFile? imageFile)
        {
            if (imageFile?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await imageFile.CopyToAsync(stream);
                }
                var imagePath = $"/images/{imageFile.FileName}";
                image.ImagePath = imagePath;
            }
            var procName = "Proc_Image_Insert";
            var res = await _dbContext.Connection.ExecuteAsync(procName, image);

            return res;
        }

        public async Task<int> UpdateAsync(Guid id, Image image, IFormFile? imageFile)
        {
            if (imageFile?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await imageFile.CopyToAsync(stream);
                }
                var imagePath = $"/images/{imageFile.FileName}";
                image.ImagePath = imagePath;
            }
            var procName = "Proc_Image_Update";
            var param = new DynamicParameters();
            param.Add("ImagePath", image.ImagePath);
            param.Add("TableId", id);
            param.Add("ModifiedBy", "LQHUY");
            var res = await _dbContext.Connection.ExecuteAsync(procName, param);

            return res;
        }
    }
}
