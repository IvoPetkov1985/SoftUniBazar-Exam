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

        public AdService(BazarDbContext _context)
        {
            context = _context;
        }

        public async Task AddAdAsync(AdFormModel model, string userId)
        {
            Ad ad = new()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                OwnerId = userId,
                CreatedOn = DateTime.Now
            };

            await context.Ads.AddAsync(ad);

            await context.SaveChangesAsync();
        }

        public async Task AddAdToCollectionAsync(string userId, int id)
        {
            AdBuyer cartEntry = new()
            {
                BuyerId = userId,
                AdId = id
            };

            if (await context.AdsBuyers.ContainsAsync(cartEntry) == false)
            {
                await context.AdsBuyers.AddAsync(cartEntry);

                await context.SaveChangesAsync();
            }
        }

        public async Task<AdFormModel> CreateEditModelAsync(int id)
        {
            AdFormModel editModel = await context.Ads
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
                .SingleAsync();

            return editModel;
        }

        public async Task EditAdAsync(AdFormModel model, int id)
        {
            Ad adToEdit = await context.Ads
                .SingleAsync(a => a.Id == id);

            adToEdit.Name = model.Name;
            adToEdit.Description = model.Description;
            adToEdit.ImageUrl = model.ImageUrl;
            adToEdit.Price = model.Price;
            adToEdit.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdViewModel>> GetAllAdsAsync()
        {
            IEnumerable<AdViewModel> allAds = await context.Ads
                .AsNoTracking()
                .Select(a => new AdViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    CreatedOn = a.CreatedOn.ToString(DateTimeFormat),
                    Category = a.Category.Name,
                    Owner = a.Owner.UserName
                })
                .ToListAsync();

            return allAds;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            IEnumerable<CategoryViewModel> allCategories = await context.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return allCategories;
        }

        public async Task<IEnumerable<AdViewModel>> GetAllItemsInCollectionAsync(string userId)
        {
            IEnumerable<AdViewModel> adsInCollection = await context.AdsBuyers
                .AsNoTracking()
                .Where(ab => ab.BuyerId == userId)
                .Select(ab => new AdViewModel()
                {
                    Id = ab.Ad.Id,
                    Name = ab.Ad.Name,
                    Description = ab.Ad.Description,
                    ImageUrl = ab.Ad.ImageUrl,
                    Price = ab.Ad.Price,
                    Category = ab.Ad.Category.Name,
                    Owner = ab.Ad.Owner.UserName,
                    CreatedOn = ab.Ad.CreatedOn.ToString(DateTimeFormat)
                })
                .ToListAsync();

            return adsInCollection;
        }

        public async Task<bool> IsAdExistingAsync(int id)
        {
            Ad? ad = await context.Ads
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);

            return ad != null;
        }

        public async Task<bool> IsUserAuthorizedAsync(string userId, int id)
        {
            Ad? ad = await context.Ads
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id && a.OwnerId == userId);

            return ad != null;
        }

        public async Task RemoveAdFromCollectionAsync(string userId, int id)
        {
            AdBuyer cartEntry = new()
            {
                BuyerId = userId,
                AdId = id
            };

            if (await context.AdsBuyers.ContainsAsync(cartEntry))
            {
                context.AdsBuyers.Remove(cartEntry);

                await context.SaveChangesAsync();
            }
        }
    }
}
