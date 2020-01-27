namespace ProductShop.DTO.Products
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class SoldProductsWithCountDTO
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("products")]
        public ICollection<SimpleProductDTO> Products { get; set; }
    }
}
