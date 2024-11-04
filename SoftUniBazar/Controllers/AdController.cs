using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Contracts;
using SoftUniBazar.Models;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Controllers
{
    public class AdController : BaseController
    {
        private readonly IAdService service;

        public AdController(IAdService adService)
        {
            service = adService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await service.GetAllAdsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await service.GetCategoriesAsync();

            var model = new AdFormModel()
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormModel model)
        {
            var categories = await service.GetCategoriesAsync();

            if (!categories.Any(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), MissingCategoryErrorMsg);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await service.GetCategoriesAsync();
                return View(model);
            }

            string userId = GetUserId();

            await service.CreateNewAdEntityAsync(model, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool isExisting = await service.IsAdExistingAsync(id);

            if (isExisting == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            bool isUserAuthorized = await service.IsUserAuthorizedAsync(userId, id);

            if (isUserAuthorized == false)
            {
                return Unauthorized();
            }

            var model = await service.CreateModelToEditAsync(id);
            model.Categories = await service.GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdFormModel model, int id)
        {
            bool isExisting = await service.IsAdExistingAsync(id);

            if (isExisting == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            bool isUserAuthorized = await service.IsUserAuthorizedAsync(userId, id);

            if (isUserAuthorized == false)
            {
                return Unauthorized();
            }

            var categories = await service.GetCategoriesAsync();

            if (!categories.Any(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), MissingCategoryErrorMsg);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await service.GetCategoriesAsync();
                return View(model);
            }

            await service.EditAdAsync(id, model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            bool isExisting = await service.IsAdExistingAsync(id);

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

            var model = await service.GetAllAdsInCartAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            string userId = GetUserId();

            await service.RemoveAdFromCartAsync(userId, id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            bool isExisting = await service.IsAdExistingAsync(id);

            if (isExisting == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            bool isUserAuthorized = await service.IsUserAuthorizedAsync(userId, id);

            if (isUserAuthorized == false)
            {
                return Unauthorized();
            }

            await service.DeleteAdAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
