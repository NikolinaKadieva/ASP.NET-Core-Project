namespace EstateRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Estate;
    public class Estate
    {
        public int Id { get; init; }

        [Required]
        [MaxLengthAttribute(TypeMaxLength)]
        public string Type { get; set; }
        

        [Required]
        [MaxLength(TypeOfConstructionMaxLength)]
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

        public int DealerId { get; init; }

        public Dealer Dealer { get; init; }
    }
}
