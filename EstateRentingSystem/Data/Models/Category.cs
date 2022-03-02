using System.Collections.Generic;

namespace EstateRentingSystem.Data.Models
{
    public class Category
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Estate> Estates { get; init; } = new List<Estate>();
    }
}
