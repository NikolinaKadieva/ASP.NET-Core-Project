namespace EstateRentingSystem.Test.Data
{
    using EstateRentingSystem.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Estates
    {
        public static IEnumerable<Estate> TenPublicEstates
            => Enumerable.Range(0, 10).Select(i => new Estate
            { 
                IsPublic = true
            });
    }
}
