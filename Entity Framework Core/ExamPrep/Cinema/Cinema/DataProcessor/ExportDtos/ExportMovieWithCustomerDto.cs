namespace Cinema.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class ExportMovieWithCustomerDto
    {
        public string MovieName { get; set; }

        public string Rating { get; set; }

        public string TotalIncomes { get; set; }

        public ExportCustomerFromMovieDto[] Customers { get; set; }
    }
}
