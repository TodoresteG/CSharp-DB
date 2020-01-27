namespace CarDealer.DTO
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class CarsAndPartsDTO
    {
        [JsonProperty("car")]
        public CarMakeModelDistanceDTO Car { get; set; }

        [JsonProperty("parts")]
        public ICollection<PartNamePriceDTO> Parts { get; set; }
    }
}
