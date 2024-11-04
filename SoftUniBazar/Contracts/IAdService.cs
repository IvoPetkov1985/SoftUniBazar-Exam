using SoftUniBazar.Models;

namespace SoftUniBazar.Contracts
{
    public interface IAdService
    {
        Task AddAdToCartAsync(string userId, int id);

        Task<AdFormModel> CreateModelToEditAsync(int id);

        Task CreateNewAdEntityAsync(AdFormModel model, string userId);

        Task DeleteAdAsync(int id);

        Task EditAdAsync(int id, AdFormModel model);

        Task<bool> IsAdExistingAsync(int id);

        Task<IEnumerable<AdAllViewModel>> GetAllAdsAsync();

        Task<IEnumerable<AdCartViewModel>> GetAllAdsInCartAsync(string userId);

        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        Task RemoveAdFromCartAsync(string userId, int id);

        Task<bool> IsUserAuthorizedAsync(string userId, int id);
    }
}
