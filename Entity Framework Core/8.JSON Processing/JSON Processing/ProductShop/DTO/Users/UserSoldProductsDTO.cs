namespace ProductShop.DTO.Users
{
    using Newtonsoft.Json;
    using DTO.Products;
    using System.Collections.Generic;

    public class UserSoldProductsDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public ICollection<SoldProductDTO> SoldProducts { get; set; } = new HashSet<SoldProductDTO>();
    }
}
