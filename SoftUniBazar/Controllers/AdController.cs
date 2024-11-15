using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Interfaces;
using SoftUniBazar.Models;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Controllers
{
    public class AdController : BaseController
    {
        private readonly IAdService service;

        public AdController(IAdService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ICollection<AdViewModel> model = await service.GetAllAdsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ICollection<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            AdFormModel model = new()
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormModel model)
        {
            ICollection<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            if (categories.Any(c => c.Id == model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), CategoryMissingMsg);
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = categories;

                return View(model);
            }

            string userId = GetUserId();

            await service.CreateNewAdAsync(model, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool isExisting = await service.IsAdExisting(id);

            if (isExisting == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            bool isAuthorized = await service.IsUserAuthorized(userId, id);

            if (isAuthorized == false)
            {
                return Unauthorized();
            }

            ICollection<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            AdFormModel model = await service.CreateAdModelToEdit(id);

            model.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdFormModel model)
        {
            bool isExisting = await service.IsAdExisting(id);

            if (isExisting == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            bool isAuthorized = await service.IsUserAuthorized(userId, id);

            if (isAuthorized == false)
            {
                return Unauthorized();
            }

            ICollection<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            if (categories.Any(c => c.Id == model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), CategoryMissingMsg);
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = categories;

                return View(model);
            }

            await service.EditAdEntityAsync(id, model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            bool isExisting = await service.IsAdExisting(id);

            if (isExisting == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            await service.AddAdToCartAsync(userId, id);

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            ICollection<AdViewModel> cartCollection = await service.GetAllAdsInCart(userId);

            return View(cartCollection);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            bool isExisting = await service.IsAdExisting(id);

            if (isExisting == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            await service.RemoveAdFromCartAsync(userId, id);

            return RedirectToAction(nameof(All));
        }
    }
}
