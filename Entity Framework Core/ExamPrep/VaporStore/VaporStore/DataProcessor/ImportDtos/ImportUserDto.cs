namespace VaporStore.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.UserValidator;

    public class ImportUserDto
    {
        [Required]
        [RegularExpression(FullNamePattern)]
        public string FullName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(AgeMinRange, AgeMaxRange)]
        public int Age { get; set; }

        public ImportCardDto[] Cards { get; set; }
    }
}
