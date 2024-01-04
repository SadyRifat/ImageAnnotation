using ImageAnnotation.Data;
using ImageAnnotation.Models;
using ImageAnnotation.Repository;
using Microsoft.EntityFrameworkCore;

namespace ImageAnnotation.Internal.Repository
{
    public class ImageRepository(ApplicationDbContext dbContext) : IImageRepository
    {
        public bool Add(AssetInfo assetInfo)
        {
            dbContext.Add(assetInfo);
            return Save();
        }

        public bool Delete(AssetInfo assetInfo)
        {
            dbContext.Remove(assetInfo);
            return Save();
        }

        public async Task<IEnumerable<AssetInfo>> GetAll()
        {
            return await dbContext.Assets.ToListAsync();
        }

        public async Task<AssetInfo> GetByIdAsync(string id)
        {
            return await dbContext.Assets.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = dbContext.SaveChanges();
            return saved > 0;
        }

        public bool Update(AssetInfo assetInfo)
        {
            dbContext.Update(assetInfo);
            return Save();
        }
    }
}
