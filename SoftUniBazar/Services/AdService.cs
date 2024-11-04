using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Contracts;
using SoftUniBazar.Data;
using SoftUniBazar.Data.DataModels;
using SoftUniBazar.Models;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Services
{
    public class AdService : IAdService
    {
        private readonly BazarDbContext context;

        public AdService(BazarDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task AddAdToCartAsync(string userId, int id)
        {
            if (!context.AdsBuyers.Any(ab => ab.AdId == id && ab.BuyerId == userId))
            {
                await context.AdsBuyers.AddAsync(new AdBuyer()
                {
                    AdId = id,
                    BuyerId = userId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<AdFormModel> CreateModelToEditAsync(int id)
        {
            var modelToEdit = await context.Ads
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AdFormModel()
                {
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    CategoryId = a.CategoryId
                })
                .FirstAsync();

            return modelToEdit;
        }

        public async Task CreateNewAdEntityAsync(AdFormModel model, string userId)
        {
            var adEntity = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now,
                OwnerId = userId
            };

            await context.Ads.AddAsync(adEntity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAdAsync(int id)
        {
            var entityToDelete = await context.Ads
                .FirstAsync(a => a.Id == id);

            context.Ads.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        public async Task EditAdAsync(int id, AdFormModel model)
        {
            var entityToEdit = await context.Ads
                .FirstAsync(a => a.Id == id);

            entityToEdit.Name = model.Name;
            entityToEdit.Description = model.Description;
            entityToEdit.ImageUrl = model.ImageUrl;
            entityToEdit.Price = model.Price;
            entityToEdit.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();
        }

        public async Task<bool> IsAdExistingAsync(int id)
        {
            var ad = await context.Ads
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<AdAllViewModel>> GetAllAdsAsync()
        {
            var allAds = await context.Ads
                .AsNoTracking()
                .Select(a => new AdAllViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    Category = a.Category.Name,
                    CreatedOn = a.CreatedOn.ToString(DateTimeFormat),
                    Owner = a.Owner.UserName
                })
                .ToListAsync();

            return allAds;
        }

        public async Task<IEnumerable<AdCartViewModel>> GetAllAdsInCartAsync(string userId)
        {
            var adsInCart = await context.AdsBuyers
                .AsNoTracking()
                .Where(ab => ab.BuyerId == userId)
                .Select(a => new AdCartViewModel()
                {
                    Id = a.Ad.Id,
                    Name = a.Ad.Name,
                    Description = a.Ad.Description,
                    ImageUrl = a.Ad.ImageUrl,
                    Price = a.Ad.Price,
                    Category = a.Ad.Category.Name,
                    CreatedOn = a.Ad.CreatedOn.ToString(DateTimeFormat),
                    Owner = a.Ad.Owner.UserName
                })
                .ToListAsync();

            return adsInCart;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await context.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return categories;
        }

        public async Task RemoveAdFromCartAsync(string userId, int id)
        {
            if (context.AdsBuyers.Any(ab => ab.AdId == id && ab.BuyerId == userId))
            {
                var entityToRemove = await context.AdsBuyers.FirstAsync(ab => ab.AdId == id && ab.BuyerId == userId);

                context.AdsBuyers.Remove(entityToRemove);

                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsUserAuthorizedAsync(string userId, int id)
        {
            var adToCheck = await context.Ads
                .AsNoTracking()
                .FirstAsync(a => a.Id == id);

            if (userId != adToCheck.OwnerId)
            {
                return false;
            }

            return true;
        }
    }
}
