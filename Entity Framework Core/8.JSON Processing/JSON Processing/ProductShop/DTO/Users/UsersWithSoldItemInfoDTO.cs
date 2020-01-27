namespace ProductShop.DTO.Users
{
    using Newtonsoft.Json;
    using Products;
    using System.Collections.Generic;

    public class UsersWithSoldItemInfoDTO
    {
        [JsonProperty("fisrtName")]
        public string FisrtName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("soldProducts")]
        public SoldProductsWithCountDTO SoldProducts { get; set; }
    }
}
