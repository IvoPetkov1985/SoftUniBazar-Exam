using SoftUniBazar.Models;

namespace SoftUniBazar.Contracts
{
    public interface IAdService
    {
        Task AddAdAsync(AdFormModel model, string userId);

        Task AddAdToCollectionAsync(string userId, int id);

        Task<AdFormModel> CreateEditModelAsync(int id);

        Task EditAdAsync(AdFormModel model, int id);

        Task<IEnumerable<AdViewModel>> GetAllAdsAsync();

        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();

        Task<IEnumerable<AdViewModel>> GetAllItemsInCollectionAsync(string userId);

        Task<bool> IsAdExistingAsync(int id);

        Task<bool> IsUserAuthorizedAsync(string userId, int id);

        Task RemoveAdFromCollectionAsync(string userId, int id);
    }
}
