using ImageAnnotation.Data;
using ImageAnnotation.Models;

namespace ImageAnnotation.Repository
{
    public interface IImageRepository
    {
        Task<IEnumerable<AssetInfo>> GetAll();
        Task<AssetInfo> GetByIdAsync(string id);
        bool Add(AssetInfo assetInfo);
        bool Update(AssetInfo assetInfo);
        bool Delete(AssetInfo assetInfo);
        bool Save();
    }
}
