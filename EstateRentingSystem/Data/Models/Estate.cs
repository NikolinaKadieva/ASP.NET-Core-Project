﻿namespace EstateRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Estate
    {
        public int Id { get; init; }

        [Required]
        [MaxLengthAttribute(EstateTypeMaxLength)]
        public string Type { get; set; }
        

        [Required]
        [MaxLength(EstateTypeOfConstructionMaxLength)]
        public string TypeOfConstruction { get; set; }

        [Required]
        public string Description { get; set; }

        public int YearOfConstruction { get; set; }

        [Required]
        public int Squaring { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }
    }
}