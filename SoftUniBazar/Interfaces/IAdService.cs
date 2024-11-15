using SoftUniBazar.Models;

namespace SoftUniBazar.Interfaces
{
    public interface IAdService
    {
        Task AddAdToCartAsync(string userId, int id);

        Task<AdFormModel> CreateAdModelToEdit(int id);

        Task CreateNewAdAsync(AdFormModel model, string userId);

        Task EditAdEntityAsync(int id, AdFormModel model);

        Task<ICollection<AdViewModel>> GetAllAdsAsync();

        Task<ICollection<AdViewModel>> GetAllAdsInCart(string userId);

        Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync();

        Task<bool> IsAdExisting(int id);

        Task<bool> IsUserAuthorized(string userId, int id);

        Task RemoveAdFromCartAsync(string userId, int id);
    }
}
