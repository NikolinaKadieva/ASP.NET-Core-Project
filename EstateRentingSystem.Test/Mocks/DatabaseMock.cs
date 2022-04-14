namespace EstateRentingSystem.Test.Mocks
{
    using System;
    using EstateRentingSystem.Data;
    using Microsoft.EntityFrameworkCore;
    

    public static class DatabaseMock
    {
        public static EstateRentingDbContext Instance
        {
            get 
            {
                var dbContextOptions = new DbContextOptionsBuilder<EstateRentingDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new EstateRentingDbContext(dbContextOptions);
            }
        }
    }
}
