namespace MusicHub.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static ModelValidator.PerformerValidator;

    [XmlType("Performer")]
    public class ImportPerformerDto
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

        [Range(AgeMinRange, AgeMaxRange)]
        public int Age { get; set; }

        [Range(typeof(decimal), NetWorthMinRange, NetWorthMaxRange)]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public ImportSongPerformerDto[] PerformersSongs { get; set; }
    }
}
