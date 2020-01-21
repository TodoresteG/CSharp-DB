﻿namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.TownValidator;

    public class Town
    {
        [Key]
        public int TownId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();

        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
