namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static ModelValidator.OfficerValidator;

    [XmlType("Officer")]
    public class ImportOfficerDto
    {
        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string Name { get; set; }

        [Range(typeof(decimal), SalaryMinRange, SalaryMaxRange)]
        public decimal Money { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Weapon { get; set; }

        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public PrisonerWithIdDto[] Prisoners { get; set; }
    }
}
