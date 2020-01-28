namespace PetStore.Services.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Models.ModelValidator;

    public class CategoryListingServiceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
