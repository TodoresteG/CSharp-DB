namespace SoftJail.DataProcessor.ImportDto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.PrisonerValidator;

    public class ImportPrisonerDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(NicknamePattern)]
        public string Nickname { get; set; }

        [Range(AgeMinRange, AgeMaxRange)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        [Range(typeof(decimal), BailMinRange, BailMaxRange)]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public ImportMailDto[] Mails { get; set; }
    }
}
