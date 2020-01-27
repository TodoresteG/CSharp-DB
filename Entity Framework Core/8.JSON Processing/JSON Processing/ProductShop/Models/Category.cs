namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.CategoryValidator;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; } = new HashSet<CategoryProduct>();
    }
}
