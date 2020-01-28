namespace PetStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Models.Pet;

    public class PetsController : Controller
    {
        private readonly IPetService petService;

        public PetsController(IPetService petService)
        {
            this.petService = petService;
        }

        public IActionResult All(int page = 1)
        {
            var allPets = this.petService.All(page);
            var total = this.petService.Total();

            var model = new AllPetsViewModel 
            {
                Pets = allPets,
                CurrentPage = page,
                Total = total
            };

            return View(model);
        }

        public IActionResult Delete(int id) 
        {
            var pet = this.petService.Details(id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public IActionResult ConfirmDelete(int id) 
        {
            var deleted = this.petService.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
