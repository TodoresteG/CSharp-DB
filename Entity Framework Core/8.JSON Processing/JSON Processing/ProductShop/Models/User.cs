namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static ModelValidator.UserValidator;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; }

        [Range(0, 110)]
        public int? Age { get; set; }

        [InverseProperty("Seller")]
        public ICollection<Product> ProductsSold { get; set; } = new HashSet<Product>();

        [InverseProperty("Buyer")]
        public ICollection<Product> ProductsBought { get; set; } = new HashSet<Product>();
    }
}
