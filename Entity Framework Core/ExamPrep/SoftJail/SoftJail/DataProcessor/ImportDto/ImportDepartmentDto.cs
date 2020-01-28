namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.DepartmentValidator;

    public class ImportDepartmentDto
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public ImportCellDto[] Cells { get; set; }
    }
}
