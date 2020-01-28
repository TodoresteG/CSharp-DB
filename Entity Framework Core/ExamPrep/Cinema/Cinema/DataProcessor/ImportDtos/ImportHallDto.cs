namespace Cinema.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.HallValidator;

    public class ImportHallDto
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        [Range(1, 10000000000)]
        public int Seats { get; set; }
    }
}
