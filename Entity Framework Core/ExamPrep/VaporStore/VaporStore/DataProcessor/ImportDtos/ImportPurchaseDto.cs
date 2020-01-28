namespace VaporStore.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static ModelValidator.PurchaseValidator;
    using static ModelValidator.CardValidator;

    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        [XmlAttribute("title")]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [RegularExpression(ProductKeyPattern)]
        public string Key { get; set; }

        [Required]
        [RegularExpression(NumberPattern)]
        public string Card { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
