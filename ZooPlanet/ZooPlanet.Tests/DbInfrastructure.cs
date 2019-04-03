namespace ZooPlanet.Tests
{
    using ZooPlanet.Data;

    using Microsoft.EntityFrameworkCore;
    using System;

    public class DbInfrastructure
    {
        public static ZooPlanetDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ZooPlanetDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ZooPlanetDbContext(dbOptions);
        }
    }
}