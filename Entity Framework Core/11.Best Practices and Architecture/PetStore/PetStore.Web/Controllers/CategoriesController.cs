namespace PetStore.Web.Controllers
{
    using Services;
    using Services.Implementations;
    using Models.Category;
    using Microsoft.AspNetCore.Mvc;
    using Services.Models.Category;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult All() 
        {
            var allCategories = this.categoryService.All();

            return View(allCategories);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var serviceModel = new CategoryCreateServiceModel 
            {
                Name = model.Name,
                Description = model.Description
            };

            this.categoryService.Create(serviceModel);

            return this.RedirectToAction("All", "Categories");
        }

        public IActionResult Edit(int id) 
        {
            var category = this.categoryService.Details(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel model) 
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var serviceModel = new CategoryEditServiceModel 
            {
                Name = model.Name,
                Description = model.Description
            };

            this.categoryService.Edit(serviceModel);

            return this.RedirectToAction("All", "Categories");
        }

        public IActionResult Details(int id)
        {
            var category = this.categoryService.Details(id);

            return View(category);
        }
    }
}
