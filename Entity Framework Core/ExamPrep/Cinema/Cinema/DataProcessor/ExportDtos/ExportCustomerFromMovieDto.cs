namespace Cinema.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    [JsonArray("Customers")]
    public class ExportCustomerFromMovieDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Balance { get; set; }
    }
}
