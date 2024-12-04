using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Contracts;
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
            IEnumerable<AdViewModel> model = await service.GetAllAdsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            IEnumerable<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            AdFormModel model = new()
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormModel model)
        {
            IEnumerable<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            if (categories.Any(c => c.Id == model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), CategoryInvalidMessage);
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = categories;

                return View(model);
            }

            string userId = GetUserId();

            await service.AddAdAsync(model, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await service.IsAdExistingAsync(id) == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (await service.IsUserAuthorizedAsync(userId, id) == false)
            {
                return Unauthorized();
            }

            AdFormModel model = await service.CreateEditModelAsync(id);

            IEnumerable<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            model.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdFormModel model, int id)
        {
            if (await service.IsAdExistingAsync(id) == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (await service.IsUserAuthorizedAsync(userId, id) == false)
            {
                return Unauthorized();
            }

            IEnumerable<CategoryViewModel> categories = await service.GetAllCategoriesAsync();

            if (categories.Any(c => c.Id == model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), CategoryInvalidMessage);
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = categories;

                return View(model);
            }

            await service.EditAdAsync(model, id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            IEnumerable<AdViewModel> model = await service.GetAllItemsInCollectionAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (await service.IsAdExistingAsync(id) == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            await service.AddAdToCollectionAsync(userId, id);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            if (await service.IsAdExistingAsync(id) == false)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            await service.RemoveAdFromCollectionAsync(userId, id);

            return RedirectToAction(nameof(All));
        }
    }
}
