namespace PetStore.Web.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    public class EditCategoryViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }
}
