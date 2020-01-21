namespace P01_StudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.ResourceValidator;

    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(UrlMaxLenght)]
        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
