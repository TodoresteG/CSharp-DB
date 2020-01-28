namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.EmployeeValidator;

    public class ImportEmployeeDto
    {
        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        [RegularExpression(UsernamePattern)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(PhoneNumberPattern)]
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}
