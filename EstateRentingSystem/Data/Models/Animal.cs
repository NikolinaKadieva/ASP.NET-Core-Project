namespace EstateRentingSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Animal;
    public class Animal
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(AnimalTypeMaxLength)]
        public string Type { get; set; }

        public IEnumerable<Estate> Estates { get; init; } = new List<Estate>();
    }
}
