namespace ProductShop.DTO.Products
{
    using Newtonsoft.Json;

    public class SimpleProductDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
