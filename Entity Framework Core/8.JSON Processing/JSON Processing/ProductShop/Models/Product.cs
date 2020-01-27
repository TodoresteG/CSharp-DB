namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.ProductValidator;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int? BuyerId { get; set; }

        public User Buyer { get; set; }

        public int SellerId { get; set; }

        public User Seller { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; } = new HashSet<CategoryProduct>();
    }
}
