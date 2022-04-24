namespace EstateRentingSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Furniture;
    public class Furniture
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(FurnitureTypeMaxLength)]
        public string Type { get; set; }

        public IEnumerable<Estate> Estates { get; init; } = new List<Estate>();
    }
}