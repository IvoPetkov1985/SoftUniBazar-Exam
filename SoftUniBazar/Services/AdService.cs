using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.DataModels;
using SoftUniBazar.Interfaces;
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

        public async Task AddAdToCartAsync(string userId, int id)
        {
            if (await context.AdsBuyers.AnyAsync(ab => ab.BuyerId == userId && ab.AdId == id) == false)
            {
                AdBuyer entry = new()
                {
                    BuyerId = userId,
                    AdId = id
                };

                await context.AdsBuyers.AddAsync(entry);

                await context.SaveChangesAsync();
            }
        }

        public async Task<AdFormModel> CreateAdModelToEdit(int id)
        {
            Ad adToEdit = await context.Ads
                .AsNoTracking()
                .FirstAsync(a => a.Id == id);

            AdFormModel formModel = new()
            {
                Name = adToEdit.Name,
                Description = adToEdit.Description,
                ImageUrl = adToEdit.ImageUrl,
                Price = adToEdit.Price,
                CategoryId = adToEdit.CategoryId
            };

            return formModel;
        }

        public async Task CreateNewAdAsync(AdFormModel model, string userId)
        {
            Ad adEntity = new()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.Now,
                Price = model.Price,
                CategoryId = model.CategoryId,
                OwnerId = userId
            };

            await context.Ads.AddAsync(adEntity);

            await context.SaveChangesAsync();
        }

        public async Task EditAdEntityAsync(int id, AdFormModel model)
        {
            Ad ad = await context.Ads
                .FirstAsync(a => a.Id == id);

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();
        }

        public async Task<ICollection<AdViewModel>> GetAllAdsAsync()
        {
            ICollection<AdViewModel> allAds = await context.Ads
                .AsNoTracking()
                .Select(a => new AdViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    CreatedOn = a.CreatedOn.ToString(AdDateAndTimeFormat),
                    Price = a.Price,
                    Category = a.Category.Name,
                    Owner = a.Owner.UserName
                })
                .ToListAsync();

            return allAds;
        }

        public async Task<ICollection<AdViewModel>> GetAllAdsInCart(string userId)
        {
            ICollection<AdViewModel> cartCollection = await context.AdsBuyers
                .AsNoTracking()
                .Where(ab => ab.BuyerId == userId)
                .Select(ab => new AdViewModel()
                {
                    Id = ab.Ad.Id,
                    Name = ab.Ad.Name,
                    Description = ab.Ad.Description,
                    ImageUrl = ab.Ad.ImageUrl,
                    CreatedOn = ab.Ad.CreatedOn.ToString(AdDateAndTimeFormat),
                    Price = ab.Ad.Price,
                    Category = ab.Ad.Category.Name,
                    Owner = ab.Ad.Owner.UserName
                })
                .ToListAsync();

            return cartCollection;
        }

        public async Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync()
        {
            ICollection<CategoryViewModel> allCategories = await context.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return allCategories;
        }

        public async Task<bool> IsAdExisting(int id)
        {
            Ad? ad = await context.Ads
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsUserAuthorized(string userId, int id)
        {
            Ad ad = await context.Ads
                .AsNoTracking()
                .FirstAsync(a => a.Id == id);

            if (ad.OwnerId != userId)
            {
                return false;
            }

            return true;
        }

        public async Task RemoveAdFromCartAsync(string userId, int id)
        {
            if (await context.AdsBuyers.AnyAsync(ab => ab.BuyerId == userId && ab.AdId == id))
            {
                AdBuyer entry = await context.AdsBuyers
                    .FirstAsync(ab => ab.BuyerId == userId && ab.AdId == id);

                context.AdsBuyers.Remove(entry);

                await context.SaveChangesAsync();
            }
        }
    }
}
